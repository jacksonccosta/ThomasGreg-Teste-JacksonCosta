using ThomasGregTest.Application;

namespace ThomasGregTest.WebApp;

public interface ILogradouroApiService
{
    Task<ApiResponse<IEnumerable<LogradouroResponse>>?> ObterLogradouros();
    Task<ApiResponse<LogradouroResponse>?> ObterLogradouroPorId(int id);
    Task<ApiResponse<int>?> AdicionarLogradouro(AdicionarLogradouroQuery cliente);
    Task<ApiResponse<int>?> AtualizarLogradouro(AtualizarLogradouroQuery cliente);
}
