namespace ThomasGregTest.Domain;

public interface ICliente
{
    public int Id { get; set; }
    string Nome { get; set; }
    List<Logradouro> Logradouros { get; set; }
    string Email { get; set; } 
    string Logotipo { get; set; }
}
