namespace ThomasGregTest.Domain;

public interface IClienteRepository
{
    Task<int> Salvar(Cliente entity, CancellationToken cancellationToken);
    Task<ICliente?> ObterPorId(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ICliente>?> ListarClientes(CancellationToken cancellationToken);
    Task<bool> ExcluirPorId(int id, CancellationToken cancellationToken);
    Task<bool> VerificaEmailCadastrado(string email, int id, CancellationToken cancellationToken);
}
