using ThomasGregTest.Domain;

namespace ThomasGregTest.WebApp;

public interface ITokenService
{
    Task<string>? ObterToken();
    string? ObterRefreshToken();
    void SalvarJwt(JwtToken jwtToken);
    Task<string>? RenovarRefreshToken();
    void SalvarRefreshToken(string refreshToken);
    void ExcluirCookies();
}
