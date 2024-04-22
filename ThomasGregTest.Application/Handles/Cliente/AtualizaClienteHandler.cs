using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ThomasGregTest.Application;

public class AtualizarClienteHandler(IClienteRepository clienteRepository,
                                    UserAuthenticator userAuthenticator,
                                    IUsuarioRepository usuarioRepository,
                                    IConfiguration configuration) : IRequestHandler<AtualizarClienteQuery, IEvento>
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEvento> Handle(AtualizarClienteQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var emailJaCadastrado = await _clienteRepository.VerificaEmailCadastrado(request.Email, request.Id, cancellationToken);
            if (emailJaCadastrado)
                return new ResultadoEventos(success, "Já existe um cadastro com esse Email.");

            if (String.IsNullOrWhiteSpace(request.Nome))
                return new ResultadoEventos(success, "O campo Nome é obrigatório.");

            if (String.IsNullOrWhiteSpace(request.Email))
                return new ResultadoEventos(success, "O campo Email é obrigatório.");

            var cliente = await _clienteRepository.ObterPorId(request.Id, cancellationToken);
            cliente.Nome = request.Nome;
            cliente.Email = request.Email;

            if (request.Logotipo != null && !String.IsNullOrWhiteSpace(request.Logotipo.NomeArquivo))
            {
                var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(request.Logotipo.NomeArquivo);

                var caminhoLogotipo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, nomeArquivo);

                if (!Directory.Exists(_configuration.GetSection("FilePath:Cliente.Logotipo").Value))
                    Directory.CreateDirectory(_configuration.GetSection("FilePath:Cliente.Logotipo").Value);
              
                SaveBase64Service.SaveBase64ToFile(request.Logotipo.Base64, caminhoLogotipo);
              
                var caminhoLogotipoAntigo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, cliente.Logotipo);
              
                DeleteFileService.DeleteFile(caminhoLogotipoAntigo);
              
                cliente.Logotipo = nomeArquivo;
            }

            var clienteMap = ClienteMapper<Cliente>.Map(cliente);
            var result = await _clienteRepository.Salvar(clienteMap, cancellationToken);
            success = result > 0;

            return new ResultadoEventos(success, success ? "Cliente atualizado com sucesso!" : "Nenhuma atualização realizada.");
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
