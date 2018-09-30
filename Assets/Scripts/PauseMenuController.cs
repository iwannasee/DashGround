using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {
	public GameObject gameOverMenu;
    public Guild guide;
	// Use this for initialization
	void Start () {
		gameOverMenu.SetActive(false);
	}

	void Update(){
        if (StartPanel.GetGameStart())
        {
            PauseHanding();
        }
		
	}

	public void DisplayGameOverButtons(){
		gameOverMenu.SetActive(true);
	}

	//---------------------------------------------------------------
	//---------------------------------------------------------------
	private void PauseHanding ()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) {
            guide.ToggleGuide();

            print("PauseGame!");
		}
	}
	//---------------------------------------------------------------
	//---------------------------------------------------------------
}
