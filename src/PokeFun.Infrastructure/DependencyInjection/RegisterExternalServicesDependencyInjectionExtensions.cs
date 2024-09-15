using Microsoft.Extensions.DependencyInjection;
using PokeFun.Application.Services.Abstractions;
using PokeFun.Infrastructure.ApplicationServices;

namespace PokeFun.Infrastructure.DependencyInjection;
public static class RegisterExternalServicesDependencyInjectionExtensions
{
    private const string DefaultPokemonApiUrl = "https://pokeapi.co/api/v2/";
    private const string DefaultTransaltionUrl = "https://api.funtranslations.com/translate/";

    public static IServiceCollection RegisterExternalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IExternalPokemonService, ExternalPokemonService>(
            options =>
            {
                options.BaseAddress = new Uri(DefaultPokemonApiUrl);
            });

        serviceCollection.AddHttpClient<IExternalFunTranslationService, ExternalFunTranslationService>(
            options =>
            {
                options.BaseAddress = new Uri(DefaultTransaltionUrl);
            });

        return serviceCollection;
    }
}
