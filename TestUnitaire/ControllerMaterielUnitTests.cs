using apiMateriels.Controllers;
using BLL.interfaces;
using domain.DTO;
using domain.DTO.Requests;
using domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestUnitaire
{

    public class ControllerMaterielUnitTests
    {

        //CreateMaterielAsync_is_ok() unit test:
        [Fact]
        public async Task CreateMaterielAsync_return_ok()
        {
            //Arrange:
            DTOmaterielRequest dTOmaterielRequest = new DTOmaterielRequest()
            {
                Name = "Toto",
                categories = new List<int>(),
                endGarantee = DateTime.Now,
                LastUpdate = DateTime.Now.AddYears(5),
                proprietaireId = 2,
                serviceDat = DateTime.Now
            };

            Materiel createdDevice = new Materiel()
            {
                Id = 5,
                Name = dTOmaterielRequest.Name,
                ServiceDat = dTOmaterielRequest.serviceDat,
                EndGarantee = dTOmaterielRequest.endGarantee,
                ProprietaireId = dTOmaterielRequest.proprietaireId,
                categories = dTOmaterielRequest.categories.Select(reference => new Category() { Reference = reference }).ToList()

            };

            IGestMaterielsService gestMaterielsService = Mock.Of<IGestMaterielsService>();
            Mock.Get(gestMaterielsService)
                .Setup(service => service.CreateMaterielAsync(It.IsAny<Materiel>()))
                .ReturnsAsync(createdDevice);


            DTOmaterielResponse expected = new DTOmaterielResponse()
            {
                Id = createdDevice.Id,
                Name = createdDevice.Name,
                ServiceDat = createdDevice.ServiceDat,
                EndGarantee = createdDevice.EndGarantee,
                proprietaireId = createdDevice.ProprietaireId
            };

            MaterielsController materielsController = new MaterielsController(gestMaterielsService);

            //Act:
            var result = await materielsController.CreateMaterielAsync(dTOmaterielRequest);

            //Assert:
            Assert.NotNull(result as OkObjectResult);

            var content = (result as OkObjectResult).Value as DTOmaterielResponse;
            Assert.NotNull(content);

            Assert.Equal(expected.Id, content.Id);
            Assert.Equal(expected.Name, content.Name);
            Assert.Equal(expected.ServiceDat, content.ServiceDat);
            Assert.Equal(expected.EndGarantee, content.EndGarantee);
            Assert.Equal(expected.proprietaireId, content.proprietaireId);
        }
        //======================================================================================================
        //CreateMaterielAsync_return_bad_request_because_name_not_set() unit test:
        [Fact]
        public async Task CreateMaterielAsync_return_bad_request_because_name_not_set()
        {
            //Arrange:
            DTOmaterielRequest dTOmaterielRequest = new DTOmaterielRequest()
            {
                categories = new List<int>(),
                endGarantee = DateTime.Now,
                LastUpdate = DateTime.Now.AddYears(5),
                proprietaireId = 2,
                serviceDat = DateTime.Now
            };

            Materiel createdDevice = new Materiel()
            {
                Id = 5,
                Name = dTOmaterielRequest.Name,
                ServiceDat = dTOmaterielRequest.serviceDat,
                EndGarantee = dTOmaterielRequest.endGarantee,
                ProprietaireId = dTOmaterielRequest.proprietaireId,
                categories = dTOmaterielRequest.categories.Select(reference => new Category() { Reference = reference }).ToList()

            };

            IGestMaterielsService gestMaterielsService = Mock.Of<IGestMaterielsService>();
            Mock.Get(gestMaterielsService)
                .Setup(service => service.CreateMaterielAsync(It.IsAny<Materiel>()))
                .ReturnsAsync(createdDevice);


            DTOmaterielResponse expected = new DTOmaterielResponse()
            {
                Id = createdDevice.Id,
                Name = createdDevice.Name,
                ServiceDat = createdDevice.ServiceDat,
                EndGarantee = createdDevice.EndGarantee,
                proprietaireId = createdDevice.ProprietaireId
            };

            MaterielsController materielsController = new MaterielsController(gestMaterielsService);

            //Act:
            var result = await materielsController.CreateMaterielAsync(dTOmaterielRequest);

            //Assert:
            Assert.NotNull(result as OkObjectResult);

            var content = (result as OkObjectResult).Value as DTOmaterielResponse;
            Assert.NotNull(content);

            Assert.Equal(expected.Id, content.Id);
            Assert.Equal(expected.Name, content.Name);
            Assert.Equal(expected.ServiceDat, content.ServiceDat);
            Assert.Equal(expected.EndGarantee, content.EndGarantee);
            Assert.Equal(expected.proprietaireId, content.proprietaireId);
        }
        //======================================================================================================
        //CreateMaterielAsync_return_obad_request_because_nameserviceDat_not_set() unit test:
        [Fact]
        public async Task CreateMaterielAsync_return_obad_request_because_nameserviceDat_not_set()
        {
            //Arrange:
            DTOmaterielRequest dTOmaterielRequest = new DTOmaterielRequest()
            {
                Name = "test",
                categories = new List<int>(),
                endGarantee = DateTime.Now,
                LastUpdate = DateTime.Now.AddYears(5),
                proprietaireId = 2,

            };

            Materiel createdDevice = new Materiel()
            {
                Id = 5,
                Name = dTOmaterielRequest.Name,
                ServiceDat = dTOmaterielRequest.serviceDat,
                EndGarantee = dTOmaterielRequest.endGarantee,
                ProprietaireId = dTOmaterielRequest.proprietaireId,
                categories = dTOmaterielRequest.categories.Select(reference => new Category() { Reference = reference }).ToList()

            };

            IGestMaterielsService gestMaterielsService = Mock.Of<IGestMaterielsService>();
            Mock.Get(gestMaterielsService)
                .Setup(service => service.CreateMaterielAsync(It.IsAny<Materiel>()))
                .ReturnsAsync(createdDevice);


            DTOmaterielResponse expected = new DTOmaterielResponse()
            {
                Id = createdDevice.Id,
                Name = createdDevice.Name,
                ServiceDat = createdDevice.ServiceDat,
                EndGarantee = createdDevice.EndGarantee,
                proprietaireId = createdDevice.ProprietaireId
            };

            MaterielsController materielsController = new MaterielsController(gestMaterielsService);

            //Act:
            var result = await materielsController.CreateMaterielAsync(dTOmaterielRequest);

            //Assert:
            Assert.NotNull(result as OkObjectResult);

            var content = (result as OkObjectResult).Value as DTOmaterielResponse;
            Assert.NotNull(content);

            Assert.Equal(expected.Id, content.Id);
            Assert.Equal(expected.Name, content.Name);
            Assert.Equal(expected.ServiceDat, content.ServiceDat);
            Assert.Equal(expected.EndGarantee, content.EndGarantee);
            Assert.Equal(expected.proprietaireId, content.proprietaireId);
        }
        //======================================================================================================
        //CreateMaterielAsync_return_bad_request_because_endGarantee_not_set() unit test:
        [Fact]
        public async Task CreateMaterielAsync_return_bad_request_because_endGarantee_not_set()
        {
            //Arrange:
            DTOmaterielRequest dTOmaterielRequest = new DTOmaterielRequest()
            {
                Name = "test",
                categories = new List<int>(),

                LastUpdate = DateTime.Now.AddYears(5),
                proprietaireId = 2,
                serviceDat = DateTime.Now
            };

            Materiel createdDevice = new Materiel()
            {
                Id = 5,
                Name = dTOmaterielRequest.Name,
                ServiceDat = dTOmaterielRequest.serviceDat,
                EndGarantee = dTOmaterielRequest.endGarantee,
                ProprietaireId = dTOmaterielRequest.proprietaireId,
                categories = dTOmaterielRequest.categories.Select(reference => new Category() { Reference = reference }).ToList()

            };

            IGestMaterielsService gestMaterielsService = Mock.Of<IGestMaterielsService>();
            Mock.Get(gestMaterielsService)
                .Setup(service => service.CreateMaterielAsync(It.IsAny<Materiel>()))
                .ReturnsAsync(createdDevice);


            DTOmaterielResponse expected = new DTOmaterielResponse()
            {
                Id = createdDevice.Id,
                Name = createdDevice.Name,
                ServiceDat = createdDevice.ServiceDat,
                EndGarantee = createdDevice.EndGarantee,
                proprietaireId = createdDevice.ProprietaireId
            };

            MaterielsController materielsController = new MaterielsController(gestMaterielsService);

            //Act:
            var result = await materielsController.CreateMaterielAsync(dTOmaterielRequest);

            //Assert:
            Assert.NotNull(result as OkObjectResult);

            var content = (result as OkObjectResult).Value as DTOmaterielResponse;
            Assert.NotNull(content);

            Assert.Equal(expected.Id, content.Id);
            Assert.Equal(expected.Name, content.Name);
            Assert.Equal(expected.ServiceDat, content.ServiceDat);
            Assert.Equal(expected.EndGarantee, content.EndGarantee);
            Assert.Equal(expected.proprietaireId, content.proprietaireId);
        }
        //======================================================================================================
        //TO DO: getAllMaterielsAsync() unit test:
        [Fact]
        public async Task getAllMaterielsAsync()
        {
            //Arrange:
            var mockMaterielService = Mock.Of<IGestMaterielsService>();


            Mock.Get(mockMaterielService)
            .Setup(ms => ms.GetAllMaterielsAsync())
            .ReturnsAsync(new List<Materiel>());

            var materielController = new MaterielsController(mockMaterielService);

            //Act:
            var materiels = await materielController.getAllMaterielsAsync();

            //Assert:
            Assert.NotNull(materiels as OkObjectResult);
            Assert.Empty((materiels as OkObjectResult).Value as List<Materiel>);

            //}
            //====================================================================================================== 
            //TO DO: GetMaterielsByReference() unit test:
            //[Theory]
            //public async void GetMaterielByReference(){
            //Arrange:
            //Act:
            //Assert:
            //}
            //======================================================================================================
            //TO DO: UpdateMaterielAsync() unit test:
            //[Fact]
            //public Task TaskupdateMaterielAsync(){
            //Arrange:
            //Act:
            //Assert:
            //}
            //======================================================================================================
            //TO DO: DeleteMaterielAsync() unit test:
            //[Theory]
            //[InlineData(1)]
            //public async void DeleteMaterielAsync(){
            //Arrange:
            //Act:
            //Assert:
            //}
            //======================================================================================================
        }
    }
}
//os identifiants de connexion sont désormais :

