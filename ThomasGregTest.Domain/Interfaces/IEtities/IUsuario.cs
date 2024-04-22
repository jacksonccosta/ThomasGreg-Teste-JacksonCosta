namespace ThomasGregTest.Domain;

public interface IUsuario
{
    int Id { get; set; }
    string Nome { get; set; }
    string Email { get; set; }
    string Senha { get; set; }
    bool Ativo { get; set; }
}
