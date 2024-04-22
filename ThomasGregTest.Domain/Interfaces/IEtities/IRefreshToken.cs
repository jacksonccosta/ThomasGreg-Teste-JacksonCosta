using System.ComponentModel.DataAnnotations;

namespace ThomasGregTest.Domain;

public interface IRefreshToken
{
    [Key]
    string Token { get; set; }
    int UsuarioId { get; set; }
    DateTime DataExpiracao { get; set; }
}
