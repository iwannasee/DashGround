using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelectManager : MonoBehaviour {
	//public Button selectLocationBtn;
	//public Button inputBtn;
	//public Button searchBtn;
	public GameObject mapPanel;
	public GameObject inputPanel;
	public GameObject searchPanel;
	public GameObject mapLocation;
	public GameObject goBtn;
	public GameObject charSelectGameObj;
	public GameObject mapSelectGameObj;
	private bool isFirstDisplay = true;
	private string selectedMap;
	void Start(){
		HideInputPanel ();
		HideSearchPanel ();
		//HideMapSelectPanel ();
	}

	//---------------------------------------------------------------
	public void DisplaySearchPanel(){
		HideInputPanel ();
		HideMapSelectPanel ();
		searchPanel.SetActive(true);
	}
	//---------------------------------------------------------------
	private void HideSearchPanel(){
		searchPanel.SetActive(false);
	}

	/*****************************************************************************************
	::::::::::::::::This is the area of functioning CAMPAIGN BUTTON::::::::::::::::::::::::::
	******************************************************************************************/
	public void DisplayMapSelectPanel(){
		HideInputPanel ();
		HideSearchPanel ();
		mapPanel.SetActive(true);
	}

	//---------------------------------------------------------------
	public void DisplayGoButton(){
		GetComponent<BoxCollider2D>().enabled = true;
		goBtn.SetActive(true);
	}

	//---------------------------------------------------------------
	public void LoadMap(){
		ScrollSnapRect scrollSnapObj = GameObject.FindObjectOfType<ScrollSnapRect>();
		if(!scrollSnapObj){
			Debug.LogError("No scrollSnapRect Object found");
			return;
		}
		if(scrollSnapObj.GetDragStatus() == false) //if the page is not being dragged
		{
			int pageIndex;
			pageIndex = scrollSnapObj.GetCurrentPage();
			SceneManager.LoadScene ("03 Level " + pageIndex.ToString());
		}
	}
	//---------------------------------------------------------------
	private void OnMouseDown(){
		if(goBtn.activeInHierarchy){
			if(goBtn.GetComponent<MyUIHoverListener>().GetIsMouseHovering() == false){
				HideGoButton();
			}
		}
	}
	//---------------------------------------------------------------
	private void HideMapSelectPanel(){
		mapPanel.SetActive(false);
	}
	//---------------------------------------------------------------

	private void HideGoButton(){
		GetComponent<BoxCollider2D>().enabled = false;
		goBtn.SetActive(false);
	}

	//---------------------------------------------------------------
	/*****************************************************************************************
	::::::::::::::::This is the area of functioning INPUT BUTTON::::::::::::::::::::::::::::::
	******************************************************************************************/
	public void DisplayInputPanel(){
		if (!TextInputController.newlyAdded && !isFirstDisplay) {
			inputPanel.GetComponent<TextInputController> ().ResetNewlyInputState ();
		}
		 
		HideMapSelectPanel ();
		HideSearchPanel ();
		inputPanel.SetActive(true);
		isFirstDisplay = false;
	}
	//---------------------------------------------------------------
	private void HideInputPanel(){
		inputPanel.SetActive(false);
	}
	//---------------------------------------------------------------
	public void GetTextFromInputField(){
		string input = "";
		//Find the input field game object
		//As there s only Input Field GameObject as child of the input panel, its index is 0
		GameObject inputFieldGameObj = inputPanel.transform.GetChild (0).gameObject;
		TextInputController inputController = inputFieldGameObj.GetComponent<TextInputController> ();
		if (inputController.GetIsTextCommitable ()) {
			InputField inputField = inputFieldGameObj.GetComponent<InputField> ();
			input = inputField.text;
			inputField.text = "";
		}
		Debug.Log ("input is: " + input);
		//return input;
	}


	public void ChangeToCharacterSelect(){
		mapSelectGameObj.SetActive(false);
		charSelectGameObj.SetActive(true);
		ScrollSnapRect scrollSnapRect  = mapPanel.GetComponentInChildren<ScrollSnapRect>();
		if(scrollSnapRect){
			selectedMap = scrollSnapRect.GetCurrentMapName();
			Debug.Log(selectedMap);
		}else{
			Debug.Log("not found scroll snap rect object to get map name");
		}
	}

	public string GetSelectedMap(){
		return selectedMap;
	}
}
