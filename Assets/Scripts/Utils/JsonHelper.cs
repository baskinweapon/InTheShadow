using System;
using System.IO;
using UnityEngine;

public static class SaveDirectory
{
#if UNITY_EDITOR
	public static string Path = Application.dataPath + "/StreamingAssets/gamedata.json";
#else
	public static string Path = Application.dataPath + "/Resources/Data/StreamingAssets/gamedata.json";
#endif
}

public static class JsonHelper {
	public static T[] FromJson<T>(string json) {
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
		return wrapper.Items;
	}

	public static string ToJson<T>(T[] array) {
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return JsonUtility.ToJson(wrapper);
	}

	public static string ToJson<T>(T[] array, bool prettyPrint) {
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return JsonUtility.ToJson(wrapper, prettyPrint);
	}

	public static bool WriteJsonToFile(string json, string fileName)
	{
		// if (File.Exists(SaveDirectory.Path + fileName + ".json"))
		// {
		// 	Debug.Log(fileName + " Exist");
		// 	return false;
		// }
		var file = File.CreateText(SaveDirectory.Path + fileName + ".json");
		file.Write(json);
		file.Close();
		return true;
	}

	public static string ReadJsonFromFile(string fileName)
	{
		if(File.Exists(fileName)){
			var sr = File.OpenText( fileName);
			var line = sr.ReadToEnd();
			return line;
		} else {
			Debug.Log("Could not Open the file: " + fileName + " for reading.");
			return null;
		}
	}
	
	[Serializable]
	private class Wrapper<T> {
		public T[] Items;
	}
}
