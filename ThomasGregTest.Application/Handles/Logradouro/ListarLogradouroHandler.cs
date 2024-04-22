using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;

namespace ThomasGregTest.Application;

public class ListarLogradouroHandler(ILogradouroRepository logradouroeRepository,
                                    UserAuthenticator userAuthenticator,
                                    IUsuarioRepository usuarioRepository) : IRequestHandler<ListarLogradouroQuery, IEvento>
{
    private readonly ILogradouroRepository _logradouroeRepository = logradouroeRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<IEvento> Handle(ListarLogradouroQuery request, CancellationToken cancellationToken)
    {
        _ = new List<LogradouroResponse>();
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(false, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(false, "Acesso expirado");

            var lista = await _logradouroeRepository.ListarLogradouros(request.ClienteId, cancellationToken);
            List<LogradouroResponse>? response = LogradouroMapper<List<LogradouroResponse>>.Map(lista);

            return new ResultadoEventos(true, response, lista.Count());
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(false, ex.Message);
        }
    }
}
