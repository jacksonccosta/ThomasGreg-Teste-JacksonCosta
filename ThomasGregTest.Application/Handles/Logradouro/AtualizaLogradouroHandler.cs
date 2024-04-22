using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;

namespace ThomasGregTest.Application.Handlers
{
    public class AtualizarLogradouroHandler(ILogradouroRepository logradouroRepository,
                                            UserAuthenticator userAuthenticator,
                                            IUsuarioRepository usuarioRepository) : IRequestHandler<AtualizarLogradouroQuery, IEvento>
    {
        private readonly ILogradouroRepository _logradouroRepository = logradouroRepository;
        private readonly UserAuthenticator _userAuthenticator = userAuthenticator;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<IEvento> Handle(AtualizarLogradouroQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                if (_userAuthenticator.Email == null)
                    return new ResultadoEventos(success, "Acesso expirado");

                var usuarioLogado = await _usuarioRepository.ObterPorEmailAtivo(_userAuthenticator.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultadoEventos(success, "Acesso expirado");

                var logradouro = await _logradouroRepository.ObterPorId(request.Id, request.ClienteId, cancellationToken);

                if (logradouro == null)
                    return new ResultadoEventos(success, "Logradouro não localizado.");
                if (request.ClienteId <= 0)
                    return new ResultadoEventos(success, "O campo Cliente é obrigatório.");
                if (String.IsNullOrWhiteSpace(request.Endereco))
                    return new ResultadoEventos(success, "O campo Endereço é obrigatório.");
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

                logradouro.Endereco = request.Endereco;
                logradouro.Numero = request.Numero;
                logradouro.Bairro = request.Bairro;
                logradouro.Cidade = request.Cidade;
                logradouro.Estado = request.Estado;
                logradouro.Cep = request.Cep;

                var logradouroMap = LogradouroMapper<Logradouro>.Map(logradouro);
                var result = await _logradouroRepository.Salvar(logradouroMap, cancellationToken);
                success = result > 0;

                return new ResultadoEventos(success, success ? "Logradouro atualizado com sucesso!" : "Nenhuma atualização realizada.");
            }
            catch (Exception ex)
            {
                return new ResultadoEventos(success, ex.Message);
            }
        }
    }
}
