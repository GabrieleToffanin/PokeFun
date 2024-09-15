using MediatR;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Operations;

namespace PokeFun.Application.PokemonUseCases.GetPokemonInformation;

public record GetPokemonRequest(string PokemonName) : IRequest<OperationResult<PokemonDto>>;
