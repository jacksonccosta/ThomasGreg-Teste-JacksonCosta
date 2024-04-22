namespace ThomasGregTest.Application;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool Ativo { get; set; }
}
