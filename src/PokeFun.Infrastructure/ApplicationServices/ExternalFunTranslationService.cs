using PokeFun.Application.Services.Abstractions;
using PokeFun.Infrastructure.Request;
using PokeFun.Infrastructure.Response;
using System.Text;
using System.Text.Json;

namespace PokeFun.Infrastructure.ApplicationServices;
public sealed class ExternalFunTranslationService(
    HttpClient httpClient) : IExternalFunTranslationService
{
    private readonly HttpClient _translationClient;

    public async ValueTask<string> TranslatePokemonDescriptionUsingYodaAsync(string pokemonDescription, CancellationToken cancellationToken)
    {
        TranslationRequest request = new() { Text = pokemonDescription };

        string serialized = JsonSerializer.Serialize(request);

        HttpContent httpPostContent = new StringContent(serialized, Encoding.UTF8, "application/json");

        var httpMessageTranslationResult = await httpClient.PostAsync("yoda.json", httpPostContent, cancellationToken);

        string content = await httpMessageTranslationResult.Content.ReadAsStringAsync();

        string result = JsonSerializer.Deserialize<TranslationResponse>(content).Content.Translated;

        return result;
    }

    public async ValueTask<string> TranslatePokemonDescriptionUsingShakespeareAsync(string pokemonDescription, CancellationToken cancellationToken)
    {
        TranslationRequest request = new() { Text = pokemonDescription };

        return pokemonDescription;
    }
}
