using Moq;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.PokemonUseCases.GetFunTranslatedPokemonInformation;
using PokeFun.Application.Services.Abstractions;

namespace PokeFun.Application.Tests.Pokemon;

public sealed class PokemonTransaltedServiceTest
{
    [Theory]
    [InlineData("ditto", "It is a test description", "TestHabitat", true)]
    public async Task WhenCallingExternalPokemonServiceWithTranslation_CorrectlyEnrichesDtoAndTranslatesYoda(
        string pokemonName,
        string pokemonDescription,
        string pokemonHabitat,
        bool isLegendary)
    {
        // *** Arrange
        PokemonDto expectedPokemonDto = new(pokemonName, pokemonDescription, pokemonHabitat, isLegendary);

        GetTranslatedPokemonRequest request = new(expectedPokemonDto);
        Mock<IExternalFunTranslationService> externalTranslationService = new();

        string expectedPokemonDescription = "Test description, it is.";
        externalTranslationService.Setup(x => x.TranslatePokemonDescriptionUsingYodaAsync(pokemonDescription, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPokemonDescription);

        GetFunTranslatedPokemonRequestHandler translatedPokemonService = new(externalTranslationService.Object);

        // *** Act
        var pokemon =
            await translatedPokemonService.Handle(request, CancellationToken.None); // Set to none for testing purposes.

        // *** Assert
        Assert.Equal(pokemonName, pokemon.Name);
        Assert.Equal(expectedPokemonDescription, pokemon.Description);
        Assert.Equal(pokemonHabitat, pokemon.Habitat);
        Assert.True(pokemon.IsLegendary);
    }
}
