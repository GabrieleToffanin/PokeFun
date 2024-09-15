using MediatR;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Operations;
using PokeFun.Application.Services;

namespace PokeFun.Application.PokemonUseCases.GetFunTranslatedPokemonInformation;

public sealed class GetFunTranslatedPokemonRequestHandler(
    IExternalPokemonService pokemonService,
    IExternalFunTranslationService translationService) : IRequestHandler<GetTranslatedPokemonRequest, OperationResult<PokemonDto>>
{
    public async Task<OperationResult<PokemonDto>> Handle(GetTranslatedPokemonRequest request, CancellationToken cancellationToken)
    {
        return default;
    }
}
