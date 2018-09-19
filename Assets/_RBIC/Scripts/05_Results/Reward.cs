using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour {
	public Item[] items;
	public int rewardGold;
	private bool looted = false;
	/// <summary>
	/// The looted for test. Delete this later
	/// </summary>
	public bool lootedForTest;

	/*public string[] rewardChar; 
	[Tooltip("Make sure this image array has the same element number as Reward Char's")]
	//public Sprite[] correspondCharImg;
	public string[] rewardVehicle;
	[Tooltip("Make sure this image array has the same element number as Reward Vehicle's")]
	//public Sprite[] correspondVehicleImg;*/

	void Start(){
		DontDestroyOnLoad(gameObject);

		looted = lootedForTest;
	}

	public void SetHadLooted(){
		looted = true;
	}
	public bool GetHadLooted(){
		return looted;
	}

	public void DestroyReward(){
		DestroyImmediate(gameObject,true);
	}
}

