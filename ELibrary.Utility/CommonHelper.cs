namespace ELibrary.Utility;

public class CommonHelper
{
    public static List<string> ConvertStringToList(string strings, char seperator)
    {
        if (string.IsNullOrEmpty(strings) || string.IsNullOrWhiteSpace(strings))
            return new List<string>();
        return strings.Split(seperator).ToList();
    }
}
