using Microsoft.EntityFrameworkCore;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Infrastructure;

public class UsuarioRepository(ApplicationDbContext context) : IUsuarioRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> Salvar(Usuario entity, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = await _context.Usuarios.FindAsync(entity.Id);

            if (usuario != null)
                _context.Entry(usuario).CurrentValues.SetValues(entity);
            else
                _context.Usuarios.Add((Usuario)entity);

            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao salvar usuário: {ex.Message}");
            return 0;
        }
    }
    public async Task<IUsuario?> ObterPorEmailAtivo(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao obter usuário por e-mail: {ex.Message}");
            return null;
        }
    }
    public async Task<IUsuario?> ObterPorId(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao obter usuário: {ex.Message}");
            return null;
        }
    }
    public async Task<IEnumerable<IUsuario>?> ListarUsuarios(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Usuarios.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao listar usuários: {ex.Message}");
            return null;
        }
    }
}
