using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WorldTitle : MonoBehaviour {
	private Text worldName;
	public Transform worldSelector;
	private bool isListShown = false;
	private bool isListInitialized = false;
	// Use this for initialization
	void Start () {
		worldName = GetComponentInChildren<Text>();
		int currentWorld = PlayerProgress.presentWorldIndex;
		worldName.text = MapDictionary.worldList[currentWorld];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if(!isListShown){
			if(!isListInitialized){
				InitWorldList();
			}
			worldSelector.gameObject.SetActive(true);
			isListShown = true;
		}else{
			worldSelector.gameObject.SetActive(false);
			isListShown = false;
		}
	}

	void InitWorldList(){
		int selectableWorldNum = PlayerProgress.playerData.maxReachableWorld;
		int currentWorldNum = PlayerProgress.presentWorldIndex;
		//Show buttons in accordance with the max playable world
		for (int i = 0; i<selectableWorldNum; i++){
			Transform thisBtn = worldSelector.GetChild(i);
			thisBtn.gameObject.SetActive(true);
			if (i == currentWorldNum){
				Button toWorldBtn = thisBtn.GetComponent<Button>();
				toWorldBtn.interactable = false;
			}
		}
		isListInitialized = true;
	}

	public void LoadWorld(string mapSelectWorldName){
		string currentSceneName = SceneManager.GetActiveScene().name;

		for(int i = 0; i< PlayerProgress.playerData.maxReachableWorld; i++){
			Debug.Log(worldSelector.GetChild(i).name);
			if(EventSystem.current.currentSelectedGameObject == worldSelector.GetChild(i).gameObject){
				PlayerProgress.presentWorldIndex = i;
				Debug.Log("increase presenworld successfully " + i + " "+ EventSystem.current.currentSelectedGameObject);
				  
			}
		}
		if(currentSceneName!= mapSelectWorldName){
			LevelManager.SLoadLevel(mapSelectWorldName);
		}else{
			Debug.Log("cant load because select the same current map");
		}

		Debug.Log("presentWorld " + PlayerProgress.presentWorldIndex);
	}
}
