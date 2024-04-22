using ThomasGregTest.Application;

namespace ThomasGregTest.WebApp;

public interface IClienteApiService
{
    Task<ApiResponse<IEnumerable<ClienteResponse>>?> ObterClientes();
    Task<ApiResponse<ClienteResponse>?> ObterClientePorId(int id);
    Task<ApiResponse<string>?> AdicionarCliente(AdicionarClienteQuery cliente);
    Task<ApiResponse<string>?> AtualizarCliente(AtualizarClienteQuery cliente);
}
