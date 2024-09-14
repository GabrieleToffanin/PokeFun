using Moq;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.PokemonUseCases.GetPokemonInformation;
using PokeFun.Application.Services;

namespace PokeFun.Application.Tests.Pokemon;

public sealed class PokemonServiceTests
{
    [InlineData("ditto", "TestDescription", "TestHabitat", true)]
    public async Task WhenCallingExternalPokemonService_CorrectlyEnrichesDto(
        string pokemonName,
        string pokemonDescription,
        string pokemonHabitat,
        bool isLegendary)
    {
        // *** Arrange
        GetPokemonRequest request = new(pokemonName);
        Mock<IExternalPokemonService> externalPokemonService = new();

        externalPokemonService.Setup(x => x.GetPokemonInfoAsync(pokemonName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ExternalPokemonDto());

        GetPokemonRequestHandler pokemonService = new(externalPokemonService.Object);

        // *** Act
        var pokemon =
            await pokemonService.Handle(request, CancellationToken.None); // Set to none for testing purposes.

        // *** Assert
        Assert.Equal(pokemonName, pokemon.Name);
        Assert.Equal(pokemonDescription, pokemon.Description);
        Assert.Equal(pokemonHabitat, pokemon.Habitat);
        Assert.True(pokemon.IsLegendary);
    }
}
