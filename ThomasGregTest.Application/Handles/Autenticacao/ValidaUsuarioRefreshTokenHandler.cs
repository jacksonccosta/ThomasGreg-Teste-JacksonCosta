using ThomasGregTest.Core;
using ThomasGregTest.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ThomasGregTest.Application;

public class ValidaUsuarioRefreshTokenHandler(
    IJwtService jwtService,
    IUsuarioRepository usuarioRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IHttpContextAccessor httpContextAccessor,
    IMediator mediator) : IRequestHandler<ValidaUsuarioRefreshTokenQuery, IEvento>
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IMediator _mediator = mediator;

    public async Task<IEvento> Handle(ValidaUsuarioRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IUsuario? usuario = null;
            if (!String.IsNullOrEmpty(request.RefreshToken))
            {
                var token = await _refreshTokenRepository.ObterPorChaveUsuario(request.RefreshToken);
                if (token == null || token.DataExpiracao < DateTime.Now)
                    return new ResultadoEventos(false, new JwtToken
                    {
                        AccessToken = string.Empty,
                        Token = string.Empty,
                        TokenType = string.Empty,
                        ExpiresIn = 0
                    });
                usuario = await _usuarioRepository.ObterPorId(token.UsuarioId, new CancellationToken());
            }
            if (usuario == null)
                return new ResultadoEventos(false, new JwtToken
                {
                    AccessToken = string.Empty,
                    Token = string.Empty,
                    TokenType = string.Empty,
                    ExpiresIn = 0
                });

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
