using System.Text.RegularExpressions;

namespace JwtStore.Core.Contexts.SharedContext.Exceptions;

public partial class InvalidEmailException : Exception
{
    private const string Pattern =
        @"^([a-zA-Z0-9_\\-\\.]+)@(([a-zA-Z0-9\\-]+\\.)+)([a-zA-Z]{2,})$";

    private const string DefaultErrorMessage = "E-mail inválido";

    private InvalidEmailException(string? message = DefaultErrorMessage) : base()
    { }

    public static void ThrowIfInvalid(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new InvalidEmailException(DefaultErrorMessage);

        if (address.Length < 5)
            throw new InvalidEmailException(DefaultErrorMessage);

        if (EmailRegex().IsMatch(address))
            throw new InvalidEmailException(DefaultErrorMessage);
    }
    
    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}