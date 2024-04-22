namespace ThomasGregTest.Domain;

public interface IUsuarioRepository
{
    Task<int> Salvar(Usuario entity, CancellationToken cancellationToken);
    Task<IUsuario?> ObterPorEmailAtivo(string email, CancellationToken cancellationToken);
    Task<IUsuario?> ObterPorId(int id, CancellationToken cancellationToken); 
    Task<IEnumerable<IUsuario>?> ListarUsuarios(CancellationToken cancellationToken);
}
