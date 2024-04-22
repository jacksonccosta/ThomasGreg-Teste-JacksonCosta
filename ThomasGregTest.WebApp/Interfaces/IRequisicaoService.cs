using ThomasGregTest.Web.Enums;

namespace ThomasGregTest.WebApp;

public interface IRequisicaoService
{
    Task<HttpResponseMessage> EnviarRequisicaoSemAutenticacao<TRequest, TResponse>(string endpoint, EMethods httpMethods, TRequest serialize = default);
    Task<ApiResponse<TResponse>?> EnviarRequisicaoAutenticada<TRequest, TResponse>(string endpoint, EMethods httpMethods, TRequest serialize = default);

}
