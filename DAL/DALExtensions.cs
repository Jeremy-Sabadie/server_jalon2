using DAL.Sessions.Implementations;
using DAL.Sessions.Interfaces;
using DAL.UOW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
//Psibilité d'accéder au fichier TestIntegrationProject malgré le fait qu'il soit internal.
[assembly: InternalsVisibleTo("TestIntegrationProject")]
namespace DAL;
//Classe dédié à la configuration de la DAL du projet.
public class DALOptions
{

    public string DBConnectionString { get; set; }
}

public static class DALExtension
{

    public static IServiceCollection AddDAL(this IServiceCollection services, Action<DALOptions> configure = null)
    {
        DALOptions options = new();

        configure?.Invoke(options);


        //Une connection MariaDB est attribué pour l'nterfaceIDBSession pour son injection lors de sa demande à l'injecteur de dépendances.
        services.AddScoped<IDBSession, DBSessionMariaDB>((services) =>
        {//La chaîne de connection pour établir la connection est récupéré dans le fichier de configuration dédié à la valeur correspondante à la clef:DBConnectionStrings/
            options.DBConnectionString = services.GetRequiredService<IConfiguration>().GetSection("DBConnectionStrings").Value;
            //La connection créée est retournée
            return new DBSessionMariaDB(options.DBConnectionString);

        });

        services.AddTransient<IUOW, UnitOfWork>();

        return services;
    }
}