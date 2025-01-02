using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace View
{
    public static class LocalizationManager
    {
        private static Dictionary<string, string> localizedText;
        private static bool isReady = false;
        private static string missingTextString = "Localized text not found";

        public static event System.Action OnLanguageChanged;

        public static void LoadLocalizedText(string fileName)
        {
            localizedText = new Dictionary<string, string>();
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                {
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                }
            }
            else
            {
                Debug.LogAssertion("Cannot find file!");
            }
            isReady = true;
        }

        public static string GetLocalizedValue(string key, string defaultResultKey)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            string result = missingTextString;
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }
            else if (!localizedText.ContainsKey(key) && localizedText.ContainsKey(defaultResultKey))
            {
                result = localizedText[defaultResultKey];
            }
            else
            {
                Debug.LogError(key + " : " + defaultResultKey + " not found");
                result = string.Empty;
            }
            return result;
        }

        public static bool GetIsReady()
        {
            return isReady;
        }
    }

    [System.Serializable]
    public class LocalizationData
    {
        public LocalizationItem[] items;
    }

    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }
}