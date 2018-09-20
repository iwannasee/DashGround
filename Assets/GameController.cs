﻿using System.Collections;
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



		float dashZPos = dash.transform.position.z;
		float zPosToChangeView = targetBoard.transform.position.z - viewChangeDistance;


		//Change camera view if approach near the target 
		if(dashZPos >= zPosToChangeView){
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
            if (dashZPos >= targetBoard.transform.position.z){
               
                CurrentDashResultHandle ();
            }
            else
            {
                print("12121sasddsadasdd");
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
	 
	private void CurrentDashResultHandle ()
	{
        
        if (!scoreSlider.GetIsFillingEffect() && numberToDashRemaining <= 0) {
			GameOver();
		} else {
			PrepareForNextDash (); 
		}
	}

	private void GameOver(){
		PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController> ();
		pauseMenuController.DisplayNotifText ();
		pauseMenuController.DisplayGameOverButtons ();
	}

	private void PrepareForNextDash(){
        
        waitTimeToRefressCounter -= Time.deltaTime;
		if(waitTimeToRefressCounter <= 0){
			camManager.ChangeToMainCamera();

			//Detach main camera from dash
			Camera.main.transform.SetParent(null);
			camManager.ResetCameraPosition();
			dash.DestroyDashGameObject();

			//renew IsHitTarget to false
			RenewTarget();
			SpawnNewDash();
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
		   
		//RuleManager ruleMng = FindObjectOfType<RuleManager>();
		//bool bCanMoveNextLevel = ruleMng.CheckLevelPassableBaseOnScore(scoreOfThisLevel);
		//increase dash stock based on level
		if(level > 7){
			numberToDashRemaining += 4;
		}else if (level > 4) {
			numberToDashRemaining += 3;
		}else{
			numberToDashRemaining += 2;
		}
	}
} 