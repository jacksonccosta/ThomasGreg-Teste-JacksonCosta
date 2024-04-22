using ThomasGregTest.Application;
using ThomasGregTest.Application.Util;
using ThomasGregTest.Domain;
using ThomasGregTest.Infrastructure;

namespace ThomasGregTest.API;

public static class MapDependencyRepository
{
    public static void RepositoryMap(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        
        services.AddSingleton<UserAuthenticator>();
        services.AddSingleton<IJwtService, JwtService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<Cookie>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ILogradouroRepository, LogradouroRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}
