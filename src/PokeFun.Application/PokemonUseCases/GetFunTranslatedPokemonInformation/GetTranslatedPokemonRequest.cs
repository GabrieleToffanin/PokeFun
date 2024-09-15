using MediatR;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Operations;

namespace PokeFun.Application.PokemonUseCases.GetFunTranslatedPokemonInformation;

public record GetTranslatedPokemonRequest(string PokemonName) : IRequest<OperationResult<PokemonDto>>;
