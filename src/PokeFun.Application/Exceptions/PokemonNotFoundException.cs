using System.Net;
using System.Runtime.CompilerServices;

namespace PokeFun.Application.Exceptions;
public class PokemonNotFoundException : Exception
{
    public PokemonNotFoundException()
    {

    }

    public PokemonNotFoundException(string message) : base(message)
    {

    }

    public PokemonNotFoundException(string message, Exception innerException) : base(message, innerException)
    {

    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowIfNotFound(
        string pokemonName,
        HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.NotFound)
            throw new PokemonNotFoundException($"Can not find any pokemon with given name {pokemonName}.");
    }

}
