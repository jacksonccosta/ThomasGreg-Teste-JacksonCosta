using ThomasGregTest.Core;
using MediatR;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Application;

public class SelecionarLogradouroHandler(ILogradouroRepository logradouroRepository,
                                        UserAuthenticator userAuthenticator,
                                        IUsuarioRepository usuarioRepository) : IRequestHandler<SelecionarLogradouroQuery, IEvento>
{
    private readonly ILogradouroRepository _logradouroRepository = logradouroRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<IEvento> Handle(SelecionarLogradouroQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var logradouro = await _logradouroRepository.ObterPorId(request.Id,request.ClienteId, cancellationToken);
            var logradouroMap = LogradouroMapper<LogradouroResponse>.Map(logradouro);
            if (logradouro == null)
                return new ResultadoEventos(success, "Logradouro não localizado.");

            return new ResultadoEventos(true, logradouroMap);
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
