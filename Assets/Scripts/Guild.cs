using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Guild : MonoBehaviour {

	public RectTransform guidePanel;
	// Use this for initialization
	void Start () {
		if(guidePanel){
			guidePanel.gameObject.SetActive(false);
		}
	}


	public void ToggleGuide()
	{
		if(guidePanel){
			if(guidePanel.gameObject.activeInHierarchy){
				guidePanel.gameObject.SetActive(false);
				Time.timeScale = 1;
			}else{
				guidePanel.gameObject.SetActive(true);
				Time.timeScale = 0;
			}
		}
	}


	public void QuitGame()
	{
		Application.Quit();
	}
}
