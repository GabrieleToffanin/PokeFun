
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokeFun.Application.Models.Pokemon;
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
    public async Task<IActionResult> GetPokemon(
        [FromRoute] string pokemonName)
    {
        return Ok();
    }
}
