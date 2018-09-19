using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int selectableWorldNum = PlayerProgress.playerData.maxReachableWorld;
		int currentWorldNum =PlayerProgress.presentWorldIndex;
		for(int i = 0; i < transform.childCount; i++){
			Text worldText = transform.GetChild(i).GetComponentInChildren<Text>();
			worldText.text = MapDictionary.worldList[i];
		}

	}



}
