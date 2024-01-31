using BLL.services;
using DAL.Repository.Interfaces;
using DAL.UOW;
using domain.Entities;
using Moq;

namespace TestUnitaire
{
    public class GestMaterielServiceUnitTest
    {
        //1- Test unitaire de la fonction CreateMaterielAsync() pour  la cr�ation de mat�riel:
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

            //Cr�ation du mock pour simulacre:
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

            //Act appel de la fonction � tester:
            var result = await gestMaterielService.CreateMaterielAsync(newDevice);


            //Assert, v�rification du retour:
            Assert.NotNull(result);// V�rifie que le r�sultat n'est pas null.
            Assert.Equal(newDevice.Name, result.Name);
            Assert.Equal(1, result.Id);
        }
        //=============================================================================================================================
        //2- Test unitaire de la fonction UpdateMaterielAsync() pour la mise � jour du mat�riel:
        [Fact]
        public async Task UpdateMaterielAsync()
        {
            //Arrange:
            //Noveau DTOmaterielRequest pour pouvoir comparer les valeurs au r�sultat du test:
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

            //Cr�ation d'un mock duUnit Of Work pour simuler l'appel de la fonction � tester:
            var mockUOW = Mock.Of<IUOW>();
            //Cr�ation d'un mock du MaterielRepository pour simuler l'appel de la fonction � tester:
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();
            //Quand le Mock UOW appellera le mock MaterielRepository (de l'interface ImaterielRepository)il retournera la m�thode UpdateMaterielAsync que l'on veut tester et retournera le r�sultat ( le nouveau mat�riel mis � jour dont on donne les valeurs attendues pour la comparaison:
            Mock.Get(mockMaterielRepository)
                .Setup(mr => mr.UpdateMaterielAsync(updateValues))
                //Retour des valeurs attendues dans le mat�riel retourn� par la m�thode test�:
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
            //Cr�ation du nouveau mock gestMaterielService qui impl�mente la m�thode � tester:
            var gestMaterielService = new GestMaterielService(mockUOW); //SUT System Under Test.

            //Act appel de la fonction � tester avec les valeur pour la mise � jour comme  arguments:
            var result = await gestMaterielService.UpdateMaterielAsync(updateValues);


            //Assert, comparaison du retour du test:
            Assert.NotNull(result);// V�rifie que le r�sultat n'est pas null.
            Assert.Equal(updateValues.Name, result.Name);//V�rifie que la valeur de l'attribut Name attendue correspond � celle retourn�e.
            Assert.Equal(updateValues.Id, result.Id);//V�rifie que la valeur de l'attribut Id attendue correspond � celle retourn�e.
        }
        //Fin du test.
        //=============================================================================================================================

        //3- Test unitaire de la fonction DeleteMaterielAsync() pour la suppression des mat�riels:
        //      D�finition d'un cas de test pour une m�thode de th�orie (Theory)        
        [Theory]
        // avec une donn�e d'entr�e sp�cifique (InlineData) �gale � 1
        [InlineData(1)]
        public async void DeleteMaterielAsyncIs_false(int idMat)
        {
            // Configuration des donn�es pour le test DeleteMaterielAsync() avec un identifiant donn�(idMat):
            //Arange:
            // Cr�ation des mocks pour la simulation :
            var mockDb = Mock.Of<IUOW>();//Mock du Unit Of Work
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();//Mock du r�pository des mat�riels.

            // Configuration du comportement du mock pour la m�thode Materiels du mockDb:
            Mock.Get(mockDb)
        // Configuration du comportement du mock pour la m�thode 'Materiels' de l'objet mockDb:
        .Setup(ms => ms.Materiels)
        .Returns(mockMaterielRepository);
            // Configuration du comportement du mock pour la m�thode DeleteMaterielAsync du mockMaterielRepository:
            Mock.Get(mockMaterielRepository)
         .Setup(ms => ms.DeleteMaterielAsync(idMat))
         // Configuration du mock pour retourner une valeur asynchrone (false dans ce cas):
         .ReturnsAsync(false);

            var MaterielService = new GestMaterielService(mockDb);
            //Act:

            var result = await MaterielService.DeleteMaterielAsync(idMat);
            //Assert:           
            // v�rification pour savoir  si le r�sultat est vrai ou faux (dans ce cas, devrait �tre faux)
            Assert.True(!result);
        }

        //=============================================================================================================================
        [Theory]
        // avec une donn�e d'entr�e sp�cifique (InlineData) �gale � 1
        [InlineData(1)]
        public async void DeleteMaterielAsyncIs_true(int idMat)
        {
            // Configuration des donn�es pour le test DeleteMaterielAsync() avec un identifiant donn�(idMat):
            //Arange:
            // Cr�ation des mocks pour la simulation :
            var mockDb = Mock.Of<IUOW>();//Mock du Unit Of Work
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();//Mock du r�pository des mat�riels.

            // Configuration du comportement du mock pour la m�thode Materiels du mockDb:
            Mock.Get(mockDb)
        // Configuration du comportement du mock pour la m�thode 'Materiels' de l'objet mockDb:
        .Setup(ms => ms.Materiels)
        .Returns(mockMaterielRepository);
            // Configuration du comportement du mock pour la m�thode DeleteMaterielAsync du mockMaterielRepository:
            Mock.Get(mockMaterielRepository)
         .Setup(ms => ms.DeleteMaterielAsync(idMat))
         // Configuration du mock pour retourner une valeur asynchrone (false dans ce cas):
         .ReturnsAsync(true);

            var MaterielService = new GestMaterielService(mockDb);
            //Act:

            var result = await MaterielService.DeleteMaterielAsync(idMat);
            //Assert:           
            // v�rification pour savoir  si le r�sultat est vrai ou faux (dans ce cas, devrait �tre vrai)
            Assert.True(result);
        }

        //=============================================================================================================================
        //4 - Test unitaire de la fonction GetAllMaterielsAsync() pour l'affichage des mat�riels:
        [Fact]
        public async Task GetAllMaterielsAsync()
        {//Arrange:
            var mockDb = Mock.Of<IUOW>();//Mock du Unit Of Work
            var mockMaterielRepository = Mock.Of<ImaterielsRepository>();//Mock du r�pository des mat�riels.
            // Configuration du comportement du mock du UOW mockDb pour qu'il retourne le Materielrepository qui impl�mente la m�thode � tester:
            Mock.Get(mockDb)
                .Setup(ms => ms.Materiels)
        .Returns(mockMaterielRepository);
            // Configuration du comportement du mock pour la m�thode DGetAllMaterielsAsync() du mockMaterielRepository:
            Mock.Get(mockMaterielRepository)
             .Setup(ms => ms.GetAllMaterielsAsync())
             //Retourne une liste vide de mat�riels lors de l'appel de la m�thode GetAllMaterielsAsync()
             .ReturnsAsync(new List<Materiel>());


            var MaterielService = new GestMaterielService(mockDb);
            //Act:
            //Appel de la m�thode GetAllMaterielsAsync() � tester:
            IEnumerable<Materiel> materiels = await MaterielService.GetAllMaterielsAsync();

            //Assert:
            //V�rification que la m�thode du repository  test� retourne bien une liste vide de mat�riels.
            Assert.Empty(materiels);
        }
    }
    //=============================================================================================================================

}
