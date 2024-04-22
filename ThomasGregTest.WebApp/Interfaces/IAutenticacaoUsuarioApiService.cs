using ThomasGregTest.Application;

namespace ThomasGregTest.WebApp;

public interface IAutenticacaoUsuarioApiService
{
    Task<bool> AutenticacaoUsuario(AutenticacaoUsuarioQuery autenticacaoUsuario);
    Task<ApiResponse<UsuarioLogadoResponse>> ObterUsuarioLogado();
}
