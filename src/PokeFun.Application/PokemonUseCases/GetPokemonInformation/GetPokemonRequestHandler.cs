using MediatR;
using PokeFun.Application.Exceptions;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Operations;
using PokeFun.Application.Services;
using System.Runtime.CompilerServices;

namespace PokeFun.Application.PokemonUseCases.GetPokemonInformation;
public sealed class GetPokemonRequestHandler(IExternalPokemonService externalPokemonService)
    : IRequestHandler<GetPokemonRequest, OperationResult<PokemonDto>>
{
    private readonly IExternalPokemonService _externalPokemonService = externalPokemonService;

    public async Task<OperationResult<PokemonDto>> Handle(GetPokemonRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (!this.IsValidPokemonName(request.PokemonName))
                return OperationResult<PokemonDto>.CreateResult(null!, Outcome.BadRequest);

            PokemonDto result = await this._externalPokemonService.GetPokemonInfoAsync(request.PokemonName, cancellationToken);

            return OperationResult<PokemonDto>.CreateResult(result, Outcome.Success);
        }
        catch (PokemonNotFoundException e)
        {
            return OperationResult<PokemonDto>.CreateResult(null!, Outcome.NotFound, e.Message);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsValidPokemonName(string pokemonName)
        => !string.IsNullOrEmpty(pokemonName) && pokemonName.All(char.IsLetter);
}
