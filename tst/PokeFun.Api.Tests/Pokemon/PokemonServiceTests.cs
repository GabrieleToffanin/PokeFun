using Moq;
using PokeFun.Application.Exceptions;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.PokemonUseCases.GetPokemonInformation;
using PokeFun.Application.Services;

namespace PokeFun.Application.Tests.Pokemon;

/// <summary>
/// Base testing for common operation results.
/// </summary>
public sealed class PokemonServiceTests
{
    [Theory]
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
            .ReturnsAsync(new PokemonDto(pokemonName, pokemonDescription, pokemonHabitat, isLegendary));

        GetPokemonRequestHandler pokemonService = new(externalPokemonService.Object);

        // *** Act
        var pokemon =
            await pokemonService.Handle(request, CancellationToken.None); // Set to none for testing purposes.



        // *** Assert
        Assert.True(pokemon.OperationOutcome is Operations.Outcome.Success);
        Assert.Equal(pokemonName, pokemon.Value.Name);
        Assert.Equal(pokemonDescription, pokemon.Value.Description);
        Assert.Equal(pokemonHabitat, pokemon.Value.Habitat);
        Assert.True(pokemon.Value.IsLegendary);
    }

    [Theory]
    [InlineData("", "TestDescription", "TestHabitat", true)]
    [InlineData(null, "TestDescription", "TestHabitat", true)]
    public async Task WhenRequestedPokemonNameIsNull_ReturnsWrongRequestOperationResult(
        string pokemonName,
        string pokemonDescription,
        string pokemonHabitat,
        bool isLegendary)
    {
        // *** Arrange
        GetPokemonRequest request = new(pokemonName);
        Mock<IExternalPokemonService> externalPokemonService = new();

        externalPokemonService.Setup(x => x.GetPokemonInfoAsync(pokemonName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PokemonDto(pokemonName, pokemonDescription, pokemonHabitat, isLegendary));

        GetPokemonRequestHandler pokemonService = new(externalPokemonService.Object);

        // *** Act
        var pokemon =
            await pokemonService.Handle(request, CancellationToken.None); // Set to none for testing purposes.

        // *** Assert
        Assert.True(pokemon.OperationOutcome == Operations.Outcome.BadRequest);
    }

    [Theory]
    [InlineData("o_O", "TestDescription", "TestHabitat", true)]
    public async Task WhenRequestedPokemonNameNotAPokemonName_ReturnsNotFound(
        string pokemonName,
        string pokemonDescription,
        string pokemonHabitat,
        bool isLegendary)
    {
        // *** Arrange
        GetPokemonRequest request = new(pokemonName);
        Mock<IExternalPokemonService> externalPokemonService = new();

        externalPokemonService.Setup(x => x.GetPokemonInfoAsync(pokemonName, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new PokemonNotFoundException($"Can not find any pokemon with given name {pokemonName}."));

        GetPokemonRequestHandler pokemonService = new(externalPokemonService.Object);

        // *** Act
        var pokemon =
            await pokemonService.Handle(request, CancellationToken.None); // Set to none for testing purposes.

        // *** Assert
        Assert.True(pokemon.OperationOutcome == Operations.Outcome.NotFound);
        Assert.NotNull(pokemon.ErrorMessage);
        Assert.Equal($"Can not find any pokemon with given name {pokemonName}.", pokemon.ErrorMessage);
    }
}
