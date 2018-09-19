using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingShot : MonoBehaviour {
	public float speed = 5f;
	// Use this for initialization
	
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Brick")||
			col.gameObject.CompareTag("Prisoner")||
			col.gameObject.CompareTag("Prisoner Paddle")) {
			Destroy (gameObject);
		}
	}

}
