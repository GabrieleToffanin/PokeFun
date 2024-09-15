namespace PokeFun.Application.Services.Abstractions;

public interface IExternalFunTranslationService
{
    ValueTask<string> TranslatePokemonDescriptionUsingYodaAsync(string pokemonDescription, CancellationToken cancellationToken);

    ValueTask<string> TranslatePokemonDescriptionUsingShakespeareAsync(string pokemonDescription, CancellationToken cancellationToken);
}
