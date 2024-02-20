using Assets.Game.Configurations;
using System;
using System.IO;

public class Logger
{
    private static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Armenia 1064\Logs");
    private static readonly LogConfig _config; 

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
        if (true) //TODO: Переделать на обычны класс, чтобы можно было пролучать настройки
            return;
        using (var writer = new StreamWriter(path, true))
            writer.WriteLine($"{DateTime.Now}: {text}");
    }
}

