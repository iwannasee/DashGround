using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinLoseCondition : MonoBehaviour {
	public GameObject reward;
	public float regretfulTime = 5;
	private static bool isWinner;
	// Use this for initialization
	void Start () {
		isWinner = false;
	}

	void Update(){
		if(Prisoner.GetAllPrisonerDead()){
			regretfulTime -= Time.unscaledDeltaTime;
			if(regretfulTime<=0){
				int currentWorldIndex = PlayerProgress.presentWorldIndex;
				string lvName = "02 MapSelect World " + MapDictionary.worldList[currentWorldIndex]; 
				if (reward) {
					reward.GetComponent<Reward> ().DestroyReward ();
				}

				SceneManager.LoadScene(lvName);

			}
		}
	}

	public static bool GetIsWinner(){
		return isWinner;
	}

	public void Lose(){
		isWinner = false;
		PlayerPrefManager.SetUITextStatus (PlayerPrefManager.GUITEXT_STATUS_CHANGING);
		UITextController.SetUITextStatusType (UITextController.DISPLAY_TEXT.LOSE, "");
	}

	public void Win(){
		isWinner = true;
		PlayerPrefManager.SetUITextStatus (PlayerPrefManager.GUITEXT_STATUS_CHANGING);
		UITextController.SetUITextStatusType (UITextController.DISPLAY_TEXT.CLEAR, "");
	}

	/// <summary>
	/// Sets the is winner. For testing. Delete this later.
	/// </summary>
	/// <param name="win">If set to <c>true</c> window.</param>
	public static void SetIsWinner(bool win){
		isWinner = win;
	}
}
