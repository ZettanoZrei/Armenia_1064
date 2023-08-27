using System;
using System.IO;

public static class Logger
{
    private static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Armenia 1064\Logs");

    static Logger()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var fileName = $"log_{DateTime.Now:G}";
        fileName = fileName.Replace(':', '-').Replace('\\','.').Replace('/','.');
        path = Path.Combine(path, $"{fileName}.txt");
        File.Create(path).Close();
    }
    public static void WriteLog(string text)
    {
        using (var writer = new StreamWriter(path, true))
            writer.WriteLine($"{DateTime.Now}: {text}");
    }
}

