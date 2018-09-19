using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour {
	public const int coinValue = 5;
	public float fallingSpeed;

	void Update(){
		transform.Translate (Vector3.down * Time.deltaTime * fallingSpeed);
	}

	public void CoinDestroy(){
		Destroy(gameObject);
	}
}
