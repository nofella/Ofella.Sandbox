using System.Diagnostics.CodeAnalysis;

namespace Ofella.Sandbox.FunctionalProgramming;

public readonly struct Result<TValue>
    where TValue : class
{
    private readonly TValue? _value;
    private readonly Error? _error;

    internal Result(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);

        _value = value;
        _error = null;
    }

    internal Result(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        _value = null;
        _error = error;
    }

    public bool TryConsume([NotNullWhen(true)] out TValue? value, [NotNullWhen(false)] out Error? error)
        // or TryCatch or Try or TryGetValue or HasFailed and so on...
    {
        value = _value;
        error = _error;

        return _value is not null;
    }

    public static Result<TValue> Ok(TValue value) => new(value);
}

public readonly record struct Error(string Id, string Message)
{
    public Result<TValue> AsResult<TValue>() where TValue : class => new(this);
};