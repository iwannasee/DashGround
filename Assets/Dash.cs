using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

	public float launchSpeed = 0.0f;
	public float pushingForce = 1.0f; 

	private  bool isDashed = false;
	private Rigidbody rigidBody;
	private CameraManager camManager;

	private bool bCanInput = true;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		camManager = GameObject.FindObjectOfType<CameraManager>();
	} 
	
	// Update is called once per frame
	void Update () {

		if(bCanInput){
			if(Input.GetKey(KeyCode.Space)){
                rigidBody.isKinematic = false;
                Launch();
            } 
			//Navigate the directions
			if(Input.GetKey(KeyCode.LeftArrow)){
				PushForce(pushingForce, -transform.right);
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				PushForce(pushingForce, transform.right);
			}
			if(Input.GetKey(KeyCode.UpArrow)){
				PushForce(pushingForce, transform.up);
			}
			if(Input.GetKey(KeyCode.DownArrow)){ 
				PushForce(pushingForce, -transform.up);
			}
		}


	}

	void OnCollisionEnter(Collision collision){
		rigidBody.isKinematic = true;
	}


	public bool GetIsDashed(){
		return isDashed;
	}

	void Launch(){
		StartRotate();
		//guard check
		if(!rigidBody){ 
			Debug.Log("Not found rigidbody in the dash object");
			return; 
		}
		//launch dash by increasing velocity
		rigidBody.velocity = transform.forward*launchSpeed;
		camManager.AttachViewToDashObject();

		isDashed = true; 
	}

	private void PushForce(float force, Vector3 direction){
		//guard check
		if(!rigidBody){
			Debug.Log("Not found rigidbody in the dash object");
			return;
		}
		rigidBody.AddForce(force*direction);
	}

	private void StartRotate(){
		GetComponent<RotateOverTime>().StartRotate();
	}

	public void DestroyDashGameObject(){
		Destroy(this.gameObject);
	}

	public void DisableInput(){
		bCanInput = false;
	}

	public void EnableInput(){
		bCanInput = true;
	}
}
