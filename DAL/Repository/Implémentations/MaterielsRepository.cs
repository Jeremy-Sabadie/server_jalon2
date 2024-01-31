using DAL.Repository.Interfaces;
using DAL.Sessions.Interfaces;
using Dapper;
using domain.Entities;

namespace DAL.Repository.Implémentations
{
    internal class MaterielsRepository : ImaterielsRepository
    {
        private readonly IDBSession _db;

        public MaterielsRepository(IDBSession dBSession)
        {
            _db = dBSession;
        }
        #region create materiel
        public async Task<Materiel> CreateMaterielAsync(Materiel materiel)
        {
            string name = materiel.Name;
            DateTime serviceDat = materiel.ServiceDat;
            DateTime endGarantee = materiel.EndGarantee;
            int? proprietaireId = materiel.ProprietaireId;
            List<Category> categories = materiel.categories;

            //Materiel insert in the Materiel table request:
            string createMaterielRequest = "Insert into materiel(name,serviceDat,endGarantee,proprietaireId) values(@name,@serviceDat,@endGarantee,@proprietaireId); SELECT LAST_INSERT_ID()";
            //Insert thidMat created in the CategoryMateriel table request:
            string insertIdMaterielIntoCatMat = " insert into CategoryMateriel(idMat) values(@id)";
            // Request for the insert of the device's category référecnce created in the Category table:
            string InsertInCategoryReferenceInCatMat = "insert into CategoryMateriel(refCat, idMat) values (@category, @lastinsertedid)";

            //Execute insertIdMaterielIntoCatMat:
            var lastinsertedid = await _db.Connection.ExecuteScalarAsync<int>(createMaterielRequest, new
            {
                name,
                serviceDat,
                endGarantee,
                proprietaireId

            }, _db.Transaction);
            if (lastinsertedid > 0)
            {
                materiel.Id = lastinsertedid;
                var insertInCatMat = await _db.Connection.ExecuteAsync(InsertInCategoryReferenceInCatMat, new
                {
                    lastinsertedid,
                    category = categories[0].Reference
                }, _db.Transaction);
                return materiel;
            }
            else return null;





        }
        #endregion
        /// <summary>
        /// Retrieve all the database devices.
        /// </summary>
        /// <returns></returns>
        /// 
        #region get all
        public async Task<IEnumerable<Materiel>> GetAllMaterielsAsync()
        {
            string request = "select * from materiel";
            return await _db.Connection.QueryAsync<Materiel>(request, transaction: _db.Transaction);
        }
        #endregion
        #region get by id
        /// <summary>
        /// Retrieve a device according the id gived of parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Materiel> GetMaterielByIdAsync(int id)
        {
            string request = "select * from materiel where id= @ID";
            return await _db.Connection.QueryFirstOrDefaultAsync<Materiel>(request, new { ID = id }, _db.Transaction);
        }
        #endregion
        #region update materiel
        /// <summary>
        /// Update the material  with the new values gived with the materiel in arguments.
        /// </summary>
        /// <param name="materiel"></param>
        /// <returns></returns>
        public async Task<Materiel> UpdateMaterielAsync(Materiel materiel)
        {
            int id = materiel.Id;
            string newName = materiel.Name;
            DateTime newServiceDate = materiel.ServiceDat;
            DateTime newEndGarantee = materiel.EndGarantee;
            int? newProprietaireId = materiel.ProprietaireId;
            List<Category> categories = materiel.categories;
            DateTime now = DateTime.Now;

            // 1- Récupération de la date stockée pour vérifier si la mise à jour peut être effectuée.
            string lastUpdateRetrieveRequest = "SELECT lastUpdate FROM materiel WHERE id = @id";
            DateTime? lastUpdate = await _db.Connection.QueryFirstOrDefaultAsync<DateTime?>(lastUpdateRetrieveRequest, new { id });

            // 2- Vérification si la mise à jour peut être effectuée en comparant les dates.
            // if (lastUpdate.HasValue && materiel.lastUpdate != lastUpdate.Value) throw new UpdateEntityException(materiel);

            // REQUÊTE pour l'update des valeurs autres que les catégories.
            string updateQuery = "UPDATE materiel SET name = @newName, serviceDat = @newServiceDate, endGarantee = @newEndGarantee, proprietaireId = @newProprietaireId, lastUpdate = @now WHERE id = @id";
            // Exécution de la requête de mise à jour.
            var updated = await _db.Connection.ExecuteAsync(updateQuery, new { id, newName, newServiceDate, newEndGarantee, newProprietaireId, now });

            // SUPPRESSION des anciennes liaisons de catégories du matériel.
            string deleteCategories = "DELETE FROM CategoryMateriel WHERE idMat = @id";
            var clearCategories = await _db.Connection.ExecuteAsync(deleteCategories, new { id });

            // INSERTION des nouvelles liaisons pour la mise à jour des catégories du matériel.
            foreach (var category in materiel.categories)
            {
                string newMaterialCategories = "INSERT INTO CategoryMateriel (idMat, refCat) VALUES (@id, @category)";
                // Exécution de la requête d'insertion pour chaque catégorie.
                var insertNewCategories = await _db.Connection.ExecuteAsync(newMaterialCategories, new { id, category = category.Reference });
            }

            materiel.lastUpdate = DateTime.Now;
            return materiel;

        }
        #endregion
        public async Task<IEnumerable<Materiel>> GetMaterielsByCategory(int reference)
        {//Sélection de tous les matériels quand le paramètre passé correspon à la rérérence de la table category jointe à la table CategoryMateriel et Materiels
            string request = @"
                    SELECT materiel.* FROM materiel 
                    JOIN CategoryMateriel ON materiel.id = CategoryMateriel.idMat
                    JOIN category ON CategoryMateriel.refCat = category.reference
                    WHERE category.reference = @reference";

            return await _db.Connection.QueryAsync<Materiel>(request, new { reference });
        }

        #region delete materiel
        /// <summary>
        /// removes material according to its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteMaterielAsync(int id)
        {//TODO: transaction for the delete of materiel in the category table. 
            string DeleteFromCategoryRequest = "delete from CategoryMateriel where idMat = @id";
            string deleteFromMaterielRequest = "delete from materiel where id= @id";

            await _db.Connection.ExecuteAsync(DeleteFromCategoryRequest, new { id }, _db.Transaction);
            var nbSupp = await _db.Connection.ExecuteAsync(deleteFromMaterielRequest, new { id }, _db.Transaction);

            return nbSupp > 0;
        }
        #endregion
        //La fonction GetMaterielByValueAsync prend un string servant de valeur de comparaison en BDD:
        public async Task<IEnumerable<Materiel>> GetMaterielByKeyValueAsync(string keyValue)
        {//  Requête de comparaison  de la clef récupéré dan la barre de recherche avec les id des matériels pour récupérer les matériels correspondant grace à des jointures entre les tables materiel, CategoryMateriel et category.
            string request = @"
             SELECT distinct m.*
FROM materiel m 
JOIN CategoryMateriel cm ON cm.idMat = m.id 
JOIN category c ON c.reference = cm.refCat 
JOIN users u ON u.id = m.proprietaireId 
WHERE  u.name LIKE @keyValue
OR m.name LIKE @keyValue
OR m.id = @idKey";

            //Parse de la variable de comparaison de l'id.
            int.TryParse(keyValue, out int idkey);
            var keyIsMatching = await _db.Connection.QueryAsync<Materiel>(request, new { keyValue = "%" + keyValue + "%", idKey = idkey });

            return keyIsMatching;

        }
        public async Task<IEnumerable<Category>> GetCategoryWhereMateriel(int idMat)
        {
            string request = @"
SELECT c.name FROM category c 
JOIN CategoryMateriel cm ON c.Reference = cm.refCat 
JOIN materiel m ON cm.idMat =m.id 
WHERE cm.idMat =@idMat";
            //Exécution de la reqête
            return await _db.Connection.QueryAsync<Category>(request, new { idMat });
        }

    }
}

