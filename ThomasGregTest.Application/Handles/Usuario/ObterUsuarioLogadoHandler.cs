using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;

namespace ThomasGregTest.Application.Handlers
{
    public class ObterUsuarioLogadoHandler(IUsuarioRepository usuarioRepository,
        UserAuthenticator userAuthenticator) : IRequestHandler<ObterUsuarioLogadoQuery, IEvento>
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly UserAuthenticator _userAuthenticator = userAuthenticator;

        public async Task<IEvento> Handle(ObterUsuarioLogadoQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                if (_userAuthenticator.Email == null)
                    return new ResultadoEventos(success, "Acesso expirado");

                var usuario = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
                if (usuario == null)
                    return new ResultadoEventos(success, "Acesso expirado");

                var usuarioMap = UsuarioMapper<UsuarioLogadoResponse>.Map(usuario);
                return new ResultadoEventos(true, usuarioMap);
            }
            catch (Exception ex)
            {
                return new ResultadoEventos(success, ex.Message);
            }
        }
    }
}
