﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private static bool bGameStart = false;
	// Use this for initialization
	private bool missSoundPlaying = false;
	private Dash dash;
	private CameraManager camManager;
    private TargetBoard targetBoard;
    private float waitTimeToRefressCounter;
    private ScoreSlider scoreSlider;

    [Tooltip("How long to wait until the next dashing turn")]
	public float waitTimeToRefresh = 1f;
	[Tooltip("How long to wait until the next dashing turn")]
	public float viewChangeDistance = 20f;
    public float targetDashDistance;

	public GameObject dashObjectPref;
    public Text scoreText;
    public Text dashesLeftText;
    public RectTransform startGamePanel;

    /*Rules for game. Move to level manager later*/
    public int numberToDashRemaining = 2;
	public int scoreOfThisLevel = 0;

	void Start () {
		scoreSlider = FindObjectOfType<ScoreSlider>();
		if(dashesLeftText){
        	dashesLeftText.text = "Dashes left: " + numberToDashRemaining.ToString();
        } 
        waitTimeToRefressCounter = waitTimeToRefresh;
		targetBoard = GameObject.FindObjectOfType<TargetBoard>();
		dash = GameObject.FindObjectOfType<Dash>();
		camManager = GameObject.FindObjectOfType<CameraManager>();
	}
	 
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && !bGameStart){
			bGameStart = true;
			startGamePanel.gameObject.SetActive(false);
		}

		if (!bGameStart){return;}

		if(!dash){return;}

		if(!dash.GetIsDashed()){return;}

        float dashZPos = dash.transform.position.z;
		float zPosToChangeView = targetBoard.transform.position.z - viewChangeDistance;

		bool dashHitSomething = dash.GetIsDashed() && dash.IsKinematic();
		//Change camera view if approach near the target 
		//Or if dash hit something
		if(dashZPos >= zPosToChangeView || dashHitSomething){
			dash.DisableInput();
			camManager.StopWindSound();
			camManager.ChangeToTargetCamera();

			if(targetBoard.GetTargetIsHit()){
				AddScore(targetBoard.GetScore());
                UpdateSlider();
				RenewTarget(); 
			}
            
            //when dash reach the target-like distance, handle the result
			if (dashZPos >= targetBoard.transform.position.z || dashHitSomething){
				if(dashZPos >= targetBoard.transform.position.z && !missSoundPlaying){
					//TODO play miss sound here
					print("Play miss sound");
					AudioSource.PlayClipAtPoint(dash.missHit, dash.transform.position, 1f);
					missSoundPlaying = true;
				}

                PrepareForNextDash();

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
			camManager.ResetCameraPosition(); //TODO seem dedundants, consider to remove

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
		GameObject dashGameObject = (GameObject) Instantiate(dashObjectPref, this.transform.position, this.transform.rotation);
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
			camManager.ExposeCameraToTarget(16f);
			camManager.ResetCameraReady();

            targetBoard = GameObject.FindObjectOfType<TargetBoard>();

            /*Camera cam = GameObject.FindGameObjectWithTag("Target Camera").GetComponent<Camera>();
            cam.enabled = false;
            camManager.ChangeToMainCamera();*/

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

	public static bool GetIsGameStarting(){
		return bGameStart;
	}
} 
