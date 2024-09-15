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

        httpMessageTranslationResult.EnsureSuccessStatusCode();

        string content = await httpMessageTranslationResult.Content.ReadAsStringAsync();

        TranslationResponse result = JsonSerializer.Deserialize<TranslationResponse>(content);

        if (result.Success.Total <= 0) // If for some reason the service was not capable of translating the resource.
            return pokemonDescription;

        return result.Content.Translated;
    }

    public async ValueTask<string> TranslatePokemonDescriptionUsingShakespeareAsync(string pokemonDescription, CancellationToken cancellationToken)
    {
        TranslationRequest request = new() { Text = pokemonDescription };

        string serialized = JsonSerializer.Serialize(request);

        HttpContent httpPostContent = new StringContent(serialized, Encoding.UTF8, "application/json");

        var httpMessageTranslationResult = await httpClient.PostAsync("shakespeare.json", httpPostContent, cancellationToken);

        httpMessageTranslationResult.EnsureSuccessStatusCode();

        string content = await httpMessageTranslationResult.Content.ReadAsStringAsync();

        TranslationResponse result = JsonSerializer.Deserialize<TranslationResponse>(content);

        if (result.Success.Total <= 0) // If for some reason the service was not capable of translating the resource, or rate limit kicked in.
            return pokemonDescription;

        return result.Content.Translated;
    }
}
