using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadSystem : MonoBehaviour  {

	private static readonly string SAVE_FILE = "playerprogress.json";
	private static string fileName = Path.Combine (Application.persistentDataPath, SAVE_FILE);
	//---------------------------------------------------------------
	public static void SaveGame(GameData dataToSave){
		string json = JsonUtility.ToJson (dataToSave);

		if (File.Exists (fileName)) {
			File.Delete (fileName);
			Debug.Log("Save file exists.");
		}
		File.WriteAllText (fileName, json);
		Debug.Log ("Player saved to " + fileName);
	}
	//---------------------------------------------------------------
	public static GameData LoadGame(){
		if (!File.Exists (fileName)) {
			Debug.Log("No save data to load");
			GameData loadedData= new GameData();
			loadedData.playerName = "ViEt";
			loadedData.gold = 0;
			//Set present world is 1
			loadedData.presentWorld = 0;
			loadedData.maxReachableWorld = 1;
			loadedData.availableMaps = new List<string>();
			loadedData.availableMaps.Add("Bat Cave");
			loadedData.completedMaps = new List<string>();
			loadedData.availableCharacters = new List<string>();
			loadedData.availableCharacters.Add(CommonData.char_pippo);
			loadedData.availableVehicles = new List<string>();
			loadedData.availableVehicles.Add(CommonData.veh_oven);
			SaveGame(loadedData);
			return loadedData;
		}else{
			string jsonFromSaveFile = File.ReadAllText (fileName);
			GameData loadedData = JsonUtility.FromJson<GameData> (jsonFromSaveFile);
			Debug.Log("Load successed from " + fileName);
			return loadedData;
		}
	}
}
