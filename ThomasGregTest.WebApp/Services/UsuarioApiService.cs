using ThomasGregTest.Application;
using ThomasGregTest.Web.Enums;

namespace ThomasGregTest.WebApp;

public class UsuarioApiService : IUsuarioApiService
{
    private readonly IRequisicaoService _requisicaoService;
    public UsuarioApiService(IRequisicaoService requisicaoService)
    {
        _requisicaoService = requisicaoService;
    }

    public async Task<ApiResponse<string>> AdicionarUsuario(AdicionarUsuarioQuery query)
    {
        var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AdicionarUsuarioQuery, string>("usuario", EMethods.POST, query);
        return apiResponse;
    }

    public async Task<ApiResponse<string>> AtualizarUsuario(AtualizarUsuarioQuery query)
    {
        var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AtualizarUsuarioQuery, string>("usuario", EMethods.PUT, query);
        return apiResponse;
    }

   
}
