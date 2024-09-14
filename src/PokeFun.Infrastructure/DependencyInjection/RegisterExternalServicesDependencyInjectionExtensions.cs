using Microsoft.Extensions.DependencyInjection;
using PokeFun.Application.Services;
using PokeFun.Infrastructure.ApplicationServices;

namespace PokeFun.Infrastructure.DependencyInjection;
public static class RegisterExternalServicesDependencyInjectionExtensions
{
    private const string DefaultPokemonApiUrl = "https://pokeapi.co/api/v2/pokemon/";

    public static IServiceCollection RegisterExternalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IExternalPokemonService, ExternalPokemonService>(
            options =>
            {
                options.BaseAddress = new Uri(DefaultPokemonApiUrl);
            });

        return serviceCollection;
    }
}
