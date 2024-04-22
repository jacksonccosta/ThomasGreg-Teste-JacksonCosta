namespace ThomasGregTest.Application;

public static class FileToBase64Service
{
    public static string FileToBase64(string filePath)
    {
        try
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(fileBytes);
            return base64String;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
