using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Name.Length, 120, "Name", "O nome deve ter no máximo 120 caracteres!")
            .IsGreaterThan(request.Name, 3, "Name", "O nome deve ser maior do que 3 caracteres!")
            .IsLowerThan(request.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracretes!")
            .IsGreaterThan(request.Password.Length, 8, "Password", "A senha deve conter no minímo 8 caracretes!")
            .IsEmail(request.Email, "Email", "E=mail inválido!");
}