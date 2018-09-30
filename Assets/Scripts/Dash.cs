using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Dash : MonoBehaviour {

	public float launchSpeed = 0.0f;
	public float pushingForce = 1.0f; 
	private bool bCanInput = true;
	private  bool isDashed = false;
	private Rigidbody rigidBody;
	private CameraManager camManager;
	private Animator dashAnimator;
	public AudioClip stabHit;
	public AudioClip missHit;
	public AudioClip winding;
	public AudioClip launch;

    private Button UpBtn;
    private Button DownBtn;
    private Button LeftBtn;
    private Button RightBtn;


    // Use this for initialization
    void Start () {
        //ButtonSetup();
       
        rigidBody = GetComponent<Rigidbody>();
		camManager = GameObject.FindObjectOfType<CameraManager>();
		dashAnimator = GetComponent<Animator>();
	} 
	
	// Update is called once per frame
	void Update () {

        if (!StartPanel.GetGameStart())
        {
			return;
		}

		bool camReady = camManager.GetCamReady();
		
		if(camReady && bCanInput && StartPanel.GetGameStart())
        {
			if(Input.GetMouseButtonDown(0) && rigidBody.isKinematic==true){
                rigidBody.isKinematic = false;
                dashAnimator.SetTrigger("launch");
            }

            //Navigate the directions by arrow 
            /*
            if (Input.GetKey(KeyCode.LeftArrow)){
				PushForce(pushingForce, -transform.right);
			}

            if (Input.GetKey(KeyCode.RightArrow)){
				PushForce(pushingForce, transform.right);
			}
			if(Input.GetKey(KeyCode.UpArrow)){
				PushForce(pushingForce, transform.up);
			}
			if(Input.GetKey(KeyCode.DownArrow)){ 
				PushForce(pushingForce, -transform.up);
			}*/
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

	public void DisableInput(){
		bCanInput = false;
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

    public void PushForceDashRight()
    {
        print("Right");
        PushForce(pushingForce, transform.right);
    }

    public void PushForceDashLeft()
    {
        print("Left");
        PushForce(pushingForce, -transform.right);
    }

    public void PushForceDashUp()
    {
        print("Up");
        PushForce(pushingForce, transform.up);
    }

    public void PushForceDashDown()
    {
        print("Down");
        PushForce(pushingForce, -transform.up);
    }

    /*
    public void ButtonSetup()
    {
        RightBtn = GameObject.FindGameObjectWithTag("Right Button").GetComponent<Button>();
        LeftBtn = GameObject.FindGameObjectWithTag("Left Button").GetComponent<Button>();
        DownBtn = GameObject.FindGameObjectWithTag("Down Button").GetComponent<Button>();
        UpBtn = GameObject.FindGameObjectWithTag("Up Button").GetComponent<Button>();

        EventTrigger triggerUp = UpBtn.GetComponent<EventTrigger>();
        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerDown;
        entryUp.callback.AddListener((eventData) => { PushForceDashUp(); });
        triggerUp.triggers.Add(entryUp);
        
       
        EventTrigger triggerDown = DownBtn.GetComponent<EventTrigger>();
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((eventData) => { PushForceDashDown(); });
        triggerDown.triggers.Add(entryDown);

        EventTrigger triggerLeft = LeftBtn.GetComponent<EventTrigger>();
        EventTrigger.Entry entryLeft = new EventTrigger.Entry();
        entryLeft.eventID = EventTriggerType.PointerDown;
        entryLeft.callback.AddListener((eventData) => { PushForceDashLeft(); });
        triggerLeft.triggers.Add(entryLeft);

        
        EventTrigger triggerRight = RightBtn.GetComponent<EventTrigger>();
        EventTrigger.Entry entryRight = new EventTrigger.Entry();
        entryRight.eventID = EventTriggerType.PointerDown;
        entryRight.callback.AddListener((eventData) => { PushForceDashRight(); });
        triggerRight.triggers.Add(entryRight);
        //UpBtn.
    }*/

}
