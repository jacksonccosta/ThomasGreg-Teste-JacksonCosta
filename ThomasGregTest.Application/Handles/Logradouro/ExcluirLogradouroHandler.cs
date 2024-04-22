using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;

namespace ThomasGregTest.Application;

public class ExcluirLogradouroHandler(ILogradouroRepository logradouroRepository,
                                    UserAuthenticator userAuthenticator,
                                    IUsuarioRepository usuarioRepository) : IRequestHandler<ExcluirLogradouroQuery, IEvento>
{
    private readonly ILogradouroRepository _logradouroRepository = logradouroRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<IEvento> Handle(ExcluirLogradouroQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var verificacao = await _logradouroRepository.ObterPorId(request.Id, request.ClienteId, cancellationToken);
            if (verificacao == null)
                return new ResultadoEventos(success, "Logradouro não localizado.");

            success = await _logradouroRepository.ExcluirPorId(request.Id, request.ClienteId, cancellationToken);

            return new ResultadoEventos(success, success ? "Logradouro excluído com sucesso!" : "Falha ao excluir o logradouro!");
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
