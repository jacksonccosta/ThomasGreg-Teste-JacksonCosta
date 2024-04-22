namespace ThomasGregTest.Domain;

public class Cliente : ICliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Logotipo { get; set; } = null!;
    public List<Logradouro> Logradouros { get; set; } = null!;
}
