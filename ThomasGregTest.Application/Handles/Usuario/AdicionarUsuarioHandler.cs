using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;
using ThomasGregTest.Application.Util;

namespace ThomasGregTest.Application;

public class AdicionarUsuarioHandler(IUsuarioRepository repository,
    UserAuthenticator userAuthenticator) : IRequestHandler<AdicionarUsuarioQuery, IEvento>
{
    private readonly IUsuarioRepository _repository = repository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;

    public async Task<IEvento> Handle(AdicionarUsuarioQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            if (String.IsNullOrWhiteSpace(request.Nome))
                return new ResultadoEventos(success, "O campo Nome é obrigatório.");

            if (String.IsNullOrWhiteSpace(request.Email))
                return new ResultadoEventos(success, "O campo Email é obrigatório.");

            if (String.IsNullOrWhiteSpace(request.Senha))
                return new ResultadoEventos(success, "O campo Senha é obrigatório.");

            var existsUser = await _repository.ObterPorEmailAtivo(request.Email, cancellationToken);
            if (existsUser != null)
                return new ResultadoEventos(success, "Jà existe um usuário cadastrado com esse e-mail");

            var usuario = UsuarioMapper<Usuario>.Map(request);
            usuario.Senha = Criptografia.Encrypt(request.Senha);
            var upsertedId = await _repository.Salvar(usuario, cancellationToken);
            success = upsertedId > 0;
          
            return new ResultadoEventos(success, success ? "Usuário cadastrado com sucesso!" : "Falha ao cadastrar o usuário!", null, upsertedId);
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
