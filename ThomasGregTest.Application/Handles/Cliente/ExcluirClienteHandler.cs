using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ThomasGregTest.Application;

public class ExcluirClienteHandler(IClienteRepository clienteRepository,
                                    UserAuthenticator userAuthenticator,
                                    IUsuarioRepository usuarioRepository,
                                    IConfiguration configuration) : IRequestHandler<ExcluirClienteQuery, IEvento>
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEvento> Handle(ExcluirClienteQuery request, CancellationToken cancellationToken)
    {
        var success = false;
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(success, "Acesso expirado");

            var cliente = await _clienteRepository.ObterPorId(request.Id, cancellationToken);
            if (cliente == null)
                return new ResultadoEventos(success, "Cliente não localizado.");

            success = await _clienteRepository.ExcluirPorId(request.Id, cancellationToken);
            if (success)
            {
                var caminhoLogotipoAntigo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, cliente.Logotipo);

                DeleteFileService.DeleteFile(caminhoLogotipoAntigo);
            }
            return new ResultadoEventos(success, success ? "Cliente excluído com sucesso!" : "Falha ao excluir o cliente!");
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(success, ex.Message);
        }
    }
}
