
namespace PokeFun.Application.Services;

public interface IExternalFunTranslationService
{
    ValueTask<string> TranslatePokemonDescriptionAsync(string pokemonDescription, CancellationToken cancellationToken);
}
