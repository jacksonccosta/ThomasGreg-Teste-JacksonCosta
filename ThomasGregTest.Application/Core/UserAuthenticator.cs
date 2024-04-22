using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ThomasGregTest.Application;

public class UserAuthenticator(IHttpContextAccessor accessor)
{
    private readonly IHttpContextAccessor _httpAcessor = accessor;

    public string Email => _httpAcessor.HttpContext.User.Identity.Name;

    public string Name => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
    public string PrimarySid => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.PrimarySid)?.Value;

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        return _httpAcessor.HttpContext.User.Claims;
    }
}
