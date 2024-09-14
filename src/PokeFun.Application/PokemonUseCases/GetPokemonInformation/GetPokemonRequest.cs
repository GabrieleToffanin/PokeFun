using MediatR;
using PokeFun.Application.Models.Pokemon;

namespace PokeFun.Application.PokemonUseCases.GetPokemonInformation;

public record GetPokemonRequest(string PokemonName) : IRequest<PokemonDto>;
