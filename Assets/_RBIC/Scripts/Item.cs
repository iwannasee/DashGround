using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public class Item{
	public string itemName;
	public Sprite itemImg;
	public int itemValue;
	public enum TYPE {
		CHARACTER,
		VEHICLE,
		SHOT,
		MAP,
		GOLD
	};
	public TYPE itemType;
	public bool isAlreadyInStock;
}


public class ItemInit : MonoBehaviour {
	public Item item = new Item();
}




