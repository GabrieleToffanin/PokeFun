using MediatR;
using Moq;
using PokeFun.Application.Exceptions;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.PokemonUseCases.GetPokemonInformation;
using PokeFun.Application.Services;
using PokeFun.Application.Services.Abstractions;

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
        Assert.Equal(pokemonName, pokemon.Name);
        Assert.Equal(pokemonDescription, pokemon.Description);
        Assert.Equal(pokemonHabitat, pokemon.Habitat);
        Assert.True(pokemon.IsLegendary);
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

        Mock<IMediator> externalPokemonService = new();

        externalPokemonService.Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PokemonDto(pokemonName, pokemonDescription, pokemonHabitat, isLegendary));

        PokemonService pokemonService = new(externalPokemonService.Object);

        // *** Act
        var pokemon =
            await pokemonService.GetPokemonAsync(pokemonName, CancellationToken.None); // Set to none for testing purposes.

        // *** Assert
        Assert.True(pokemon.OperationOutcome == Operations.Outcome.BadRequest);
    }

    [Theory]
    [InlineData("bhobho", "TestDescription", "TestHabitat", true)]
    public async Task WhenRequestedPokemonNameNotAPokemonName_ReturnsNotFound(
        string pokemonName,
        string pokemonDescription,
        string pokemonHabitat,
        bool isLegendary)
    {
        GetPokemonRequest request = new(pokemonName);

        Mock<IMediator> externalPokemonService = new();

        externalPokemonService.Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new PokemonNotFoundException($"Can not find any pokemon with given name {pokemonName}."));

        PokemonService pokemonService = new(externalPokemonService.Object);

        // *** Act
        var pokemon =
            await pokemonService.GetPokemonAsync(pokemonName, CancellationToken.None); // Set to none for testing purposes.

        // *** Assert
        Assert.True(pokemon.OperationOutcome == Operations.Outcome.NotFound);
        Assert.NotNull(pokemon.ErrorMessage);
        Assert.Equal($"Can not find any pokemon with given name {pokemonName}.", pokemon.ErrorMessage);
    }
}
