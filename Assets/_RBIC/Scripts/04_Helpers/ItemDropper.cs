using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour {
	public GameObject CoinPref;
	public GameObject others;
	[Range(0.0f, 0.9f)]
	public float rateForDropItem;

	public void DropItem(){
		if (Random.value > rateForDropItem) {
			Instantiate (CoinPref, transform.position, Quaternion.identity);
		} else if (Random.value > rateForDropItem) {
			Instantiate (CoinPref, transform.position, Quaternion.identity);
		}
	}
}
