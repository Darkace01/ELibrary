using Microsoft.AspNetCore.Http;

namespace ELibrary.Utility;

public class CommonHelper
{
    public static List<string> ConvertStringToList(string strings, char seperator) => string.IsNullOrEmpty(strings) || string.IsNullOrWhiteSpace(strings) ? new List<string>() : strings.Split(seperator).ToList();

    public static string GetFileFormat(IFormFile file) => new FileInfo(file.FileName).Extension;

    public static bool CheckFileFormat(IFormFile file, string extension) => GetFileFormat(file).ToLower() == extension.ToLower();
}
