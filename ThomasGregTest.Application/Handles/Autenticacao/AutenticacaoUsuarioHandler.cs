using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using ThomasGregTest.Application.Util;

namespace ThomasGregTest.Application;

public class AutenticacaoUsuarioHandler(
    IJwtService jwtService,
    IUsuarioRepository usuariorRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator) : IRequestHandler<AutenticacaoUsuarioQuery, IEvento>
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly IUsuarioRepository _usuarioRepository = usuariorRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IMediator _mediator = mediator;

    public async Task<IEvento> Handle(AutenticacaoUsuarioQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IUsuario usuario = await _usuarioRepository.ObterPorEmailAtivo(request.Email, cancellationToken);

            if (usuario == null)
                return new ResultadoEventos(false, "Verifique se digitou corretamente os dados de acesso e tente novamente.");

            if (!Criptografia.Verify(request.Password, usuario.Senha))
                return new ResultadoEventos(false, "Login/Senha inválidos, tente novamente.");

            var jwt = _jwtService.GeraTokenUsuario(usuario);
            await _refreshTokenRepository.AtualizarPorUsuario(jwt.RefreshToken);
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return new ResultadoEventos(true, new JwtToken
            {
                AccessToken = jwt.AccessToken,
                Token = jwt.RefreshToken.Token,
                TokenType = jwt.TokenType,
                ExpiresIn = jwt.ExpiresIn
            });
        }
        catch (Exception ex)
        {
            return new ResultadoEventos(false, ex.Message);
        }       
    }
}
