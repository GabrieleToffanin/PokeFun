using PokeFun.Application.Exceptions;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Services;
using System.Text.Json;

namespace PokeFun.Infrastructure.ApplicationServices;
internal sealed class ExternalPokemonService(HttpClient client)
    : IExternalPokemonService
{
    private readonly HttpClient _pokemonApiHttpClient = client;

    public async ValueTask<PokemonDto> GetPokemonInfoAsync(string desiredPokemon, CancellationToken cancellationToken)
    {
        var httpMessage = await this._pokemonApiHttpClient.GetAsync(desiredPokemon, cancellationToken);

        PokemonNotFoundException.ThrowIfNotFound(desiredPokemon, httpMessage.StatusCode);

        string response = await httpMessage.Content.ReadAsStringAsync();

        ExternalPokemonDto pokemonDto = JsonSerializer.Deserialize<ExternalPokemonDto>(response);

        // Compose the api calls chain an create complete object
        PokemonDto pokemon = new(pokemonDto.Name, string.Empty, string.Empty, default);

        return pokemon;
    }
}
