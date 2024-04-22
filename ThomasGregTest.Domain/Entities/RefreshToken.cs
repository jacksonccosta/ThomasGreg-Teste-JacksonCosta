using System.ComponentModel.DataAnnotations;

namespace ThomasGregTest.Domain;

public class RefreshToken : IRefreshToken
{
    [Key]
    public string Token { get; set; } = null!;
    public int UsuarioId { get; set; }
    public DateTime DataExpiracao { get; set; }
}
