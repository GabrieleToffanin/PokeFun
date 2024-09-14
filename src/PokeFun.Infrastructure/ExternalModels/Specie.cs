using System.Text.Json.Serialization;

namespace PokeFun.Application.Models.Pokemon;

public class Specie
{
    [JsonPropertyName("habitat")]
    public HabitatRedirect Habitat { get; set; }
    [JsonPropertyName("is_legendary")]
    public bool is_legendary { get; set; }

}
