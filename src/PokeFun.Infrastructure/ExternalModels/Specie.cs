using System.Text.Json.Serialization;

namespace PokeFun.Application.Models.Pokemon;

public class Specie
{
    [JsonPropertyName("habitat")]
    public HabitatRedirect Habitat { get; set; }
    [JsonPropertyName("is_legendary")]
    public bool IsLegendary { get; set; }
    [JsonPropertyName("flavor_text_entries")]
    public Description[] Description { get; set; }
}

public record Description
{
    [JsonPropertyName("flavor_text")]
    public string FlavorDescription { get; set; }
    [JsonPropertyName("language")]
    public Language Language { get; set; }
}


public record Language
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}