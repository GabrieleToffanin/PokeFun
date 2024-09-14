using System.Text.Json.Serialization;

namespace PokeFun.Application.Models.Pokemon;

public record SpecieRedirect
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("url")]
    public string RedirectUrl { get; set; }
}


