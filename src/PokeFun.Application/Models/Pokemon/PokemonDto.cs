namespace PokeFun.Application.Models.Pokemon;

public record PokemonDto(
    string Name,
    string Description,
    string Habitat,
    bool IsLegendary);
