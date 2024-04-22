using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;

namespace ThomasGregTest.Application;

public class SelecionarUsuarioHandler(IUsuarioRepository usuarioRepository,
    UserAuthenticator userAuthenticator) : IRequestHandler<SelecionarUsuarioQuery, IEvento>
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;

    public async Task<IEvento> Handle(SelecionarUsuarioQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuario = await _usuarioRepository.ObterPorId(request.Id, cancellationToken);
     
            if (usuario == null)
                return new ResultadoEventos(success, "Usuario não existe na base de dados.");

            return new ResultadoEventos(true, usuario);
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
