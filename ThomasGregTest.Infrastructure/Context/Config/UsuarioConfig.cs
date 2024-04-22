using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Infrastructure;

public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Nome).HasMaxLength(50).IsUnicode(false).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(e => e.Senha).HasMaxLength(50).IsUnicode(false).IsRequired();
    }
}
