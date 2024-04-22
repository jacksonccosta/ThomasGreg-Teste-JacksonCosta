using ThomasGregTest.Application;
using ThomasGregTest.Web.Enums;

namespace ThomasGregTest.WebApp;

public class ClienteApiService : IClienteApiService
{
    private readonly IRequisicaoService _requisicaoService;

    public ClienteApiService(IRequisicaoService requisicaoService)
    {
        _requisicaoService = requisicaoService;
    }

    public async Task<ApiResponse<string>?> AdicionarCliente(AdicionarClienteQuery query)
    {
        try
        {
            var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AdicionarClienteQuery, string>("cliente", EMethods.POST, query);
            return apiResponse;
        }
        catch (HttpRequestException ex)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<ApiResponse<string>?> AtualizarCliente(AtualizarClienteQuery query)
    {
        try
        {
            var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AtualizarClienteQuery, string>("cliente", EMethods.PUT, query);
            return apiResponse;
        }
        catch (HttpRequestException ex)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<ApiResponse<IEnumerable<ClienteResponse>>?> ObterClientes()
    {
        try
        {
            var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<object, IEnumerable<ClienteResponse>>("cliente/listar", EMethods.GET);
            return apiResponse;
        }
        catch (HttpRequestException ex)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<ApiResponse<ClienteResponse>?> ObterClientePorId(int id)
    {
        try
        {
            var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<int, ClienteResponse>($"cliente/{id}", EMethods.GET);
            return apiResponse;
        }
        catch (HttpRequestException ex)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
