using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOfPrisoner : MonoBehaviour {
	// Use this for initialization
	public static int ShelterBrickCount = 0;
	public int maxHit;
	private int timesHit;
	
	//---------------------------------------------------------------
	void Start () {
		timesHit = 0;	
		ShelterBrickCount++;
	}
	//---------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		TimesHitHandle ();
	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.CompareTag ("Projectile Ball")) {
			timesHit = timesHit + 1;
		} else if (collision.gameObject.CompareTag ("Enemy Shot")) {
			timesHit = timesHit + 1;
		}
	}
	//---------------------------------------------------------------
	void TimesHitHandle(){ 
		if (timesHit >= maxHit) {
			Destroy (gameObject);
			ShelterBrickCount--;
		}
	}
}
