namespace ThomasGregTest.Domain;

public interface IJwtToken
{
    string AccessToken { get; set; }
    string Token { get; set; }
    string TokenType { get; set; }
    long ExpiresIn { get; set; }
}