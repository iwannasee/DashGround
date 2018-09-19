using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {
	public GameObject pauseMenu;
	public GameObject gameOverMenu;
	public Transform displayingText;
	// Use this for initialization
	void Start () {
		pauseMenu.SetActive(false);
		gameOverMenu.SetActive(false);
		displayingText.gameObject.SetActive(false);
	}

	void Update(){
		PauseHanding ();
	}


	public void DisplayNotifText(){
		displayingText.gameObject.SetActive(true) ;
	} 


	public void DisplayGameOverButtons(){
		gameOverMenu.SetActive(true);
	}

	public void ResumeGame ()
	{
		Time.timeScale = 1;
		ExitPauseMenu();
	}
	//---------------------------------------------------------------
	//---------------------------------------------------------------
	private void PauseHanding ()
	{
		if (Input.GetKeyUp (KeyCode.Escape) && (Time.timeScale == 1)) {
			PauseGame ();
			print("PauseGame!");
		}else if (Input.GetKeyUp (KeyCode.Escape) && (Time.timeScale == 0)) {
			ResumeGame ();
			print("resume!");
		}
	}
	//---------------------------------------------------------------
	private void PauseGame ()
	{
		Time.timeScale = 0;
		DisplayPauseMenu();
	}
	//---------------------------------------------------------------
	private void DisplayPauseMenu ()
	{
		pauseMenu.SetActive(true);
	}
	//---------------------------------------------------------------
	private void ExitPauseMenu ()
	{
		pauseMenu.SetActive(false);
	}
}
