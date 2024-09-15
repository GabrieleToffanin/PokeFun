using Moq;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.PokemonUseCases.GetFunTranslatedPokemonInformation;
using PokeFun.Application.Services;

namespace PokeFun.Application.Tests.Pokemon;

public sealed class PokemonTransaltedServiceTest
{
    [Theory]
    [InlineData("ditto", "It is a test description", "TestHabitat", true)]
    public async Task WhenCallingExternalPokemonServiceWithTranslation_CorrectlyEnrichesDtoAndTranslates(
        string pokemonName,
        string pokemonDescription,
        string pokemonHabitat,
        bool isLegendary)
    {
        // *** Arrange
        GetTranslatedPokemonRequest request = new(pokemonName);
        Mock<IExternalPokemonService> externalPokemonService = new();
        Mock<IExternalFunTranslationService> externalTranslationService = new();

        externalPokemonService.Setup(x => x.GetPokemonInfoAsync(pokemonName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PokemonDto(pokemonName, pokemonDescription, pokemonHabitat, isLegendary));

        string expectedPokemonDescription = "Test description, it is.";
        externalTranslationService.Setup(x => x.TranslatePokemonDescriptionAsync(pokemonDescription, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPokemonDescription);

        GetFunTranslatedPokemonRequestHandler translatedPokemonService = new(externalPokemonService.Object, externalTranslationService.Object);

        // *** Act
        var pokemon =
            await translatedPokemonService.Handle(request, CancellationToken.None); // Set to none for testing purposes.

        // *** Assert
        Assert.True(pokemon.OperationOutcome is Operations.Outcome.Success);
        Assert.Equal(pokemonName, pokemon.Value.Name);
        Assert.Equal(pokemonDescription, pokemon.Value.Description);
        Assert.Equal(pokemonHabitat, pokemon.Value.Habitat);
        Assert.True(pokemon.Value.IsLegendary);
    }
}
