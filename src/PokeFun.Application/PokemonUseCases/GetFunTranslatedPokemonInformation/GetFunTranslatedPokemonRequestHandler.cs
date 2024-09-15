using MediatR;
using PokeFun.Application.Models.Pokemon;
using PokeFun.Application.Services.Abstractions;
using System.Runtime.CompilerServices;

namespace PokeFun.Application.PokemonUseCases.GetFunTranslatedPokemonInformation;

public sealed class GetFunTranslatedPokemonRequestHandler(
    IExternalFunTranslationService translationService) : IRequestHandler<GetTranslatedPokemonRequest, PokemonDto>
{
    private const string YodaRequiredHabitat = "cave";
    private readonly IExternalFunTranslationService _externaltranslationService = translationService;

    public async Task<PokemonDto> Handle(GetTranslatedPokemonRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.PokemonDto.Habitat))
            return request.PokemonDto;

        // if pokemon habitat is cave or isLegendary then do Yoda
        if (this.IsForYodaTranslation(request.PokemonDto))
        {
            return request.PokemonDto with { Description = await this._externaltranslationService.TranslatePokemonDescriptionUsingYodaAsync(request.PokemonDto.Description, cancellationToken) };
        }

        return request.PokemonDto with { Description = await this._externaltranslationService.TranslatePokemonDescriptionUsingShakespeareAsync(request.PokemonDto.Description, cancellationToken) };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsForYodaTranslation(PokemonDto pokemonDto)
        => pokemonDto is { Habitat: YodaRequiredHabitat } or { IsLegendary: true };
}
