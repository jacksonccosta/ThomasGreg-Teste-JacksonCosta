using MediatR;
using Microsoft.Extensions.Configuration;
using ThomasGregTest.Core;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Application;

public class AdicionarClienteHandler(IClienteRepository clienteRepository,
                                    UserAuthenticator userAuthenticator,
                                    IUsuarioRepository usuarioRepository,
                                    IConfiguration configuration) : IRequestHandler<AdicionarClienteQuery, IEvento>
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEvento> Handle(AdicionarClienteQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var emailJaCadastrado = await _clienteRepository.VerificaEmailCadastrado(request.Email, 0, cancellationToken);
            if (emailJaCadastrado)
                return new ResultadoEventos(success, "Já existe um cadastro com esse Email.");

            if (String.IsNullOrWhiteSpace(request.Nome))
                return new ResultadoEventos(success, "O campo Nome é obrigatório.");

            if (String.IsNullOrWhiteSpace(request.Email))
                return new ResultadoEventos(success, "O campo Email é obrigatório.");

            if (String.IsNullOrWhiteSpace(request.Logotipo.Base64))
                return new ResultadoEventos(success, "O campo Logotipo é obrigatório.");

            var cliente = ClienteMapper<Cliente>.Map(request);

            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(request.Logotipo.NomeArquivo);

            var caminhoLogotipo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, nomeArquivo);

            if (!Directory.Exists(_configuration.GetSection("FilePath:Cliente.Logotipo").Value))
                Directory.CreateDirectory(_configuration.GetSection("FilePath:Cliente.Logotipo").Value);

            SaveBase64Service.SaveBase64ToFile(request.Logotipo.Base64, caminhoLogotipo);

            cliente.Logotipo = nomeArquivo;

            var upsertedId = await _clienteRepository.Salvar(cliente, cancellationToken);
            success = upsertedId > 0;

            return new ResultadoEventos(success, success ? "Cliente cadastrado com sucesso!" : "Falha ao cadastrar o Cliente!", null, upsertedId);
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
