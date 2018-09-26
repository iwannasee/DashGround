using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {

	public float minRot = 10f;
	public float maxRot = 360f;

	[Tooltip("How fast to reach the max rotation")]
	public float speedToRotate = 0.5f;

	private float rotateRate;
	private float interpolator = 0.0f;

	private Transform dashedTransform;
	private Dash dash;
	private bool bCanRotate = false;

	// Use this for initialization
	void Start () {
		dash = GetComponent<Dash>();
		dashedTransform = transform.GetChild(0);
		//dashedTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!GetIsKinematic() && bCanRotate){
			dashedTransform.Rotate(Vector3.forward, Time.deltaTime*rotateRate);
			rotateRate = Mathf.Lerp(minRot,maxRot* Random.value, interpolator); 
			interpolator += speedToRotate*Time.deltaTime;
		}
	}

	public void StartRotate(){
		bCanRotate = true;
	}

	private bool GetIsKinematic(){
		return dash.GetComponent<Rigidbody>().isKinematic;
	}
} 
