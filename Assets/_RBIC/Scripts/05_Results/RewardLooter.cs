using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardLooter : MonoBehaviour {
	private List<Item> items;
	private List<Item> newItems;
	private List<Item> alreadyExistItems;
	/// <summary>
	/// The is winner for test. Delete this later
	/// </summary>
	public bool isWinnerForTest;

	private Reward reward;
	private int acquiredGold;
	private string[] acquiredChars;
	private string[] acquiredVehs;
	private Sprite[] acquiredCharSprts;
	private Sprite[] acquiredVehSprts;
	// Use this for initialization
	void Start () {
		items = new List<Item>();
		newItems = new List<Item>();
		alreadyExistItems = new List<Item>();

		WinLoseCondition.SetIsWinner(isWinnerForTest);
		//Check whether the reward loot exists or not
		reward = FindObjectOfType<Reward>();
		if(!reward){
			Debug.LogError("Cannot find reward Object to loot");
			return;
		}
		//If there is , then check if game won or lost
		if(!WinLoseCondition.GetIsWinner()){
			Debug.Log("Well. Look like you lost. you cannot loot the reward");
			return;
		}
		//if you won, then check whether it has been looted or not
		if(reward.GetHadLooted()){
			Debug.LogError("You completed this map before. Only gold could be collected");
			acquiredGold = reward.rewardGold;
			PlayerProgress.playerData.gold += acquiredGold;
			return;
		}

		//If has ever not been looted
		acquiredGold = reward.rewardGold;
		PlayerProgress.playerData.gold += reward.rewardGold;
		print(reward);
		print(reward.items);
		if(reward.items != null){
			for (int i = 0; i < reward.items.Length; i++){
				switch (reward.items[i].itemType){
					case Item.TYPE.CHARACTER:
						if(PlayerProgress.playerData.availableCharacters.Contains(reward.items[i].itemName)){
							Debug.Log("This character is already in your group. You will receive gold instead.");
							reward.items[i].isAlreadyInStock = true;
							PlayerProgress.playerData.gold += reward.items[i].itemValue;
							alreadyExistItems.Add(reward.items[i]);
							items.Add(reward.items[i]);
						}else if(CommonData.charNameList.Contains(reward.items[i].itemName)) {
							Debug.Log(reward.items[i].itemName + " is found in datatabase");
							newItems.Add(reward.items[i]);
							items.Add(reward.items[i]);
							PlayerProgress.playerData.availableCharacters.Add(reward.items[i].itemName);
							//Do stuff here
						}else{
							Debug.Log("this prisoner name :" + reward.items[i].itemName + " is not existing in the database");
						}
						break;
					case Item.TYPE.VEHICLE:
						if(PlayerProgress.playerData.availableVehicles.Contains(reward.items[i].itemName)){
							Debug.Log("This vehicle is already in your group. You will receive gold instead.");
							reward.items[i].isAlreadyInStock = true;
							PlayerProgress.playerData.gold += reward.items[i].itemValue;
							alreadyExistItems.Add(reward.items[i]);
							items.Add(reward.items[i]);
						}else if(CommonData.vehicleList.Contains(reward.items[i].itemName)) {
							Debug.Log(reward.items[i].itemName + " is found in datatabase");
							PlayerProgress.playerData.availableVehicles.Add(reward.items[i].itemName);
							newItems.Add(reward.items[i]);
							items.Add(reward.items[i]);
							//Do stuff here
						}else{
							Debug.Log("this vehicle name :" + reward.items[i].itemName + " is not existing in the database");
						}
						break;
					case Item.TYPE.SHOT:
						break;
					case Item.TYPE.MAP:
						break;
					case Item.TYPE.GOLD:
						break;
					default: break;
				}
			}
		}
		SaveLoadSystem.SaveGame(PlayerProgress.playerData);
	}


	public int GetAcquiredGold(){
		return acquiredGold;
	}

	public List<Item> GetNewItemList(){
		return newItems;
	}

	public List<Item> GetAlreadyExistItemList(){
		return alreadyExistItems;
	}

	public List<Item> GetItemList(){
		return items;
	}


}
