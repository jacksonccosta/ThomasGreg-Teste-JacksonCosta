using System.Text.RegularExpressions;

namespace ThomasGregTest.Application;

public static class SaveBase64Service
{
    public static void SaveBase64ToFile(string base64String, string filePath)
    {
        var match = Regex.Match(base64String, @"^data:image/(?<type>.+?);base64,(?<data>.+)$");

        if (match.Success)
        {
            var type = match.Groups["type"].Value;
            var base64Data = match.Groups["data"].Value;

            byte[] bytes = Convert.FromBase64String(base64Data);

            File.WriteAllBytes(filePath, bytes);
        }
        else
        {
            byte[] bytes = Convert.FromBase64String(base64String);

            File.WriteAllBytes(filePath, bytes);
        }
    }
}
