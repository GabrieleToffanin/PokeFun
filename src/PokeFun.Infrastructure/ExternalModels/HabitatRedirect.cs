using System.Text.Json.Serialization;

namespace PokeFun.Application.Models.Pokemon;

public class HabitatRedirect
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
}
