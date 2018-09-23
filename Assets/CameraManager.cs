﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public float timeWaitToAttachView;
	private Camera mainCamera;
    private Camera targetCamera;

    public float camThreshold; //TODO change name later
	private Transform attachedFollowDashCamPosition;

	private bool isDashed = false;
	private Vector3 camStartPosition;
	// Use this for initialization
	void Start () {
        ReGetCameras();

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
			if(Mathf.Abs(mainCamera.transform.localPosition.z - attachedFollowDashCamPosition.localPosition.z)< camThreshold){
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
	}

	public void ResetCameraPosition(){
		mainCamera.transform.position = camStartPosition;
	}

	public void ChangeToTargetCamera(){
        targetCamera = GameObject.FindGameObjectWithTag("Target Camera").GetComponent<Camera>();

        mainCamera.enabled = false;
		targetCamera.enabled = true;

	}

	public void ChangeToMainCamera(){
        targetCamera = GameObject.FindGameObjectWithTag("Target Camera").GetComponent<Camera>();

        mainCamera.enabled = true;
		targetCamera.enabled = false;
    }

    public void ReGetCameras()
    {
        mainCamera = Camera.main;
        targetCamera = GameObject.FindGameObjectWithTag("Target Camera").GetComponent<Camera>();

    }

}

