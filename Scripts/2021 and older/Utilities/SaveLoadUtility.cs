using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace _Project.Scripts._Utilities
{
    public static class SaveLoadUtility
    {
        public static string SavePath = Application.dataPath + "\\saves\\";

        public static void Init()
        {
            DirectoryUtility.CreateDirectory(SavePath);
        }
        
        public static bool SavePDP(System.Object data,string fileName){ return Save(data,Application.persistentDataPath+fileName); }
        
        public static bool Save(System.Object data, string pathFileName){
           
            FileStream file;
            try
            {
                file = File.Create(pathFileName);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
           
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(file,data);
            }
            catch (Exception e)
            {
                file.Close();
                File.Delete(pathFileName);
                Debug.LogException(e);
                return false;
            }
           
            file.Close();
            return true;
        }
     
        public static System.Object LoadPDP(string fileName){ return Load(Application.persistentDataPath+fileName); }
        
        public static System.Object Load(string pathFileName){
           
            if(!File.Exists(pathFileName)) return null;
           
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathFileName,FileMode.Open);
           
            System.Object data;
            try{ data = bf.Deserialize(file); }
            catch {
                file.Close();
                return null;
            }
           
            file.Close();
            return data;
        }
    }
}