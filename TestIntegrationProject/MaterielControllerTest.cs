using domain.DTO;
using domain.DTO.Requests;
using domain.Entities;
using System.Net.Http.Json;
using TestIntegrationProject.Fixture;
using Xunit.Abstractions;

namespace TestIntegrationProject;

public class MaterielControllerTest : AbstractIntegrationTests
{
    private readonly ITestOutputHelper _logger;
    public MaterielControllerTest(APIfactory fixture, ITestOutputHelper logger) : base(fixture)
    {
        _logger = logger;
    }

    //Le décorateur Theory est utilisé pour les tests comprtant des valeurs passées en paramètre, les id 2 et 3 ici.
    //Un test sera effectué pour chaque valeur de paramètre indiquées dans les différents décorateurs pour la méthode testée.
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetMaterielWithID3ShouldBeOkAsync(int id)
    {
        //Arange.

        //ACT:
        var materiel = await _httpClient.GetFromJsonAsync<Materiel>($"api/materiels/{id}");

        //ASSERT
        Assert.NotNull(materiel);
        Assert.Equal(id, materiel.Id);
    }

    [Fact]
    public async Task GetAlMaterielShoulReturn5Elements()
    {
        var allMateriels = await _httpClient.GetFromJsonAsync<IEnumerable<Materiel>>("api/materiels");
    }




    //Le test sera effectuer en passant comme id de matériel la valeur indiqué dans le décorateur dans le paramètre de la méthode deletetesté.
    //    [InlineData(4)]  
    [Theory]

    [InlineData(4)]
    public async Task UpdateMaterielAsync(int id)
    {//ASSERT:
        DTOmaterielRequest newValues = new()
        {
            Name = "est",
            serviceDat = DateTime.Today,
            endGarantee = (DateTime.Now).AddYears(5),
            categories = new() { 1 }
        };
        //ACT:
        var rep = await _httpClient.PutAsJsonAsync<DTOmaterielRequest>("", newValues);
        //Déserialisation du condenu du Json de la réponse.
        var actual = await rep.Content.ReadFromJsonAsync<DTOmaterielResponse>();

        //ASSERT:
        //ASSERT:vérification de l'égalité de l'objet attendu et de celui retourné:
        Assert.Equal(rep.StatusCode, System.Net.HttpStatusCode.OK);


    }

    [Theory]
    [InlineData(5)]
    public async Task DeleteMaterielAsync(int id)
    {
        //ACT:
        var deleted = await _httpClient.DeleteAsync($"api/materiels/{id}");

        //        //ASSERT:
        Assert.Equal(deleted.StatusCode, System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateMaterielAsync()
    {
        //ASSERT:
        //Création d'un DTOmaterielReques contenant les valeurs du futur matériel a créer pour le test:
        DTOmaterielRequest createMateriel = new()
        {
            Name = "est",
            serviceDat = DateTime.Today,
            endGarantee = (DateTime.Now).AddYears(5),
            categories = new() { 1 }
        };
        //Création d'un DTOmaterielResponse contenant les valeurs retournées par serveur de test:
        DTOmaterielResponse expected = new()
        {
            Name = createMateriel.Name,
            ServiceDat = createMateriel.serviceDat,
            EndGarantee = createMateriel.endGarantee
        };
        //Apel de la méthode à tester grace au http client:
        var rep = await _httpClient.PostAsJsonAsync<DTOmaterielRequest>("api/materiels/", createMateriel);

        //Lecture de la réponse du contenu de la réponse du serveur(là pour débuguer).
        _logger.WriteLine(await rep.Content.ReadAsStringAsync());

        //ASSERT:
        //Vérification que le status code retourné par le serveur est bien Ok
        Assert.Equal(rep.StatusCode, System.Net.HttpStatusCode.OK);

        //Lecture du contenu du body:
        var actual = await rep.Content.ReadFromJsonAsync<DTOmaterielResponse>();

        //ASSERT:vérification de l'égalité de l'objet attendu et de celui retourné:
        Assert.Equal(expected, actual);
    }





}

