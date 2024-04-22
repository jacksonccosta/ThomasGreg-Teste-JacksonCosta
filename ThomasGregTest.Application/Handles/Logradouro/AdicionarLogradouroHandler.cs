using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;

namespace ThomasGregTest.Application;

public class AdicionarLogradouroHandler(ILogradouroRepository logradouroRepository,
                                        UserAuthenticator userAuthenticator,
                                        IUsuarioRepository usuarioRepository) : IRequestHandler<AdicionarLogradouroQuery, IEvento>
{
    private readonly ILogradouroRepository _logradouroRepository = logradouroRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<IEvento> Handle(AdicionarLogradouroQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(success, "Acesso expirado");

            if (request.ClienteId <= 0)
                return new ResultadoEventos(success, "O campo Cliente é obrigatório.");
            if (String.IsNullOrWhiteSpace(request.Endereco))
                return new ResultadoEventos(success, "O campo Endereco é obrigatório.");
            if (String.IsNullOrWhiteSpace(request.Numero))
                return new ResultadoEventos(success, "O campo Numero é obrigatório.");
            if (String.IsNullOrWhiteSpace(request.Bairro))
                return new ResultadoEventos(success, "O campo Bairro é obrigatório.");
            if (String.IsNullOrWhiteSpace(request.Cidade))
                return new ResultadoEventos(success, "O campo Cidade é obrigatório.");
            if (String.IsNullOrWhiteSpace(request.Estado))
                return new ResultadoEventos(success, "O campo Estado é obrigatório.");
            if (String.IsNullOrWhiteSpace(request.Cep))
                return new ResultadoEventos(success, "O campo Cep é obrigatório.");

            var logradouro = LogradouroMapper<Logradouro>.Map(request);
            var upsertedId = await _logradouroRepository.Salvar(logradouro, cancellationToken);
            success = upsertedId > 0;

            return new ResultadoEventos(success, success ? "Logradouro cadastrado com sucesso!" : "Falha ao cadastrar o Logradouro!", null, upsertedId);
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
