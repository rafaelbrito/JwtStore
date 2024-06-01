using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracretes!")
            .IsGreaterThan(request.Password.Length, 8, "Password", "A senha deve conter no minímo 8 caracretes!")
            .IsEmail(request.Email, "Email", "E=mail inválido!");
}