using ThomasGregTest.Core;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Application;

public class AdicionarClienteQuery(string nome, string email, ArquivoLogotipo? logotipo) : Request<IEvento>
{
    public string Nome { get; private set; } = nome;
    public string Email { get; private set; } = email;
    public ArquivoLogotipo Logotipo { get; private set; } = logotipo;
}
