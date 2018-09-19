using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUpChidren : MonoBehaviour {
	private EnemyWaveController waveController;
	// Use this for initialization
	void Start () {
		waveController = GameObject.FindGameObjectWithTag("Wave Controller").GetComponent<EnemyWaveController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(waveController.GetIsWaveCleared()){
			foreach(Transform child in this.transform){
				Destroy(child.gameObject);
			}
		}
	}
}
