using MediatR;
using PokeFun.Application.Exceptions;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Operations;
using PokeFun.Application.PokemonUseCases.GetFunTranslatedPokemonInformation;
using PokeFun.Application.PokemonUseCases.GetPokemonInformation;
using PokeFun.Application.Services.Abstractions;
using System.Runtime.CompilerServices;

namespace PokeFun.Application.Services;

public sealed class PokemonService(IMediator mediator) : IPokemonService
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask<OperationResult<PokemonDto>> GetPokemonAsync(string pokemonName, CancellationToken cancellationToken)
    {
        try
        {
            if (!this.IsValidPokemonName(pokemonName))
                return OperationResult<PokemonDto>.CreateResult(null!, Outcome.BadRequest);

            GetPokemonRequest request = new(pokemonName);

            var result = await this._mediator.Send(request, cancellationToken);

            return OperationResult<PokemonDto>.CreateResult(result, Outcome.Success);

        }
        catch (PokemonNotFoundException e)
        {
            return OperationResult<PokemonDto>.CreateResult(null!, Outcome.NotFound, e.Message);
        }
    }

    public async ValueTask<OperationResult<PokemonDto>> GetFunTranslatedPokemonAsync(string pokemonName, CancellationToken cancellationToken)
    {
        try
        {
            if (!this.IsValidPokemonName(pokemonName))
                return OperationResult<PokemonDto>.CreateResult(null!, Outcome.BadRequest);

            GetPokemonRequest request = new(pokemonName);

            var result = await this._mediator.Send(request, cancellationToken);

            GetTranslatedPokemonRequest translationRequest = new(result);

            var translationResult = await this._mediator.Send(translationRequest, cancellationToken);

            return OperationResult<PokemonDto>.CreateResult(translationResult, Outcome.Success);

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
