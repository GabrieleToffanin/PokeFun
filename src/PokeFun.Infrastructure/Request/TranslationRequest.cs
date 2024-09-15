using System.Text.Json.Serialization;

namespace PokeFun.Infrastructure.Request;

public class TranslationRequest
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}
