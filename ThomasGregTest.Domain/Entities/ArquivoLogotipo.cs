namespace ThomasGregTest.Domain;

public class ArquivoLogotipo : IArquivoLogotipo
{
    public string NomeArquivo { get; set; } = null!;
    public string Base64 { get; set; } = null!;
}
