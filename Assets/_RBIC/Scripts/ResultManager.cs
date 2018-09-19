using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour {
	public GameObject itemPref;
	public GameObject rewardLooterGameObj;
	private Reward reward;
	public Text levelText;
	public GameObject itemPanel;
	public Sprite goldSprtForReplace;
	private RewardLooter rwLooter;

	private int itemNumber;
	// Use this for initialization
	private List<Item> collectedItems;

	public float waitTimeToOpenItem = 3f;
	public int clickTimesToLoad;
	void Start () {
		reward = GameObject.FindObjectOfType<Reward>();
		rwLooter = rewardLooterGameObj.GetComponent<RewardLooter>();
		collectedItems = rwLooter.GetItemList();

		for(int itemIndex = 0; itemIndex < collectedItems.Count; itemIndex++){
			Item item = collectedItems[itemIndex];
			if(!item.isAlreadyInStock){
				print(item.itemType + " " + item.itemName + " is newly collected");
			}else{
				print(item.itemType + " " + item.itemName + " is re collected");
			}
			GameObject itemObjToDisplay = Instantiate(itemPref) as GameObject;
			itemObjToDisplay.transform.SetParent(itemPanel.transform.GetChild(itemIndex),false);
			itemObjToDisplay.transform.position = itemPanel.transform.GetChild(itemIndex).position;
		}


		itemNumber =  collectedItems.Count;
		print("there are " + itemNumber + " collected");


	}
	
	// Update is called once per frame
	void Update () {
		levelText.text = rwLooter.GetAcquiredGold().ToString();

		waitTimeToOpenItem -= Time.unscaledDeltaTime;
		if(waitTimeToOpenItem<=0){
			for(int itemIndex = 0; itemIndex < collectedItems.Count; itemIndex++){
				Item item = collectedItems[itemIndex];
				//get the whole body of item to be displayed
				Transform displayingItem = itemPanel.transform.GetChild(itemIndex).GetChild(0);

				//get the sprite part
				Transform itemImgObj = displayingItem.GetChild(0);
				if(!item.isAlreadyInStock){
					itemImgObj.GetComponent<Image>().sprite = item.itemImg;
				}else{
					itemImgObj.GetComponent<Image>().sprite = goldSprtForReplace;
					Text replaceGold = displayingItem.GetChild(1).GetComponent<Text>();
					replaceGold.text = item.itemValue.ToString();
				}
			}
		}
	}

	//Load level when click on Next button
	public void LoadLevelOfNextBtn(){
		int currentWorldIndex = PlayerProgress.presentWorldIndex;
		string lvName = "02 MapSelect World " +MapDictionary.worldList[currentWorldIndex]; 
		//each time clicked, reduce click time by 1
		clickTimesToLoad--;
		//after 1 click, unbox all the items
		if(clickTimesToLoad== 1){
			waitTimeToOpenItem = 0;
		}
		//after unbox all the items, load level
		if((clickTimesToLoad<= 0) || (waitTimeToOpenItem <= 0)){
			reward.DestroyReward();
			LevelManager.SLoadLevel(lvName);
		}
	}
}
