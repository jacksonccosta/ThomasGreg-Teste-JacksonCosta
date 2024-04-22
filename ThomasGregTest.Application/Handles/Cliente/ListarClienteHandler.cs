using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace ThomasGregTest.Application;

public class ListarClienteHandler(IClienteRepository clienteRepository,
                                UserAuthenticator userAuthenticator,
                                IUsuarioRepository usuarioRepository,
                                IConfiguration configuration) : IRequestHandler<ListarClienteQuery, IEvento>
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IConfiguration _configuration = configuration;

    public async Task<IEvento> Handle(ListarClienteQuery request, CancellationToken cancellationToken)
    {
        var response = new List<ClienteResponse>();
        try
        {
            if (_userAuthenticator.Email == null)
                return new ResultadoEventos(false, "Acesso expirado");

            var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
            if (usuarioLogado == null)
                return new ResultadoEventos(false, "Acesso expirado");

            var lista = await _clienteRepository.ListarClientes(cancellationToken);
            foreach(var cliente in lista)
            {
                var caminhoLogotipo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, cliente.Logotipo);
                cliente.Logotipo = FileToBase64Service.FileToBase64(caminhoLogotipo);
            }
            response = ClienteMapper<List<ClienteResponse>>.Map(lista);

            return new ResultadoEventos(true, response, lista.Count());
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(false, ex.Message);
        }
    }
}
