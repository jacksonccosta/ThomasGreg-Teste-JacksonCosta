namespace ThomasGregTest.Application;

public class LogradouroResponse
{
    public int Id { get; set; }
    public string Endereco { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string Cep { get; set; } = null!;
}
