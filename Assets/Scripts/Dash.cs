using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

	public float launchSpeed = 0.0f;
	public float pushingForce = 1.0f; 

	private  bool isDashed = false;
	private Rigidbody rigidBody;
	private CameraManager camManager;
	private Animator dashAnimator;
	public AudioClip stabHit;
	public AudioClip missHit;
	public AudioClip winding;
	public AudioClip launch;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		camManager = GameObject.FindObjectOfType<CameraManager>();
		dashAnimator = GetComponent<Animator>();
	} 
	
	// Update is called once per frame
	void Update () {
		bool camReady = camManager.GetCamReady();
		
		if(camReady){
			if(Input.GetKey(KeyCode.Space) && rigidBody.isKinematic==true){
                rigidBody.isKinematic = false;

                dashAnimator.SetTrigger("launch");
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
		AudioSource.PlayClipAtPoint(stabHit, transform.position, 1f);
		camManager.TurnOffDashingEffect();


	}


	public bool GetIsDashed(){
		return isDashed;
	}

	void Launch(){
		camManager.PlayWindSound();
		dashAnimator.enabled = false;
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

	public bool IsKinematic(){
		return rigidBody.isKinematic;
	}

	private void PlayWindArrowSound(){
		AudioSource.PlayClipAtPoint(winding, transform.position, 1f);
	}

	private void PlayLaunchSound(){
		AudioSource.PlayClipAtPoint(launch, transform.position, 1f);
	}
}
