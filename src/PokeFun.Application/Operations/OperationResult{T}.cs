using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace PokeFun.Application.Operations;

public sealed class OperationResult<T>
    where T : notnull
{
    public T Value { get; set; }

    public Outcome OperationOutcome { get; set; }

    public string? ErrorMessage { get; set; }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static OperationResult<T> CreateResult(
        T value,
        Outcome outcome,
        string errorMessage = null)
        => new OperationResult<T>()
        {
            Value = value,
            OperationOutcome = outcome,
            ErrorMessage = errorMessage
        };
}
