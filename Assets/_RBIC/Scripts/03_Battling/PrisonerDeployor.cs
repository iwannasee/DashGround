using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerDeployor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if ((ReadyList.prisonersWillPlay.Length > 0) && (ReadyList.vehicleWillPlay)) {
			int totalNumber = ReadyList.prisonersWillPlay.Length;
			for (int i = 0; i < totalNumber; i++) {
				GameObject vehicle = Instantiate (ReadyList.vehicleWillPlay, transform.GetChild (i).position, Quaternion.identity) as GameObject;
				if (ReadyList.prisonersWillPlay [i]) {
					GameObject prisoner = Instantiate (ReadyList.prisonersWillPlay [i], transform.GetChild (i).position, Quaternion.identity) as GameObject;
					prisoner.transform.SetParent (vehicle.transform);
				} else {
					Debug.Log ("The position " + i + " is blank");
				}
				vehicle.transform.SetParent (transform.GetChild (i));
			}
		} else {
			Debug.Log ("there is no character or no vehicle selected");
		}
	}
	
}
