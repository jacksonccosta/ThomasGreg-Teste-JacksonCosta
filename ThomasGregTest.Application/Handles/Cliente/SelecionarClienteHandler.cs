using ThomasGregTest.Core;
using MediatR;
using ThomasGregTest.Domain;
using Microsoft.Extensions.Configuration;

namespace ThomasGregTest.Application.Handlers
{
    public class SelecionarClienteHandler : IRequestHandler<SelecionarClienteQuery, IEvento>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly UserAuthenticator _userAuthenticator;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public SelecionarClienteHandler(IClienteRepository clienteRepository,
            UserAuthenticator userAuthenticator,
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration)
        {
            _clienteRepository = clienteRepository;
            _userAuthenticator = userAuthenticator;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<IEvento> Handle(SelecionarClienteQuery request, CancellationToken cancellationToken)
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
                var clienteMap = ClienteMapper<ClienteResponse>.Map(cliente);
                if (cliente == null)
                    return new ResultadoEventos(success, "Cliente não localizado.");
              
                var caminhoLogotipo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, clienteMap.Logotipo);
                clienteMap.Logotipo = FileToBase64Service.FileToBase64(caminhoLogotipo);
              
                return new ResultadoEventos(true, clienteMap);
            }
            catch (Exception ex)
            {
                return new ResultadoEventos(success, ex.Message);
            }
        }
    }
}
