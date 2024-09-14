using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Services;

namespace PokeFun.Infrastructure.ApplicationServices;
internal sealed class ExternalPokemonService : IExternalPokemonService
{
    public async ValueTask<ExternalPokemonDto> GetPokemonInfoAsync(string desiredPokemon, CancellationToken cancellationToken)
    {
        return default;
    }
}
