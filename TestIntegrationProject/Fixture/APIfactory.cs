using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
namespace TestIntegrationProject.Fixture;
//Classe APIFactory qui créera le serveur de test avec les configuration voulues pour l'environement de test(BDD de tests).
public class APIfactory : WebApplicationFactory<apiMateriels.Program>
{
    public IConfiguration Configuration { get; set; }
    //Réécriture de la configuration du serveur des tests d'intégration:
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {   //nouveau service de configuration du serveur de test avec le fichier : appsettings.Integrations.json.
            Configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.Integrations.json")
                   // Build et ajout de la configuration. 
                   .Build();
            //Possibilité d'ajouter plusieur cnfigurations.
            config.AddConfiguration(Configuration);
        });

    }


}