using System.Net.Http.Headers;
using ThomasGregTest.Web.Enums;
using Newtonsoft.Json;
using System.Text;

namespace ThomasGregTest.WebApp;

public class RequisicaoService : IRequisicaoService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenService _tokenService;
    public RequisicaoService(IHttpClientFactory httpClientFactory,
        ITokenService tokenService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
    }
    public async Task<ApiResponse<TResponse>?> EnviarRequisicaoAutenticada<TRequest, TResponse>(string endpoint, EMethods httpMethods, TRequest serialize = default)

    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("Api");
            var token = await _tokenService.ObterToken();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{httpClient.BaseAddress}{endpoint}"),
                Method = new HttpMethod(httpMethods.ToString()),
            };
            if (serialize != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(serialize), Encoding.UTF8, "application/json");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(jsonContent);

            return result;
        }
        catch (Exception ex)
        {
            // Trate a exceção conforme necessário
            Console.Error.WriteLine($"Erro ao enviar requisição autenticada: {ex.Message}");
            return null;
        }

    }

    public Task<HttpResponseMessage> EnviarRequisicaoSemAutenticacao<TRequest, TResponse>(string endpoint, EMethods httpMethods, TRequest serialize = default)
    {
        throw new NotImplementedException();
    }
}
