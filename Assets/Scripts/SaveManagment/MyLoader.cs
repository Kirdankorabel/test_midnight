using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveManagment
{
    public static class MyLoader
    {
        public static void Save(GameData gameData, string name)
        {
            gameData.name = name;
            string filePath = Path.Combine(Application.persistentDataPath, "Saves/" + name);
            Debug.Log(filePath);
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Saves")))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Saves"));
            }
            File.WriteAllText(filePath, JsonUtility.ToJson(gameData));
        }

        public static GameData Load(string name)
        {
            return JsonUtility.FromJson<GameData>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Saves/" + name)));
        }

        public static List<GameData> LoadAll()
        {
            var gameData = new List<GameData>(); 
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Saves")))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Saves"));
            }
            var info = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Saves/"));
            Debug.Log(Path.Combine(Application.persistentDataPath, "Saves/"));

            var fileInfo = info.GetFiles();
            foreach (var file in fileInfo)
            {
                gameData.Add(JsonUtility.FromJson<GameData>(File.ReadAllText(file.FullName)));
            }
            return gameData;
        }

        public static GameData LoadLast()
        {
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Saves")))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Saves/"));
            }
            var info = new DirectoryInfo(Path.Combine(Application.persistentDataPath, "Saves/"));
            var fileInfo = info.GetFiles();
            var lastData = System.DateTime.Now;
            FileInfo result = null;
            foreach (var file in fileInfo)
            {
                if (file.LastWriteTime < lastData)
                {
                    result = file;
                    lastData = file.LastWriteTime;
                }
            }

            if (result != null)
            {
                return JsonUtility.FromJson<GameData>(File.ReadAllText(result.FullName));
            }
            return null;
        }
    }
}