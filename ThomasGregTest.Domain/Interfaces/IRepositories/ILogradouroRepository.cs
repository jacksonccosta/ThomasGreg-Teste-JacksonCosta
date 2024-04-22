namespace ThomasGregTest.Domain;

public interface ILogradouroRepository
{
    Task<int> Salvar(Logradouro entity, CancellationToken cancellationToken);
    Task<ILogradouro?> ObterPorId(int id,int clienteId, CancellationToken cancellationToken);
    Task<bool> ExcluirPorId(int id, int clienteId, CancellationToken cancellationToken);
    Task<IEnumerable<ILogradouro>?> ListarLogradouros(int clienteId, CancellationToken cancellationToken);
}
