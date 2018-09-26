using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBoard : MonoBehaviour {

	public float sweetPointThreshold = 0.001f;
	public float sweetPointValue = 0.05f;

	public int scoreRate = 100;

	private int scoreObtained = 0;

	private bool bTargetIsHit = false;

	void OnCollisionEnter(Collision collision){

		if(collision.gameObject.GetComponent<Dash>()){
			bTargetIsHit = true;
			collision.transform.parent = transform;
			Vector3 hitPos = collision.transform.localPosition;
			Vector3 centerPos = new Vector3(0,0,hitPos.z);

			float distanceToCenter = Vector3.Distance(hitPos, centerPos);
			if(distanceToCenter < sweetPointThreshold){
				distanceToCenter = sweetPointValue;
			}

			scoreObtained = (int)(scoreRate/distanceToCenter);
			if(scoreObtained >= scoreRate/sweetPointValue){
				Debug.Log("distanceToCenter " + distanceToCenter);
				scoreObtained =(int)( scoreRate/sweetPointValue);
			}
			Debug.Log("score obtained : " + scoreObtained);
		}

		 
	}

	public int GetScore(){

		return scoreObtained;
	}

	public bool GetTargetIsHit(){
		return bTargetIsHit;
	}


	//reset targetIsHit property
	public void ResetTarget(){
		bTargetIsHit = false;
	}

    public void DestroyTarget()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
