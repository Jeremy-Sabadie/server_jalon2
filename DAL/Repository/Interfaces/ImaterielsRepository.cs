using domain.Entities;

namespace DAL.Repository.Interfaces
{
    public interface ImaterielsRepository
    {
        /// <summary>
        /// Create new material using the  given device arguments.
        /// </summary>
        /// <param name="materiel"></param>
        /// <returns></returns>
        public Task<Materiel> CreateMaterielAsync(Materiel materiel);
        /// <summary>
        /// Retrieve all de data base devices.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Materiel>> GetAllMaterielsAsync();
        public Task<IEnumerable<Materiel>> GetMaterielsByCategory(int reference);
        /// <summary>
        /// Retrieve a device according its id gived with parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Materiel> GetMaterielByIdAsync(int id);
        /// <summary>
        /// Updates the device with the new values gived inthe parameters where its id match.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldName"></param>
        /// <param name="oldServiceDat"></param>
        /// <param name="oldEndGarantee"></param>
        /// <param name="oldProprietaireID"></param>
        /// <param name="newName"></param>
        /// <param name="newServiceDat"></param>
        /// <param name="newEndGarantee"></param>
        /// <param name="newProprietaireId"></param>
        /// <returns></returns>
        public Task<Materiel> UpdateMaterielAsync(Materiel materiel);
        /// <summary>
        /// Delete the device  that have the id gived in the parameter.
        /// </summary>
        /// <param name="id"></param>
        public Task<bool> DeleteMaterielAsync(int id);
        public Task<IEnumerable<Materiel>> GetMaterielByKeyValueAsync(string keyValue);
        public Task<IEnumerable<Category>> GetCategoryWhereMateriel(int idMat);
    }



}
