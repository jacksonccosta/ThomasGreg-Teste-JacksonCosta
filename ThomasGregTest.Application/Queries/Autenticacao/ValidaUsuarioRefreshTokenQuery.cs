using ThomasGregTest.Core;

namespace ThomasGregTest.Application
{
    public class ValidaUsuarioRefreshTokenQuery(string refreshToken) : Request<IEvento>
    {
        public string RefreshToken { get; } = refreshToken;
    }
}
