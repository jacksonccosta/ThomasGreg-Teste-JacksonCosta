using ThomasGregTest.Core;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Application;

public class AtualizarClienteQuery(int id, string nome, string email, ArquivoLogotipo logotipo) : Request<IEvento>
{
    public int Id { get; private set; } = id;
    public string Nome { get; private set; } = nome;
    public string Email { get; private set; } = email;
    public ArquivoLogotipo Logotipo { get; private set; } = logotipo;
}
