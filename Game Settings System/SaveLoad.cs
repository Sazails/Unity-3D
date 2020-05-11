using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Game.Scripts.Settings
{
	public static class SaveLoad
	{
		public static string applicationPath = Application.dataPath;
		public static string resourcesPath = applicationPath + "\\Resources\\";
		public static string configPath = resourcesPath + "\\configs\\";

		public static void CreateNeededDirectories()
		{
			CreateDirectory(resourcesPath);
			CreateDirectory(configPath);
		}

		public static void SaveAsJson(object data, string savePath, string fileNameNoExtension,
			bool prettyFormat = true)
		{
			CreateNeededDirectories();
			File.WriteAllText(savePath + fileNameNoExtension + ".json", JsonUtility.ToJson(data, prettyFormat));
		}

		public static object LoadAsJson(string loadPath, string fileNameNoExtension)
		{
			return JsonUtility.FromJson<object>(File.ReadAllText(loadPath + fileNameNoExtension + ".json"));
		}

		public static bool Save(object data, string savePath, string fileName)
		{
			CreateNeededDirectories();
			var pathFileName = savePath + fileName;
			FileStream file;

			try
			{
				file = File.Create(pathFileName);
			}
			catch
			{
				return false;
			}

			var bf = new BinaryFormatter();

			try
			{
				bf.Serialize(file, data);
			}
			catch
			{
				file.Close();
				File.Delete(pathFileName);
				return false;
			}

			file.Close();
			return true;
		}

		public static object Load(string savePath, string fileName)
		{
			var pathFileName = savePath + fileName;

			if (!File.Exists(pathFileName)) return null;

			var bf = new BinaryFormatter();
			var file = File.Open(pathFileName, FileMode.Open);

			object data;

			try
			{
				data = bf.Deserialize(file);
			}
			catch
			{
				file.Close();
				return null;
			}

			file.Close();
			return data;
		}

		public static void CreateDirectory(string dirPath)
		{
			if (!DirectoryExists(dirPath))
				Directory.CreateDirectory(dirPath);
		}

		public static void DeleteDirectory(string dirPath)
		{
			if (DirectoryExists(dirPath))
				Directory.Delete(dirPath);
		}

		public static bool DirectoryExists(string dirPath)
		{
			return Directory.Exists(dirPath);
		}
	}
}