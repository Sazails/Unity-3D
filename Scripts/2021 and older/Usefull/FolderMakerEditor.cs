using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class FolderMakerEditor : EditorWindow {


	[MenuItem("Folder/MakeFolders")]
	public static void ShowWindow()
	{
		GetWindow<FolderMakerEditor> ("FolderMaker");
	}

	void Start()
	{
		var folder = Directory.CreateDirectory ("C:/Desktop/Hi");
	}

	void OnGUI()
	{
		GUILayout.Label ("Make folders", EditorStyles.boldLabel);

		if (GUILayout.Button ("Make Folders")) {
			string f = Application.dataPath + "/";

			Directory.CreateDirectory (f + "Scenes");
			Directory.CreateDirectory (f + "Scripts");
			Directory.CreateDirectory (f + "Models");
			Directory.CreateDirectory (f + "Materials");
			Directory.CreateDirectory (f + "PhysicMaterials");
			Directory.CreateDirectory (f + "Textures");
			Directory.CreateDirectory (f + "Fonts");
			Directory.CreateDirectory (f + "Sounds");
			Directory.CreateDirectory (f + "Prefabs");

			Debug.Log ("Folders created");
		}
	}

}
