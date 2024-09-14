using System.Text.Json.Serialization;

namespace PokeFun.Application.Models.Pokemon;

public record ExternalPokemonDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("species")]
    public SpecieRedirect Specie { get; set; }
}
