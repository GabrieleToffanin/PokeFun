using System.Text.Json.Serialization;

namespace PokeFun.Infrastructure.Response;
public class TranslationResponse
{
    [JsonPropertyName("success")]
    public Success Success { get; set; }
    [JsonPropertyName("contents")]
    public Contents Content { get; set; }
}

public class Success
{
    [JsonPropertyName("total")]
    public int Total { get; set; }
}

public class Contents
{
    [JsonPropertyName("translated")]
    public string Translated { get; set; }
}
