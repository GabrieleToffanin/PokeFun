using MediatR;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Services;

namespace PokeFun.Application.PokemonUseCases.GetPokemonInformation;
public sealed class GetPokemonRequestHandler(IExternalPokemonService externalPokemonService)
    : IRequestHandler<GetPokemonRequest, PokemonDto>
{
    private readonly IExternalPokemonService _externalPokemonService = externalPokemonService;

    public async Task<PokemonDto> Handle(GetPokemonRequest request, CancellationToken cancellationToken)
    {
        // TODO: implement HttpClient chain calls.
        return default;
    }
}
