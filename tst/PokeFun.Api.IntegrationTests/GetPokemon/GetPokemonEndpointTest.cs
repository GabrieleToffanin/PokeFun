using Microsoft.AspNetCore.Mvc.Testing;
using PokeFun.Api.IntegrationTests.Helpers;
using System.Text.Json;

namespace PokeFun.Api.IntegrationTests.GetPokemon;

public sealed class GetPokemonEndpointTest
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public GetPokemonEndpointTest(WebApplicationFactory<Program> factory)
    {
        this._httpClient = factory.CreateClient();
    }

    [Theory(Skip = "Too much config for a simple thing on Actions :(")]
    [InlineData("ditto")]
    [Trait("Pokemon", "GET")]
    public async Task WhenValidPokemonNameRequested_ShouldReturnCorrectInformations(string pokemonName)
    {
        //*** Arrange
        string endpoint = $"pokemon/{pokemonName}";

        //*** Act
        var httpResponse = await this._httpClient.GetAsync(endpoint);
        string content = await httpResponse.Content.ReadAsStringAsync();

        PokemonResponseTest result = JsonSerializer.Deserialize<PokemonResponseTest>(content);

        //*** Assert
        Assert.Equal(pokemonName, result.Name);
        Assert.False(result.IsLegendary); // As far as Ditto won't become Legendary :D
    }

    [Theory(Skip = "Too much config for a simple thing on Actions :(")]
    [InlineData("o_O")]
    [Trait("Pokemon", "GET")]
    public async Task WhenValidPokemonNameRequested_ButNotExisting_ShouldReturnNotFound(string pokemonName)
    {
        //*** Arrange
        string endpoint = $"pokemon/{pokemonName}";

        //*** Act
        var httpResponse = await this._httpClient.GetAsync(endpoint);

        //*** Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, httpResponse.StatusCode);
    }

    [Theory(Skip = "Too much config for a simple thing on Actions :(")]
    [InlineData("123123")]
    [Trait("Pokemon", "GET")]
    public async Task WhenUnvalidPokemonNameRequested_ShouldReturnBadRequest(string pokemonName)
    {
        //*** Arrange
        string endpoint = $"pokemon/{pokemonName}";

        //*** Act
        var httpResponse = await this._httpClient.GetAsync(endpoint);

        //*** Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }
}
