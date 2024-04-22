namespace ThomasGregTest.Domain;

public class JsonWebToken : IJsonWebToken
{
    public string AccessToken { get; set; } = null!;
    public RefreshToken RefreshToken { get; set; } = null!;
    public string TokenType { get; set; } = "bearer";
    public long ExpiresIn { get; set; }
}
