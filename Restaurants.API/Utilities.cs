public static class Utilities
{
    public static bool None<TSource>(this IEnumerable<TSource> source)
    {
        return !source.Any();
    }

    public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        return !source.Any(predicate);
    }

    public static string CapitalizeFirstWord(this string str)
    {
        if (str == null)
            return null;
        else if (str.Length == 0)
            return str;
        else if (str.Length == 1)
            str.ToUpper();
        else
            str = char.ToUpper(str[0]) + str.Substring(1).ToLower();
        return str;
    }

    public static void CreateWwwrootDirectoryStructureIfNotExists(string projectPath)
    {
        CreateDirectoryIfNotExists(projectPath, $"/wwwroot");
        CreateDirectoryIfNotExists(projectPath, $"/wwwroot/Files");
        CreateDirectoryIfNotExists(projectPath, $"/wwwroot/Files/Private");
        CreateDirectoryIfNotExists(projectPath, $"/wwwroot/Files/Public");
    }

    public static string GetLast(this string source, int tail_length)
    {
        if (tail_length >= source.Length)
            return source;
        return source.Substring(source.Length - tail_length);
    }

    public static void CreateDirectoryIfNotExists(string projectPath, string directoryPath)
    {
        if (!Directory.Exists($"{projectPath}{directoryPath}"))
        {
            Directory.CreateDirectory($"{projectPath}{directoryPath}");
        }
    }
}