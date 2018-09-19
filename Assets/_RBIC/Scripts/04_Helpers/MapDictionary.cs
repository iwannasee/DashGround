using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDictionary : MonoBehaviour {
	public const int WORLD_SUM_NUMBER = 2;
	public const string WRONGINPUT = "WRONG"; 
	/*==============================================================================*/
	//										MAP NAMES
	/*==============================================================================*/
	public const string w1_map_bCave = "Bat Cave";
	public const string w1_map_wBay = "Windless Bay";
	public const string w1_map_hRant = "Humbling Taurant";
	public const string w1_SC_map_vHole = "Volcanic Hole";
	public const string w1_SC_map_sSho = "Sangosho";

	public const string w2_map_dCafe = "Dungeon Cafe";
	public const string w2_map_sRock = "Sliding Rock";
	public const string w2_map_mAbs = "Magma Abyss";
	public const string w2_SC_map_cGrv = "Crimson Grave";
	public const string w2_SC_map_nReamth = "Nethereamth";
	public const string worldName_0 = "THE DESERT";
	public const string worldName_1 = "THE HIGHLAND";
	//dictionary of secret maps
	private static Dictionary<string,string> CurrentSecretMapDict;


	//list of playable maps
	public static List<string> availableMapDict;
	//list of total locations of whole game
	/*==============================================================================*/
	//WORLD 1
	public static readonly string[] mapOfWorld_0 = {
		w1_map_bCave,
		w1_map_wBay,
		w1_map_hRant,
		w1_SC_map_vHole,
		w1_SC_map_sSho
	};
	private static bool secret_world_1_reveal = false;
	/*==============================================================================*/
	//WORLD 2
	public static readonly string[] mapOfWorld_1 = {
		w2_map_dCafe,
		w2_map_sRock,
		w2_map_mAbs,
		w2_SC_map_cGrv,
		w2_SC_map_nReamth
	};
	private static bool secret_world_2_reveal = false;
	/*==============================================================================*/
	public static readonly string[][] wholeGameMaps = new string[WORLD_SUM_NUMBER][]{
		mapOfWorld_0,
		mapOfWorld_1
	};
	public static readonly string[][] SecretMaps = new string[WORLD_SUM_NUMBER][]{
		new string[]{w1_SC_map_vHole,w1_SC_map_sSho}, 	//of World 0
		new string[]{w2_SC_map_cGrv,w2_SC_map_nReamth}	//of World 1
	};

	public static readonly string[] worldList = new string[]{
		worldName_0,
		worldName_1
	};
	private static int secretMapNum;
	private int worldNumber;
	private static string[] thisWorldLocations;

	// Use this for initialization
	void Start () {
		InitMapDict();
		LoadMap();
		secretMapNum = CurrentSecretMapDict.Count;
	}

	//---------------------------------------------------------------
	private void LoadMap(){
		availableMapDict = PlayerProgress.playerData.availableMaps;
		if(availableMapDict == null){
			Debug.Log("availableMapDict is not instantiated");
			availableMapDict = new List<string> ();
		}
	} 
	//---------------------------------------------------------------
	private void InitMapDict(){
		worldNumber =  PlayerProgress.presentWorldIndex;
		thisWorldLocations = wholeGameMaps[worldNumber];
		CurrentSecretMapDict = MapInitializer.wholeGameSecretMaps[worldNumber];
		Debug.LogWarning("the world now is world :" + worldNumber);
	}
	//---------------------------------------------------------------
	public void SetMapDictValue(string name){
		if(!CheckAlreadyAcquired(name)){
			availableMapDict.Add(name);
		}
	}

	//---------------------------------------------------------------
	public string GetMapName(string inputKey){
		if(CurrentSecretMapDict.ContainsKey(inputKey)){
			string correspondValue ="";
			CurrentSecretMapDict.TryGetValue(inputKey, out correspondValue);
				if(CurrentSecretMapDict.ContainsValue(correspondValue)){
					return correspondValue;
				}
		}
		return WRONGINPUT;
	}
	//---------------------------------------------------------------
	public bool CheckAlreadyAcquired(string mapNameToCheck){
		if(availableMapDict.Contains(mapNameToCheck)){
			Debug.Log("Contained this map" + mapNameToCheck);
			return true;
		}else{
			Debug.Log("does not contain this map" + mapNameToCheck);
			return false;
		}
	}
	//---------------------------------------------------------------
	public static bool CheckIsSecretMap(string mapNameToCheck){
		bool isSecretMap; 
		if(CurrentSecretMapDict.ContainsValue(mapNameToCheck)){
			isSecretMap = true;
		}else{
			isSecretMap = false;
		}
		return isSecretMap;
	}

	//---------------------------------------------------------------
	public static string[] GetThisWorldLocations(){
		return thisWorldLocations;
	}
	//---------------------------------------------------------------
	public static int GetNormalMapsNumOfNowWorld(){
		int totalMapNum = thisWorldLocations.Length;
		int normalMapNum = totalMapNum -secretMapNum;

		return normalMapNum;
	}
	//---------------------------------------------------------------

	//---------------------------------------------------------------
	//EXTENDABLE #1 optionally disable input or just keep player able to input
	/*public bool IsAllSecretMapRevealed(){
		int acquiredSecretMapCount = 0;
		for(int i = 0; i< CurrentSecretMapDict.Count; i++){
			if(PlayerProgress.playerData.availableMaps.Contains(SecretMaps[worldNumber][i])){
				acquiredSecretMapCount++;
			}
		}
		if(acquiredSecretMapCount == CurrentSecretMapDict.Count){
			secret_world_1_reveal = true;
			return secret_world_1_reveal;
		}else{
			secret_world_1_reveal = false;
			return secret_world_1_reveal;
		}
	}*/
}
