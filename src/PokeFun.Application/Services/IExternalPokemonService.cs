
using PokeFun.Application.Models.Pokemon;

namespace PokeFun.Application.Services;

/// <summary>
/// Port for external PokemonService pokeapi.co
/// </summary>
public interface IExternalPokemonService
{
    ValueTask<ExternalPokemonDto> GetPokemonInfoAsync(string desiredPokemon, CancellationToken cancellationToken);
}
