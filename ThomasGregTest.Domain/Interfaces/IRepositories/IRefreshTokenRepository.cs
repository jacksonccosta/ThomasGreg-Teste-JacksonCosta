namespace ThomasGregTest.Domain;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> ObterPorChaveUsuario(string refreshToken);
    Task AtualizarPorUsuario(RefreshToken refreshToken);
}
