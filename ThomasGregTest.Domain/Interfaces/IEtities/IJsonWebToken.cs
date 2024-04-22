namespace ThomasGregTest.Domain;

public interface IJsonWebToken
{
    string AccessToken { get; set; }
    RefreshToken RefreshToken { get; set; }
    string TokenType { get; set; }
    long ExpiresIn { get; set; }
}
