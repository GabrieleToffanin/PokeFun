using System.Text.Json.Serialization;

namespace PokeFun.Api.IntegrationTests.Helpers;
public record PokemonResponseTest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("islegendary")]
    public bool IsLegendary { get; set; }
}
