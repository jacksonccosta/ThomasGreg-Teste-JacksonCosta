using ThomasGregTest.Core;

namespace ThomasGregTest.Application;

public class AdicionarUsuarioQuery(string nome, string email, string senha, bool ativo) : Request<IEvento>
{
    public string Nome { get; private set; } = nome.Trim();
    public string Email { get; private set; } = email.Trim();
    public string Senha { get; private set; } = senha.Trim();
    public bool Ativo { get; private set; } = ativo;
}
