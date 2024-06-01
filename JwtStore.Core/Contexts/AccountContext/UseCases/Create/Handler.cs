using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(
        Request request,
        CancellationToken cancellationToken)
    {
        #region Validação de requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição!", 500);
        }

        #endregion

        #region Gerar os Objetos

        User user;
        try
        {
            var email = new Email(request.Email);
            var password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception e)
        {
            return new Response(e.Message, 400);
        }

        #endregion

        #region Verificar se o usuário existe no banco

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Este E-mail já esta em uso!", 400);
        }
        catch
        {
            return new Response("Falha ao verificar o E-mail cadastrado!", 500);
        }

        #endregion

        #region Persiste os Dados

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falhar ao salvar os dados!", 500);
        }

        #endregion

        #region Enviar Email de ativação

        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
        }

        #endregion


        return new Response("Conta criada com sucesso!",
            new ResponseData(user.Id, user.Name, user.Email));
    }
}