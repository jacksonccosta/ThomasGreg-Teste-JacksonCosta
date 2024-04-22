namespace ThomasGregTest.Domain;

public class Logradouro : ILogradouro
{
    public int Id { get; set; }
    public string Endereco { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string Cep { get; set; } = null!;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
}
