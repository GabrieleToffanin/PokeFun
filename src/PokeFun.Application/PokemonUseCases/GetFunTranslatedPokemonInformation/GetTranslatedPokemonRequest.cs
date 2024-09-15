using MediatR;
using PokeFun.Application.Models.Pokemon;

namespace PokeFun.Application.PokemonUseCases.GetFunTranslatedPokemonInformation;

public record GetTranslatedPokemonRequest(PokemonDto PokemonDto) : IRequest<PokemonDto>;
