using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public float timeWaitToAttachView;
	private Camera mainCamera;
	public Camera targetCamera;
	private Transform attachedFollowDashCamPosition;

	private bool isDashed = false;
	private Vector3 camStartPosition;
	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		ChangeToMainCamera();

		camStartPosition = mainCamera.transform.position;
		if(!mainCamera){
			Debug.Log("Cameras Not Properly set");
		}
	}

	void Update(){

		//Actual Smooth change view process
		if(isDashed){
			mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, 
															attachedFollowDashCamPosition.localPosition, 0.1f);
			if(mainCamera.transform.localPosition == attachedFollowDashCamPosition.localPosition){
				isDashed = false;
			}
		}
	}


	public void AttachViewToDashObject(){
		StartCoroutine("SmoothChangeView");
	}
	 
	IEnumerator SmoothChangeView(){
		//time to wait before view change happens
		yield return new WaitForSeconds(timeWaitToAttachView);

		//Get the target position so that the main camera will smoothly transit to
		GameObject followCamPoint = GameObject.FindGameObjectWithTag("FollowPointForCamera");
		attachedFollowDashCamPosition = followCamPoint.transform;

		mainCamera.transform.SetParent(attachedFollowDashCamPosition.parent);
		//set key to run actual smooth view changing
		isDashed = true;
		Debug.Log("attaching view");
	}

	public void ResetCameraPosition(){
		mainCamera.transform.position = camStartPosition;
	}

	public void ChangeToTargetCamera(){
		mainCamera.enabled = false;
		targetCamera.enabled = true;
	}

	public void ChangeToMainCamera(){
		mainCamera.enabled = true;
		targetCamera.enabled = false;
	}

}

