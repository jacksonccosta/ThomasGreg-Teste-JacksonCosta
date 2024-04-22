namespace ThomasGregTest.Application;

public static class JwtSettings
{
    public static string Secret = "d41d8cd98f00b204e9800998ecf8427e";
    public static int ValidForMinutes = 600;
    public static int RefreshTokenValidForMinutes = 1440;
    public static DateTime IssuedAt => DateTime.UtcNow;
    public static DateTime AccessTokenExpiration => IssuedAt.AddMinutes(ValidForMinutes);
    public static DateTime RefreshTokenExpiration => IssuedAt.AddMinutes(RefreshTokenValidForMinutes);
}
