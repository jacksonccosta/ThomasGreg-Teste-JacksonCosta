using ThomasGregTest.Core;

namespace ThomasGregTest.Application;

public class AtualizarUsuarioQuery(int id, string nome, string email, string senha, bool ativo) : Request<IEvento>
{
    public int Id { get; private set; } = id;
    public string Nome { get; private set; } = nome.Trim();
    public string Email { get; private set; } = email.Trim();
    public string Senha { get; private set; } = senha.Trim();
    public bool Ativo { get; private set; } = ativo;
}
