using Microsoft.AspNetCore.Mvc.Testing;
using PokeFun.Api.IntegrationTests.Helpers;
using System.Text.Json;

namespace PokeFun.Api.IntegrationTests.GetPokemon;
public sealed class GetFunTranslatedPokemon : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public GetFunTranslatedPokemon(WebApplicationFactory<Program> factory)
    {
        this._httpClient = factory.CreateClient();
    }

    [Theory(Skip = "Too much config for a simple thing on Actions :(")]
    [InlineData("mewtwo")]
    [Trait("Pokemon", "GET")]
    public async Task WhenValidPokemonNameRequested_ShouldReturnCorrectInformations(string pokemonName)
    {
        //*** Arrange
        string endpoint = $"pokemon/translated/{pokemonName}";

        //*** Act
        var httpResponse = await this._httpClient.GetAsync(endpoint);
        string content = await httpResponse.Content.ReadAsStringAsync();

        PokemonResponseTest result = JsonSerializer.Deserialize<PokemonResponseTest>(content);

        //*** Assert
        Assert.Equal(pokemonName, result.Name);
        Assert.False(result.IsLegendary);
    }
}
