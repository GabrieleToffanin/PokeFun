
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Operations;
using PokeFun.Application.PokemonUseCases.GetPokemonInformation;
using System.Net;

namespace PokeFun.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController(IMediator mediator)
    : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [Route("{pokemonName}")]
    [ProducesResponseType(typeof(PokemonDto), statusCode: (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPokemon(
        [FromRoute] string pokemonName,
        CancellationToken cancellationToken)
    {
        GetPokemonRequest request = new(pokemonName);

        var result = await this._mediator.Send(request, cancellationToken);

        if (result.OperationOutcome is Outcome.BadRequest)
            return this.BadRequest("Provided pokemon name must not be Null or Empty");

        if (result.OperationOutcome is Outcome.NotFound)
            return this.NotFound(result.ErrorMessage);

        return this.Ok(result.Value);
    }

    [HttpGet]
    [Route("[action]/{pokemonName}")]
    [ProducesResponseType(typeof(PokemonDto), statusCode: (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), statusCode: (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Translated(
        [FromRoute] string pokemonName,
        CancellationToken cancellationToken)
    {
        return this.Ok();
    }
}
