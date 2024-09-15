using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Operations;

namespace PokeFun.Application.Services.Abstractions;

public interface IPokemonService
{
    ValueTask<OperationResult<PokemonDto>> GetPokemonAsync(string pokemonName, CancellationToken cancellationToken);

    ValueTask<OperationResult<PokemonDto>> GetFunTranslatedPokemonAsync(string pokemonName, CancellationToken cancellationToken);
}
