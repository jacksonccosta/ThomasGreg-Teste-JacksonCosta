using ThomasGregTest.Application;

namespace ThomasGregTest.WebApp;

public interface IUsuarioApiService
{
    Task<ApiResponse<string>> AdicionarUsuario(AdicionarUsuarioQuery cliente);
    Task<ApiResponse<string>> AtualizarUsuario(AtualizarUsuarioQuery cliente);
}
