using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// Use this for initialization
	private Dash dash;
	private CameraManager camManager;
    private TargetBoard targetBoard;
    private float waitTimeToRefressCounter;
    private ScoreSlider scoreSlider;
    public GameObject dashObject;
    public Text scoreText;
    public Text dashesLeftText;

    [Tooltip("How long to wait until the next dashing turn")]
	public float waitTimeToRefresh = 1f;
	[Tooltip("How long to wait until the next dashing turn")]
	public float viewChangeDistance = 20f;
    public float targetDashDistance;

    /*Rules for game. Move to level manager later*/
    public int numberToDashRemaining = 2;
	public int scoreOfThisLevel = 0;

	void Start () {
		scoreSlider = FindObjectOfType<ScoreSlider>();
        dashesLeftText.text = "Dashes left: " + numberToDashRemaining.ToString();
        waitTimeToRefressCounter = waitTimeToRefresh;
		targetBoard = GameObject.FindObjectOfType<TargetBoard>();
		dash = GameObject.FindObjectOfType<Dash>();
		camManager = GameObject.FindObjectOfType<CameraManager>();
	}
	 
	// Update is called once per frame
	void Update () {
		if(!dash){
            return;}
		if(!dash.GetIsDashed()){
             return;}

        //camManager.ReGetCameras();

        float dashZPos = dash.transform.position.z;
		float zPosToChangeView = targetBoard.transform.position.z - viewChangeDistance;

		bool dashHitSomething = dash.GetIsDashed()&&dash.IsKinematic();
		//Change camera view if approach near the target 
		//Or if dash hit something
		if(dashZPos >= zPosToChangeView || dashHitSomething){
			dash.DisableInput(); 
			camManager.ChangeToTargetCamera();

			if(targetBoard.GetTargetIsHit()){
				AddScore(targetBoard.GetScore());

                UpdateSlider();
                //CheckLevelPassableAndUpdateHUD();
				RenewTarget(); 
			}
            //print("ISDash not available");
            //when dash reach the target-like distance, handle the result
			if (dashZPos >= targetBoard.transform.position.z || dashHitSomething){
                PrepareForNextDash();

               // CurrentDashResultHandle ();
            }
		}   
	
	}

    private void UpdateSlider()
    {
        ScoreSlider scoreSlider = FindObjectOfType<ScoreSlider>();
       
        int scoreToPop = targetBoard.GetScore();
		scoreSlider.UpdateSlider(scoreToPop);
        scoreSlider.PopTheScore(scoreToPop);
    }
	 

	private void GameOver()
    {
        PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
        pauseMenuController.DisplayNotifText();
        pauseMenuController.DisplayGameOverButtons();
	}


    //wait for  some seconds while calculating result, then prepare setting for the next dash
	private void PrepareForNextDash(){
        //set clock
        waitTimeToRefressCounter -= Time.deltaTime;

		if(waitTimeToRefressCounter <= 0){
            //Check for gameover
            if(numberToDashRemaining <=0)
            {
                GameOver();
                return;
            }

			camManager.ChangeToMainCamera();
            print("refreshing");
            //Detach main camera from dash
			Camera.main.transform.SetParent(null);
			camManager.ResetCameraPosition();
			dash.DestroyDashGameObject();
            SpawnNewDash();
            CheckLevelUpToReNewTarget();

            ReFindDashObject();
  
            waitTimeToRefressCounter = waitTimeToRefresh;	 
		}

	}

	private void SpawnNewDash(){
		numberToDashRemaining--;
		dashesLeftText.text = "Dashes left: " + numberToDashRemaining.ToString(); 
		GameObject dashGameObject = (GameObject) Instantiate(dashObject, this.transform.position, this.transform.rotation);
        
	}

	private void ReFindDashObject(){
		dash = GameObject.FindObjectOfType<Dash>();
	}
	 
	private void RenewTarget(){
		targetBoard.ResetTarget();
	}

    private void CheckLevelUpToReNewTarget()
    {
        RuleManager ruleMng = FindObjectOfType<RuleManager>();
        if (scoreSlider.GetCanRenewTarget())
        {
            print("now spawn new target");
            targetBoard.DestroyTarget();
            
            //FindObjectOfType<TargetSpawner>().SpawnNewTargetWithGivenLevel(scoreSlider.GetCurrentLevel());
			TargetSpawner targetSpawner =FindObjectOfType<TargetSpawner>();
			targetSpawner.SpawnNewTargetOnTrunkWithLevel(scoreSlider.GetCurrentLevel());
            targetBoard = GameObject.FindObjectOfType<TargetBoard>();
            Camera cam = GameObject.FindGameObjectWithTag("Target Camera").GetComponent<Camera>();
            cam.enabled = false;
            camManager.ChangeToMainCamera();

            scoreSlider.DisableRenewTarget();

        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = scoreOfThisLevel.ToString();
    }

	public void AddScore(int scoreToAdd){
		scoreOfThisLevel += scoreToAdd;
        UpdateScoreText();
    }

	public int GetCurrentScore(){
		return scoreOfThisLevel;
	}

	public void RewardDashOnLevelUp(int level){
		//increase dash stock based on level
		int dashBonus = 0;
		if(level > 7){
			dashBonus = 4;
		}else if (level > 4) {
			dashBonus = 3;
		}else{
			dashBonus = 2;
		} 

		numberToDashRemaining += dashBonus;
	}
} 
