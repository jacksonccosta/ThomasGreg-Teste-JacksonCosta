namespace ThomasGregTest.Domain;

public class JwtToken : IJwtToken
{
    public string AccessToken { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string TokenType { get; set; } = null!;
    public long ExpiresIn { get; set; }
}
