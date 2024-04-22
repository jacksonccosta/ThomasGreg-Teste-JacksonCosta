using Microsoft.EntityFrameworkCore;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Infrastructure;

public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task AtualizarPorUsuario(RefreshToken refreshToken)
    {
        try
        {
            var currentRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UsuarioId.Equals(refreshToken.UsuarioId));
            if (currentRefreshToken != null)
            {
                _context.RefreshTokens.Remove(currentRefreshToken);
            }
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao atualizar usuário: {ex.Message}");
            return;
        }
    }
    public async Task<RefreshToken?> ObterPorChaveUsuario(string refreshToken)
    {
        try
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao obter chave por usuário: {ex.Message}");
            return null;
        }
    }
}
