using Assets.Systems.SaveSystem;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

public class FileHelper
{
    object locker = new object();
    public string Read(string path)
    {
        lock (locker) 
        {
            string str = "";
            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    str += line;
                }
            }
            return str;
        }        
    }

    public void Write(string text, string path)
    {
        lock(locker) 
        {
            using (var stream = new StreamWriter(path, false))
            {
                stream.WriteLine(text);
            }
        }       
    }

    //public static async Task SaveAsync<T>(T data)
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    await SerializeDataAsync(bf, data);
    //}
    //private static async Task SerializeDataAsync<T>(BinaryFormatter bf, T data)
    //{
    //    await Task.Run(() =>
    //    {
    //        string path = $"{Environment.CurrentDirectory}\\saveData.json";
    //        using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
    //        {
    //            bf.Serialize(stream, data);
    //            UnityEngine.MonoBehaviour.print("save done");
    //        }
    //    });
    //}
}

