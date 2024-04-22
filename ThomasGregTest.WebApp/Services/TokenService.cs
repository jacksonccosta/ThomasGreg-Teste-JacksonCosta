using Newtonsoft.Json;
using System.Text;
using ThomasGregTest.Application;
using ThomasGregTest.Domain;

namespace ThomasGregTest.WebApp;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    public TokenService(IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string>? ObterToken()
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
        if (token == null)
            return await RenovarRefreshToken();

        return token;
    }
    public string? ObterRefreshToken()
    {
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
        return refreshToken;
    }
    public async Task<string>? RenovarRefreshToken()
    {

        var refreshToken = ObterRefreshToken();
        if (refreshToken == null)
            return null;

        var httpClient = _httpClientFactory.CreateClient("Api");
        var query = new ValidaUsuarioRefreshTokenQuery(refreshToken);
        var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("usuario/refreshToken", content);

        response.EnsureSuccessStatusCode();

        var jsonContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<JwtToken>>(jsonContent);

        if (apiResponse != null && apiResponse.Success)
        {
            SalvarJwt(apiResponse.Data);
            return apiResponse.Data.AccessToken;
        }
        return null;
    }
    public void SalvarJwt(JwtToken jwtToken)
    {
        SalvarToken(jwtToken);
        SalvarRefreshToken(jwtToken.Token);
    }
    public void SalvarRefreshToken(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddMonths(1)
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
    public void SalvarToken(JwtToken jwtToken)
    {
        try
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddSeconds(jwtToken.ExpiresIn)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", jwtToken.AccessToken, cookieOptions);
        }
        catch (Exception ex)
        {

        }
    }
    public void ExcluirCookies()
    {
        try
        {
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies.Keys;

            foreach (var cookie in cookies)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
            }
        }
        catch (Exception ex)
        {

        }
    }
}
