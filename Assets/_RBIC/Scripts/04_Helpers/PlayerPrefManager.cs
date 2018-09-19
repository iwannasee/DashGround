using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefManager : MonoBehaviour {
	
	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	const string CURRENT_LEVEL_NAME_KEY = "current_level_name";
	//this is causing not moving of paddle if the scene is corrupted at the pausing time
	//const string SKILL_PAUSE_KEY = "skill_pause";
	//public const int SKILL_PAUSING = 1;
	//public const int SKILL_UNPAUSE = 0;
	
	const string GUITEXT_STT_KEY = "guitext_change";
	public const int GUITEXT_STATUS_CHANGING = 1;
	public const int GUITEXT_STATUS_CHANGE_END = 0;


	/**********************************************************************************/
	/**************	THIS HANDLES THE OPTIONS ITEMS Adjustment	***********************/
	/**********************************************************************************/
	public static void SetMasterVolume(float volume){
		if(volume>=0 && volume <= 1f){
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		}else{
			Debug.LogError("Master volume out of range!");
		}	
	}
	//----------------------------------------------------------------------------------
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
	//----------------------------------------------------------------------------------
	public static void UnlockLevel( int level){
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString (), 1);
		} else {
			Debug.LogError ("Trying to unlock level not in the build order");
		}
	}
	//----------------------------------------------------------------------------------
	public static bool IsLevelUnlocked(int level){
		int levelValue = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString ());
		bool isLevelUnlocked = (levelValue == 1);
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			return isLevelUnlocked;
		} else {
			Debug.LogError ("Trying to query level not in the build order");
			return false;
		}
	}
	//----------------------------------------------------------------------------------
	public static void SetDifficulty (float volume)
	{
		if (volume >= 1f && volume <= 3f) {
			PlayerPrefs.SetFloat (DIFFICULTY_KEY, volume);
		}else{
			Debug.LogError ("Master Difficulty out of range");
		}
	}
	//----------------------------------------------------------------------------------
	public static float GetDifficulty()
	{	
		return PlayerPrefs.GetFloat (DIFFICULTY_KEY);
	}
	//----------------------------------------------------------------------------------
	/**********************************************************************************/
	/**************	THIS HANDLES THE UI TEXT in Game Scene		***********************/
	/**********************************************************************************/
	//----------------------------------------------------------------------------------
	/*public static void SetSkillPause(int isPauseOrNot){
		PlayerPrefs.SetInt(SKILL_PAUSE_KEY, isPauseOrNot);
	}
	//----------------------------------------------------------------------------------
	public static bool GetSkillPause(){
		int isPause = PlayerPrefs.GetInt(SKILL_PAUSE_KEY);
		if(isPause == 0){
			return false;
		}else{
			return true;
		}
	}*/
	//----------------------------------------------------------------------------------
	public static void SetUITextStatus(int isChangingOrNot){
		PlayerPrefs.SetInt(GUITEXT_STT_KEY, isChangingOrNot);
	}
	//----------------------------------------------------------------------------------
	public static bool GetUITextStatus(){
		int isChangingOrNot = PlayerPrefs.GetInt(GUITEXT_STT_KEY);
		if(isChangingOrNot == 0){
			return false;
		}else{
			return true;
		}
	}
	//----------------------------------------------------------------------------------
	public static void SetCurrentLevelName(string currentLevelName){
		PlayerPrefs.SetString(CURRENT_LEVEL_NAME_KEY, currentLevelName);
	}
	//----------------------------------------------------------------------------------
	public static string GetCurrentLevelName(){
		return PlayerPrefs.GetString(CURRENT_LEVEL_NAME_KEY);
	}
}
