using PokeFun.Application.Models.Pokemon;

namespace PokeFun.Application.Services.Abstractions;

/// <summary>
/// Port for external PokemonService pokeapi.co
/// </summary>
public interface IExternalPokemonService
{
    ValueTask<PokemonDto> GetPokemonInfoAsync(string desiredPokemon, CancellationToken cancellationToken);
}
