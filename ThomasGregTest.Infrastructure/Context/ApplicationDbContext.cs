using Microsoft.EntityFrameworkCore;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    #region DbSet's
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Logradouro> Logradouros { get; set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RefreshTokenConfig());
        modelBuilder.ApplyConfiguration(new ClienteConfig());
        modelBuilder.ApplyConfiguration(new LogradouroConfig());
        modelBuilder.ApplyConfiguration(new UsuarioConfig());
    }
}
