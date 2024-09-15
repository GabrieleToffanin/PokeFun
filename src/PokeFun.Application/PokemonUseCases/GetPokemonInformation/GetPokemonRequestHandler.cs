using MediatR;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Services.Abstractions;

namespace PokeFun.Application.PokemonUseCases.GetPokemonInformation;
public sealed class GetPokemonRequestHandler(IExternalPokemonService externalPokemonService)
    : IRequestHandler<GetPokemonRequest, PokemonDto>
{
    private readonly IExternalPokemonService _externalPokemonService = externalPokemonService;

    public async Task<PokemonDto> Handle(GetPokemonRequest request, CancellationToken cancellationToken)
    {
        PokemonDto result = await this._externalPokemonService.GetPokemonInfoAsync(request.PokemonName, cancellationToken);

        return result;
    }
}
