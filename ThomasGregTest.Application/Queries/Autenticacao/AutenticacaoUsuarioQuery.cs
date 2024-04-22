using ThomasGregTest.Core;

namespace ThomasGregTest.Application
{
    public class AutenticacaoUsuarioQuery(string email, string password) : Request<IEvento>
    {
        public string Email { get; } = email;
        public string Password { get; } = password;
    }
}
