using BLL.implémentations;
using BLL.interfaces;
using BLL.security;
using BLL.services;
using DAL;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestUnitaire")]
namespace BLL;

public class BLLOptions
{
    //Here you can add your custom options
}

public static class BLLExtension
{

    public static IServiceCollection AddBLL(this IServiceCollection services, Action<BLLOptions> configure = null)
    {
        BLLOptions options = new();
        configure?.Invoke(options);


        //Enregistrement des correspondances des dépendances  pour l'injecteur de dépendances:
        services.AddTransient<IsecurityService, SecurityService>();
        services.AddTransient<IGestMaterielsService, GestMaterielService>();
        services.AddTransient<IusersService, userService>();

        //Register DAL services
        services.AddDAL();

        return services;
    }
}