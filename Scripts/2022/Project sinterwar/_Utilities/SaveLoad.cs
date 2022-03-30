using UnityEngine;
using System.IO;

namespace Assets._Core.Scripts._Utilities
{
    public static class SaveLoad
    {
        public static string _pathData = Application.dataPath + "\\.data\\";
        public static string _pathPlayer = _pathData + "player\\";
        public static string _pathScene = _pathData + "scenes\\";

        public static void Init()
        {
            EnsureDirectoryExists(_pathData);
            EnsureDirectoryExists(_pathPlayer);
            EnsureDirectoryExists(_pathScene);
        }

        private static void EnsureDirectoryExists(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Directory.Exists)
            {
                Directory.CreateDirectory(fi.DirectoryName);
            }
        }

        public static void SaveToJson(object data, string path, string fileName)
        {
            WriteToFile(path + fileName, JsonUtility.ToJson(data));
        }

        public static object LoadFromJson(string fullPath)
        {
            return JsonUtility.FromJson<object>(ReadFromFile(fullPath));
        }

        private static void WriteToFile(string path, string data)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(data);
            sw.Close();
        }

        private static string ReadFromFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string data = sr.ReadToEnd();
            sr.Close();
            return data;
        }
    }
}