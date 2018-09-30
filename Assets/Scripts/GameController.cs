using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
	// Use this for initialization
	private bool missSoundPlaying = false;
    private bool buttonHolding = false;
    private static bool isGameOver = false;
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
	//public GameObject dashesLeftText;
    public RectTransform startGamePanel;
    public GameObject Buttons;
    /*Rules for game. Move to level manager later*/
    public int numberToDashRemaining = 2;
	public int scoreOfThisLevel = 0;

	void Start () {
        isGameOver = false;
        Buttons.SetActive(false);
        UpdateScoreText();
        scoreSlider = FindObjectOfType<ScoreSlider>();
        dashesLeftText.text = "x " + numberToDashRemaining.ToString();
        waitTimeToRefressCounter = waitTimeToRefresh;
		targetBoard = GameObject.FindObjectOfType<TargetBoard>();
		dash = GameObject.FindObjectOfType<Dash>();
		camManager = GameObject.FindObjectOfType<CameraManager>();
	}
	 
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && !StartPanel.GetIsScrollingUp()){
            startGamePanel.GetComponent<Animator>().SetTrigger("StartGame");
            StartPanel.SetIsScrollingUp();
		}

		if (!StartPanel.GetGameStart()) {return;}

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
        isGameOver = true;
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
            //Detach main camera from dash
			Camera.main.transform.SetParent(null);
			camManager.ResetCameraPosition(); //TODO seem dedundants, consider to remove

			dash.DestroyDashGameObject();
            SpawnNewDash();
            buttonHolding = false;

            CheckLevelUpToReNewTarget();

            ReFindDashObject();
  
            waitTimeToRefressCounter = waitTimeToRefresh;	 
		}

	}

	private void SpawnNewDash(){
		numberToDashRemaining--;
		dashesLeftText.text = "x " + numberToDashRemaining.ToString();
		Instantiate(dashObjectPref, this.transform.position, this.transform.rotation);
	}

	private void ReFindDashObject(){
		dash = GameObject.FindObjectOfType<Dash>();
	}
	 
	private void RenewTarget(){
		targetBoard.ResetTarget();
	}

    private void CheckLevelUpToReNewTarget()
    {
        if (scoreSlider.GetCanRenewTarget())
        { 
            targetBoard.DestroyTarget();
			TargetSpawner targetSpawner =FindObjectOfType<TargetSpawner>();
			targetSpawner.SpawnNewTargetOnTrunkWithLevel(scoreSlider.GetCurrentLevel());
			camManager.ExposeCameraToTarget(16f);
			camManager.ResetCameraReady();
            targetBoard = GameObject.FindObjectOfType<TargetBoard>();
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

    public void ActivateButtons()
    {
        Buttons.SetActive(true);
        //dash.ButtonSetup();
    }

    public void DeactivateButton()
    {
        Buttons.SetActive(false);
    }

    public static bool GetIsGameOver()
    {
        return isGameOver;
    }

    /// <summary>
    /// control dash button here
    /// </summary>
    public void OnPressRight()
    {
        buttonHolding = true;
        StartCoroutine(PushForceDashRight());
    }

    public void OnPressLeft()
    {
        buttonHolding = true;
        StartCoroutine(PushForceDashLeft());
    }

    public void OnPressUp()
    {
        buttonHolding = true;
        StartCoroutine(PushForceDashUp());
    }

    public void OnPressDown()
    {
        buttonHolding = true;
        StartCoroutine(PushForceDashDown());
    }

    public void OnReleaseRight()
    {
        buttonHolding = false;
    }

    public void OnReleaseLeft()
    {
        buttonHolding = false;
    }

    public void OnReleaseUp()
    {
        buttonHolding = false;
    }

    public void OnReleaseDown()
    {
        buttonHolding = false;
    }

    IEnumerator PushForceDashRight()
    {
        while (buttonHolding)
        {
            //OnHold.Invoke();
            dash.PushForceDashRight();

            // do any custom "OnHold" behavior here

            yield return null; // makes the loop wait until next frame to continue
        }
        
    }

    IEnumerator PushForceDashLeft()
    {
        while (buttonHolding)
        {
            //OnHold.Invoke();
            dash.PushForceDashLeft();

            // do any custom "OnHold" behavior here

            yield return null; // makes the loop wait until next frame to continue
        }
        
    }

    IEnumerator PushForceDashUp()
    {
        while (buttonHolding)
        {
            //OnHold.Invoke();
            dash.PushForceDashUp();

            // do any custom "OnHold" behavior here

            yield return null; // makes the loop wait until next frame to continue
        }
        
    }

    IEnumerator PushForceDashDown()
    {
        while (buttonHolding)
        {
            //OnHold.Invoke();
            dash.PushForceDashDown();

            // do any custom "OnHold" behavior here

            yield return null; // makes the loop wait until next frame to continue
        }
    }

} 
