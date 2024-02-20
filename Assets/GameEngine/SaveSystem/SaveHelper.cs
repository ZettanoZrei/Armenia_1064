using Assets.Game.Configurations;
using Assets.Save;
using Model.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Systems.SaveSystem
{
    public class SaveHelper<T> where T : class, new()
    {
        private SaveConfing config;
        private readonly FileHelper fileHelper;
        private readonly JsonSerializer<T> jsonSerializer;
        private readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Armenia 1064");
        //private readonly string saveFileName = "saveData.json";

        private bool isSaving;
        public SaveHelper(FileHelper fileHelper, ConfigurationRuntime runtime)
        {
            this.fileHelper = fileHelper;
            this.config = runtime.SaveConfing;
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Culture = CultureInfo.InvariantCulture,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            };
            this.jsonSerializer = new JsonSerializer<T>(settings);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public IEnumerable<FileInfo> GetSavesFiles()
        {
            DirectoryInfo info = new DirectoryInfo(path);
            return info.GetFiles().OrderByDescending(p => p.CreationTime);
        }
        public async Task SaveModel(T saveModel)
        {
            if (isSaving) return;
            isSaving = true;
            await Task.Run(() =>
            {
                var data = jsonSerializer.Serialize(saveModel);
                fileHelper.Write(data, Path.Combine(path, $"{Guid.NewGuid()}.json"));
                Debug.Log("saved");
                isSaving = false;
            });
            DeleteExcessSaves();
        }
        public T LoadModel()
        {
            if (!CheckSaveFile())
            {
                return new T();
            }
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo file = info.GetFiles().OrderBy(p => p.CreationTime).Last();
            var data = fileHelper.Read(file.FullName);
            return jsonSerializer.Deserialize(data);
        }

        public T LoadModel(string path)
        {
            var data = fileHelper.Read(path);
            return jsonSerializer.Deserialize(data);
        }
        public bool CheckSaveFile()
        {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            return files.Any();
        }
        object locker = new object();
        private void DeleteExcessSaves()
        {
            lock (locker)
            {
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] files = info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
                var excessSaves = files.Length - config.savesNumber;
                if (excessSaves > 0)
                {
                    for (var k = files.Length - 1; k >= files.Length - excessSaves; k--)
                    {
                        files[k].Delete();
                    }
                }
            }
            
        }
    }
}
