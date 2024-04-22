using ThomasGregTest.Domain;

namespace ThomasGregTest.Application;

public interface IJwtService
{
    IJsonWebToken GeraTokenUsuario(IUsuario usuario);
}
