using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
	public string playerName;
	public int gold;
	public int presentWorld;
	public int maxReachableWorld;
	public List<string> availableMaps;
	public List<string> completedMaps;
	public List<string> availableCharacters;
	public List<string> availableVehicles;

}
