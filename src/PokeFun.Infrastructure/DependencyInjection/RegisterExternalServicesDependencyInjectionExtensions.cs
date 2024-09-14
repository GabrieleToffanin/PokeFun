using Microsoft.Extensions.DependencyInjection;
using PokeFun.Application.Services;
using PokeFun.Infrastructure.ApplicationServices;

namespace PokeFun.Infrastructure.DependencyInjection;
public static class RegisterExternalServicesDependencyInjectionExtensions
{
    public static IServiceCollection RegisterExternalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IExternalPokemonService, ExternalPokemonService>();

        return serviceCollection;
    }
}
