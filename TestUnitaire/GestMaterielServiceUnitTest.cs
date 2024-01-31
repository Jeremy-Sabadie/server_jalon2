using BLL.services;
using DAL.Repository.Interfaces;
using DAL.UOW;
using domain.Entities;
using Moq;

namespace TestUnitaire
{
    public class GestMaterielServiceUnitTest
    {
        //1- Test unitaire de la fonction CreateMaterielAsync() pour  la création de matériel:
        [Fact]
        public async Task CreateMaterielAsync()
        {
            //Arrange:

            //New Materiel:
            Materiel newDevice = new Materiel
            {
                Id = It.IsAny<int>(),
                Name = "test",
                ServiceDat = DateTime.Now,
                EndGarantee = DateTime.Now.AddYears(5),
                ProprietaireId = 5,
                categories = null
            };

            //Création du mock pour simulacre:
            var mockDb = Mock.Of<IUOW>();
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();
            Mock.Get(mockMaterielRepository)
                .Setup(mr => mr.CreateMaterielAsync(newDevice))
                .ReturnsAsync(new Materiel()
                {
                    Id = 1,
                    Name = "test",
                    ServiceDat = DateTime.Now,
                    EndGarantee = DateTime.Now.AddYears(5),
                    ProprietaireId = 5,
                    categories = null
                });
            Mock.Get(mockDb).Setup(db => db.Materiels).Returns(mockMaterielRepository);

            var gestMaterielService = new GestMaterielService(mockDb);

            //Act appel de la fonction à tester:
            var result = await gestMaterielService.CreateMaterielAsync(newDevice);


            //Assert, vérification du retour:
            Assert.NotNull(result);// Vérifie que le résultat n'est pas null.
            Assert.Equal(newDevice.Name, result.Name);
            Assert.Equal(1, result.Id);
        }
        //=============================================================================================================================
        //2- Test unitaire de la fonction UpdateMaterielAsync() pour la mise à jour du matériel:
        [Fact]
        public async Task UpdateMaterielAsync()
        {
            //Arrange:
            //Noveau DTOmaterielRequest pour pouvoir comparer les valeurs au résultat du test:
            Materiel updateValues = new Materiel()
            {
                Id = 2,
                Name = "test",
                ServiceDat = DateTime.Now,
                EndGarantee = DateTime.Now.AddYears(5),
                ProprietaireId = 5,
                categories = null,
                lastUpdate = DateTime.Now
            };

            //Création d'un mock duUnit Of Work pour simuler l'appel de la fonction à tester:
            var mockUOW = Mock.Of<IUOW>();
            //Création d'un mock du MaterielRepository pour simuler l'appel de la fonction à tester:
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();
            //Quand le Mock UOW appellera le mock MaterielRepository (de l'interface ImaterielRepository)il retournera la méthode UpdateMaterielAsync que l'on veut tester et retournera le résultat ( le nouveau matériel mis à jour dont on donne les valeurs attendues pour la comparaison:
            Mock.Get(mockMaterielRepository)
                .Setup(mr => mr.UpdateMaterielAsync(updateValues))
                //Retour des valeurs attendues dans le matériel retourné par la méthode testé:
                .ReturnsAsync(
                new Materiel()
                {
                    Id = 2,
                    Name = "test",
                    ServiceDat = DateTime.Now,
                    EndGarantee = DateTime.Now.AddYears(5),
                    ProprietaireId = 5,
                    categories = null,
                });
            //Si appel du mock du UOW on retourne le mock de materiel repository:
            Mock.Get(mockUOW).Setup(UOW => UOW.Materiels).Returns(mockMaterielRepository);
            //Création du nouveau mock gestMaterielService qui implémente la méthode à tester:
            var gestMaterielService = new GestMaterielService(mockUOW); //SUT System Under Test.

            //Act appel de la fonction à tester avec les valeur pour la mise à jour comme  arguments:
            var result = await gestMaterielService.UpdateMaterielAsync(updateValues);


            //Assert, comparaison du retour du test:
            Assert.NotNull(result);// Vérifie que le résultat n'est pas null.
            Assert.Equal(updateValues.Name, result.Name);//Vérifie que la valeur de l'attribut Name attendue correspond à celle retournée.
            Assert.Equal(updateValues.Id, result.Id);//Vérifie que la valeur de l'attribut Id attendue correspond à celle retournée.
        }
        //Fin du test.
        //=============================================================================================================================

        //3- Test unitaire de la fonction DeleteMaterielAsync() pour la suppression des matériels:
        //      Définition d'un cas de test pour une méthode de théorie (Theory)        
        [Theory]
        // avec une donnée d'entrée spécifique (InlineData) égale à 1
        [InlineData(1)]
        public async void DeleteMaterielAsyncIs_false(int idMat)
        {
            // Configuration des données pour le test DeleteMaterielAsync() avec un identifiant donné(idMat):
            //Arange:
            // Création des mocks pour la simulation :
            var mockDb = Mock.Of<IUOW>();//Mock du Unit Of Work
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();//Mock du répository des matériels.

            // Configuration du comportement du mock pour la méthode Materiels du mockDb:
            Mock.Get(mockDb)
        // Configuration du comportement du mock pour la méthode 'Materiels' de l'objet mockDb:
        .Setup(ms => ms.Materiels)
        .Returns(mockMaterielRepository);
            // Configuration du comportement du mock pour la méthode DeleteMaterielAsync du mockMaterielRepository:
            Mock.Get(mockMaterielRepository)
         .Setup(ms => ms.DeleteMaterielAsync(idMat))
         // Configuration du mock pour retourner une valeur asynchrone (false dans ce cas):
         .ReturnsAsync(false);

            var MaterielService = new GestMaterielService(mockDb);
            //Act:

            var result = await MaterielService.DeleteMaterielAsync(idMat);
            //Assert:           
            // vérification pour savoir  si le résultat est vrai ou faux (dans ce cas, devrait être faux)
            Assert.True(!result);
        }

        //=============================================================================================================================
        [Theory]
        // avec une donnée d'entrée spécifique (InlineData) égale à 1
        [InlineData(1)]
        public async void DeleteMaterielAsyncIs_true(int idMat)
        {
            // Configuration des données pour le test DeleteMaterielAsync() avec un identifiant donné(idMat):
            //Arange:
            // Création des mocks pour la simulation :
            var mockDb = Mock.Of<IUOW>();//Mock du Unit Of Work
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();//Mock du répository des matériels.

            // Configuration du comportement du mock pour la méthode Materiels du mockDb:
            Mock.Get(mockDb)
        // Configuration du comportement du mock pour la méthode 'Materiels' de l'objet mockDb:
        .Setup(ms => ms.Materiels)
        .Returns(mockMaterielRepository);
            // Configuration du comportement du mock pour la méthode DeleteMaterielAsync du mockMaterielRepository:
            Mock.Get(mockMaterielRepository)
         .Setup(ms => ms.DeleteMaterielAsync(idMat))
         // Configuration du mock pour retourner une valeur asynchrone (false dans ce cas):
         .ReturnsAsync(true);

            var MaterielService = new GestMaterielService(mockDb);
            //Act:

            var result = await MaterielService.DeleteMaterielAsync(idMat);
            //Assert:           
            // vérification pour savoir  si le résultat est vrai ou faux (dans ce cas, devrait être vrai)
            Assert.True(result);
        }

        //=============================================================================================================================
        //4 - Test unitaire de la fonction GetAllMaterielsAsync() pour l'affichage des matériels:
        [Fact]
        public async Task GetAllMaterielsAsync()
        {//Arrange:
            var mockDb = Mock.Of<IUOW>();//Mock du Unit Of Work
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();//Mock du répository des matériels.
            // Configuration du comportement du mock du UOW mockDb pour qu'il retourne le Materielrepository qui implémente la méthode à tester:
            Mock.Get(mockDb)
                .Setup(ms => ms.Materiels)
        .Returns(mockMaterielRepository);
            // Configuration du comportement du mock pour la méthode DGetAllMaterielsAsync() du mockMaterielRepository:
            Mock.Get(mockMaterielRepository)
             .Setup(ms => ms.GetAllMaterielsAsync())
             //Retourne une liste vide de matériels lors de l'appel de la méthode GetAllMaterielsAsync()
             .ReturnsAsync(new List<Materiel>());


            var MaterielService = new GestMaterielService(mockDb);
            //Act:
            //Appel de la méthode GetAllMaterielsAsync() à tester:
            IEnumerable<Materiel> materiels = await MaterielService.GetAllMaterielsAsync();

            //Assert:
            //Vérification que la méthode du repository  testé retourne bien une liste vide de matériels.
            Assert.Empty(materiels);
        }
    }
    //=============================================================================================================================

}
