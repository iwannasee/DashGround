using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextController : MonoBehaviour {
	public string[] guiTextArray;
	public enum DISPLAY_TEXT{
		WAVE,
		START,
		CLEAR,
		CRITICAL_HIT,
		BRAVO,
		STAGE_NAME,	
		SKILL_NAME,
		LOSE,
		NOTHING
	};
	public float maxDisplayTime;
	public float prepareTime;
	//Time for display the UI text
	private int thisWaveNumber;
	private Text currentUiText;

	private bool isDisplayTimeOut = true;
	//flag that if the time from the start of scene is over the prepare time
	//if is the prepare time passed, set UI text to WAVE...
	private bool justStart = false;
	private bool isChangingWave = false;
	private float displayTime;
	private EnemyWaveController waveController;
	private float prepareTimeForNextWaveText;
	//the UI Text "Wave..." is displayed or not
	//static private bool specialTextEnd = false;
	//text value out side guiTextArray;
	private static string otherTextValue;
	private static DISPLAY_TEXT statusToSetText;

	public GameObject ballSpawner;
		//---------------------------------------------------------------
	// Use this for initialization
	void Start () {
		justStart = false;
		waveController = GameObject.FindGameObjectWithTag ("Wave Controller").GetComponent<EnemyWaveController> ();
		if (waveController) {
			thisWaveNumber = waveController.GetCurrentWaveIndex ();
		} else {
			Debug.Log ("enemy wave controller not found!");
		}
		displayTime = maxDisplayTime;
		currentUiText = GetComponent<Text>();
		SetText(PlayerPrefManager.GetCurrentLevelName());
	}
	//---------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > prepareTime) { 
			if (!justStart) {
				PlayerPrefManager.SetUITextStatus (PlayerPrefManager.GUITEXT_STATUS_CHANGING);
				UITextController.SetUITextStatusType (UITextController.DISPLAY_TEXT.WAVE, "");
			}
			justStart = true;
		}
		ChangeTextAfterSecond ();
		
		if(PlayerPrefManager.GetUITextStatus()){
			switch (statusToSetText){
			case DISPLAY_TEXT.STAGE_NAME:
				if(!isChangingWave){SetText(otherTextValue);}
				break;
			case DISPLAY_TEXT.START:
				if(!isChangingWave){SetText(guiTextArray[(int)DISPLAY_TEXT.START]);}
				break;	
			case DISPLAY_TEXT.CLEAR:
				//justStart = false;
				displayTime = 10;
				SetText(guiTextArray[(int)DISPLAY_TEXT.CLEAR]);
				break;
			case DISPLAY_TEXT.SKILL_NAME:
				if(!isChangingWave){SetText(otherTextValue);}
				break;	
			case DISPLAY_TEXT.CRITICAL_HIT:
				if(!isChangingWave){SetText(guiTextArray[(int)DISPLAY_TEXT.CRITICAL_HIT]);}
				break;	
			case DISPLAY_TEXT.BRAVO:
				if(!isChangingWave){SetText(guiTextArray[(int)DISPLAY_TEXT.BRAVO]);}
				break;	
			case DISPLAY_TEXT.WAVE:
				thisWaveNumber = GameObject.FindGameObjectWithTag ("Wave Controller").GetComponent<EnemyWaveController> ().GetCurrentWaveIndex ();
				SetText(guiTextArray[(int)DISPLAY_TEXT.WAVE]+" "+(thisWaveNumber+1).ToString());
				displayTime = prepareTime;
				isChangingWave = true;
				break;	
			case DISPLAY_TEXT.LOSE:
				displayTime = 10;
				SetText(guiTextArray[(int)DISPLAY_TEXT.LOSE]);
				break; 
			default:
				//if(!isChangingWave){SetText("No Title");}
				break;		
			}
			isDisplayTimeOut = false;
			PlayerPrefManager.SetUITextStatus(PlayerPrefManager.GUITEXT_STATUS_CHANGE_END);
		}
	}
	//---------------------------------------------------------------
	void SetText(string text){
		currentUiText.text = text;
	}
	//---------------------------------------------------------------
	public static DISPLAY_TEXT GetUITextStatusType(){
		return statusToSetText;
	}
	//---------------------------------------------------------------
	public static void SetUITextStatusType(DISPLAY_TEXT textTypeToDisplay, string text){
		statusToSetText = textTypeToDisplay;
		otherTextValue = text;
	}
	//---------------------------------------------------------------
	void ChangeTextAfterSecond ()
	{
		if (isDisplayTimeOut == false) {
			displayTime -= Time.deltaTime;
			//TODO replace statusToSetText with another flag as it could be change when 
			//activating skill shot for example
			// When the "WAVE" UI Text display time is out
			if ((displayTime <= 0 && (statusToSetText == DISPLAY_TEXT.WAVE)) ||
			(displayTime <= 0 &&(statusToSetText == DISPLAY_TEXT.CRITICAL_HIT)) ) {
				Debug.LogWarning ("go true");
				SetText ("");
				isDisplayTimeOut = true;
				//specialTextEnd = true;
				waveController.ResetIsWaveCleared ();
				isChangingWave = false;
				displayTime = maxDisplayTime;

				//turn on the next enemy wave display
				waveController.SetActiveWaveChild();
				//reposition the stage ball and push it as the previous wave 
				ballSpawner.GetComponent<BallSpawner> ().ResetBallAtWaveStart ();
				statusToSetText = DISPLAY_TEXT.NOTHING;
			} else if (displayTime <= 0) {
				SetText ("");
				isDisplayTimeOut = true;
				displayTime = maxDisplayTime;
			}
		} 
	}
	/*---------------------------------------------------------------
	public static bool GetWaveTextDisplayEnd(){
		return specialTextEnd;
	}
	//---------------------------------------------------------------
	public static void SetWaveTextDisplayEnd(){
		specialTextEnd = false;
	}
	//---------------------------------------------------------------*/
	public bool GetJustStart(){
		return justStart;
	}
	//---------------------------------------------------------------
	public void SetJustStart(){
		justStart = false;
	}
	//---------------------------------------------------------------
	public bool GetIsChangeWave(){
		return isChangingWave;
	}
}
