using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	private int stageCollectedCoin = 0;
	public int TotalLifeBags;
	public Text levelText;
	public static GameObject[] prisonersToPlay;

	// Update is called once per frame
	void Update () {
		levelText.text = "Golds: " + stageCollectedCoin;
	}
	
	public void AddCoin(){
		stageCollectedCoin += GoldCoin.coinValue;
	}

	public int GetCollectedCoin(){
		return stageCollectedCoin;
	}

	public static void AddToPrisonersToPlay(GameObject prisoner){
	}
}
