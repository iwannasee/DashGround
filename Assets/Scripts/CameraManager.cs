using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public GameObject dashEffectPref;
	public float timeWaitToAttachView;
	public float maxLockOnTargetTime;
	public float rotationSpeedWhenStart;
	private Camera mainCamera;
    private Camera targetCamera;

	[Tooltip("the distance which anounces the arrow is launched if reached")]
    public float thresholdToChangeCamView; 
	public float thresholdToGetReady;
	private Transform attachedFollowDashCamPosition;

	private bool isDashed = false;

	private bool bIsRotateFirstTime = false;

	private Vector3 camStartPosition;
	private Quaternion camStartRotation;

	private bool cameraReady = false;
	private float lockOnPosToStartLinePosSpeed;

	private float lockOnTargetTime; 

	// Use this for initialization
	void Start () {
        ReGetCameras();

        ChangeToMainCamera();

		camStartPosition = mainCamera.transform.position;
		//camStartRotation = mainCamera.transform.rotation;
		camStartRotation = mainCamera.transform.rotation;
		if(!mainCamera){
			Debug.Log("Cameras Not Properly set");
		}
		lockOnTargetTime = maxLockOnTargetTime;
		ExposeCameraToTarget(16f);

		Vector3 newCamRot = new Vector3(-50f,0f,0f);
		mainCamera.transform.rotation = Quaternion.Euler(newCamRot);

	}

	void Update(){
		if(!GameController.GetIsGameStarting()){
			return;
		}
		if(!bIsRotateFirstTime){
			mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, camStartRotation, rotationSpeedWhenStart*Time.deltaTime);
			rotationSpeedWhenStart += Time.deltaTime;
			if( Mathf.Abs(mainCamera.transform.rotation.eulerAngles.x - camStartRotation.eulerAngles.x) <= 0.1f){
				bIsRotateFirstTime = true;
			}
			return; // remove this return if want to merge rotate time with lock on time
		} 
		 
		lockOnTargetTime -= Time.deltaTime;
		if(lockOnTargetTime<=0){
			if(!cameraReady){
				lockOnPosToStartLinePosSpeed += Time.deltaTime;
				mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camStartPosition,lockOnPosToStartLinePosSpeed*0.1f);
				if(Mathf.Abs(mainCamera.transform.position.z - camStartPosition.z) <= thresholdToGetReady){
					cameraReady = true;
				}
			}
		}


		//Actual Smooth change view process
		if(isDashed){
			mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, 
															attachedFollowDashCamPosition.localPosition, 0.1f);
			if(Mathf.Abs(mainCamera.transform.localPosition.z - attachedFollowDashCamPosition.localPosition.z)< thresholdToChangeCamView){
				isDashed = false;
			}
		}
	}

	public void StopWindSound(){
		GetComponent<AudioSource>().Stop();
	}

	public void PlayWindSound(){
		GetComponent<AudioSource>().Play();
				
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

		TurnOnDashingEffect();
        //set key to run actual smooth view changing
        isDashed = true;
	}

	public void ResetCameraPosition(){
		mainCamera.transform.position = camStartPosition;
	}

	public void ChangeToTargetCamera(){
		FindToSetTargetCamera();
        mainCamera.transform.GetChild(0).gameObject.SetActive(false);
        mainCamera.enabled = false;
		targetCamera.enabled = true;

	}

	public void ChangeToMainCamera(){
		FindToSetTargetCamera();

        mainCamera.enabled = true;
		targetCamera.enabled = false;
    }

    public void ReGetCameras()
    {
        mainCamera = Camera.main;
		FindToSetTargetCamera();

    }

    private void TurnOnDashingEffect(){ 
		mainCamera.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void TurnOffDashingEffect(){
		mainCamera.transform.GetChild(0).gameObject.SetActive(false);
    }

	public void ExposeCameraToTarget(float lockOnTargetDistance){
		TargetBoard targetBoard = FindObjectOfType<TargetBoard>();
		if(!targetBoard){
			print("no target board found");
			return;
		}

		Transform targetTransform = targetBoard.transform.parent;
		Vector3 positionToSet = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z - lockOnTargetDistance); //16f); //0.31f);
		mainCamera.transform.position = positionToSet;
    }

   public bool GetCamReady(){
   		return cameraReady;
   }

   public void ResetCameraReady(){
   		lockOnPosToStartLinePosSpeed = 0;
   		cameraReady = false;
   		lockOnTargetTime = maxLockOnTargetTime;
   }


    private void FindToSetTargetCamera(){
		targetCamera = GameObject.FindGameObjectWithTag("Target Camera").GetComponent<Camera>();

    }
}

