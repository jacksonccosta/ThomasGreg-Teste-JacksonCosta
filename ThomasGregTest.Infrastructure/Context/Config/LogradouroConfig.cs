using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Infrastructure;

public class LogradouroConfig : IEntityTypeConfiguration<Logradouro>
{
    public void Configure(EntityTypeBuilder<Logradouro> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ClienteId);
        builder.HasOne(e => e.Cliente)
            .WithMany(cliente => cliente.Logradouros)
            .HasForeignKey(e => e.ClienteId);
    }
}
