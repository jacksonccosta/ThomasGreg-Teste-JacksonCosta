using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;
using ThomasGregTest.Application.Util;

namespace ThomasGregTest.Application;

public class AtualizarUsuarioHandler(IUsuarioRepository repository,
    UserAuthenticator userAuthenticator) : IRequestHandler<AtualizarUsuarioQuery, IEvento>
{
    private readonly IUsuarioRepository _repository = repository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;

    public async Task<IEvento> Handle(AtualizarUsuarioQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            IUsuario usuarioLogado = await _repository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuario = await _repository.ObterPorId(request.Id, cancellationToken);
            if (usuario == null)
                return new ResultadoEventos(success, "Não foi possível localizar o usuário.");

            var existsUser = await _repository.ObterPorEmailAtivo(request.Email, cancellationToken);
            if (existsUser != null && existsUser.Id != usuario.Id)
                return new ResultadoEventos(success, "Jà existe um usuário cadastrado com esse e-mail");

            usuario.Nome = request.Nome;
            usuario.Email = request.Email;
            usuario.Ativo = request.Ativo;

            if (!string.IsNullOrEmpty(request.Senha))
                usuario.Senha = Criptografia.Encrypt(request.Senha);

            var usuarioMap = UsuarioMapper<Usuario>.Map(usuario);
            var result = await _repository.Salvar(usuarioMap, cancellationToken);
            success = result > 0;

            return new ResultadoEventos(success, success ? "Usuário atualizado com sucesso!" : "Nenhuma atualização realizada.");
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
