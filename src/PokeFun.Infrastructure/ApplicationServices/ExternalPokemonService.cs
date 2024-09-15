using PokeFun.Application.Exceptions;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Services;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace PokeFun.Infrastructure.ApplicationServices;
internal sealed class ExternalPokemonService(HttpClient client)
    : IExternalPokemonService
{
    private readonly HttpClient _pokemonApiHttpClient = client;

    public async ValueTask<PokemonDto> GetPokemonInfoAsync(string desiredPokemon, CancellationToken cancellationToken)
    {
        var httpMessage = await this._pokemonApiHttpClient.GetAsync($"pokemon/{desiredPokemon}", cancellationToken);

        PokemonNotFoundException.ThrowIfNotFound(desiredPokemon, httpMessage.StatusCode);

        string response = await httpMessage.Content.ReadAsStringAsync();

        ExternalPokemonDto pokemonDto = JsonSerializer.Deserialize<ExternalPokemonDto>(response);

        if (!this.TryGetSpecieResourceId(pokemonDto, out int specieId))
        {
            //Decide
        }

        var specieHttpMessage = await this._pokemonApiHttpClient.GetAsync($"pokemon-species/{specieId}", cancellationToken); // This call would be good to be cached.

        string specieResponse = await specieHttpMessage.Content.ReadAsStringAsync();

        Specie specie = JsonSerializer.Deserialize<Specie>(specieResponse);

        PokemonDto pokemon = new(pokemonDto.Name, specie.Description.FirstOrDefault().FlavorDescription, specie.Habitat.Name, specie.IsLegendary);

        return pokemon;
    }

    private bool TryGetSpecieResourceId(ExternalPokemonDto externalPokemonDto, [NotNullWhen(true)] out int specieResourceId)
    {
        string trimmedUrl = externalPokemonDto.Specie.RedirectUrl.TrimEnd('/');
        string resourceId = trimmedUrl.Substring(trimmedUrl.LastIndexOf('/') + 1); // It should get just the int we need for the specie call

        if (!int.TryParse(resourceId, out specieResourceId))
        {
            return false;
        }

        return true;
    }
}

