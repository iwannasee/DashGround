using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour {
	public float fadeOutTime;
	private Image fadePanel;
	private Color currentColor = new Color(0f,0f,0f,0f);
	private bool fadable = false;
	private string levelNameToLoad = "";
	private OptionController optController;
	// Use this for initialization
	void Start () {
		optController = GameObject.FindObjectOfType<OptionController> ();
		if(!optController){
			Debug.Log("there is no Option controller found");
		}

		fadePanel = GetComponent<Image> ();
		if(!fadePanel){
		 	Debug.Log("there is no fade panel found");
		 	return;
		 }

		fadePanel.color = currentColor;
	}

	// Update is called once per frame
	void Update () {
		if (fadable) {
			float alphaChange = Time.deltaTime / fadeOutTime;
			currentColor.a += alphaChange; 
			fadePanel.color = currentColor;
			if (fadePanel.color.a >= fadeOutTime) {
				int worldIndex = PlayerProgress.presentWorldIndex;
				SceneManager.LoadScene (levelNameToLoad);
			}
		}
	}

	public void FadeOutAndLoadLevel(string levelName){
		fadePanel.enabled = true;
		fadable = true;
		levelNameToLoad = levelName;
	}

	public void FadeOutSaveExit(string levelName){
		optController.SaveOption ();
		fadePanel.enabled = true;
		fadable = true;
		levelNameToLoad = levelName;

	}

	public void QuitGame(){
		Application.Quit();
		Debug.Log("quit runs");
	}
}
