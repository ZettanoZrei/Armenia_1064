using Assets.Save;
using Model.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Systems.SaveSystem
{
    public class SaveHelper<T> where T: class, new()  
    {
        private readonly FileHelper fileHelper;
        private readonly JsonSerializer<T> jsonSerializer;
        //private readonly string path = $"{Environment.CurrentDirectory}\\saveData.json";
        private readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Armenia 1064");
        private readonly string saveFileName = "saveData.json";

        private bool isSaving;
        public SaveHelper(FileHelper fileHelper)
        {
            this.fileHelper = fileHelper;
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Culture = CultureInfo.InvariantCulture,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            };
            this.jsonSerializer = new JsonSerializer<T>(settings);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public async Task SaveModel(T saveModel)
        {   
            if(isSaving) return;
            isSaving = true;
            await Task.Run(() =>
            {
                var data = jsonSerializer.Serialize(saveModel);
                fileHelper.Write(data, Path.Combine(path, saveFileName));
                Debug.Log("saved");
                isSaving = false;
            });
        }

        public T LoadModel() 
        {
            if (!CheckSaveFile())
            {
                return new T();
            }
            var data = fileHelper.Read(Path.Combine(path, saveFileName));
            return jsonSerializer.Deserialize(data);
        }  
        
        public bool CheckSaveFile()
        {
            return File.Exists(Path.Combine(path, saveFileName));
        }
    }
}
