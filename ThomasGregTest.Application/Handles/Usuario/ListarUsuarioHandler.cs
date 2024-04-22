using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;

namespace ThomasGregTest.Application.Handlers
{
    public class ListarUsuarioHandler(IUsuarioRepository repository,
        UserAuthenticator userAuthenticator) : IRequestHandler<ListarUsuarioQuery, IEvento>
    {
        private readonly IUsuarioRepository _repository = repository;
        private readonly UserAuthenticator _userAuthenticator = userAuthenticator;

        public async Task<IEvento> Handle(ListarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var response = new List<UsuarioResponse>();
            try
            {
                if (_userAuthenticator.Email == null)
                    return new ResultadoEventos(false, "Acesso expirado");

                var usuarioLogado = await _repository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultadoEventos(false, "Acesso expirado");

                var lista = await _repository.ListarUsuarios(cancellationToken);
                response = UsuarioMapper<List<UsuarioResponse>>.Map(lista);

                return new ResultadoEventos(true, response, lista.Count());
            }
            catch (Exception ex)
            {
                return new ResultadoEventos(false, ex.Message);
            }
        }
    }
}
