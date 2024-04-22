namespace ThomasGregTest.WebApp;

public static class ConvertFileService
{
    public static async Task<string> ConvertFileToBase64(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        byte[] bytes = memoryStream.ToArray();
        return Convert.ToBase64String(bytes);
    }
}
