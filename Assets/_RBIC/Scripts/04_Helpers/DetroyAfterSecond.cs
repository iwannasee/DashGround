using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyAfterSecond : MonoBehaviour {
	public float lastingTime;
	// Use this for initialization
	void Start () {
		Destroy(gameObject,lastingTime);
	}
	

}
