using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputController : MonoBehaviour {
	/* Public variables*/
	public GameObject statusText;
	public GameObject inputFieldGameObj;
	public GameObject representMap;
	public GameObject mapList;
	public float maxDelayForRetry = 2;

	/* Private variables*/
	private MapDictionary mapDict;
	private Text textOfSttText;
	private InputField inputField;
	private string inputPassword;
	private string acquireMapName;
	private string introText = "Input Code to explore more Secret Places";
	private string wrongPassText = "Incorrect Password";
	private string correctPassText = "Correct! A destination found!";
	//EXTENDABLE #1 optionally disable input or just keep player able to input
	//private string allFoundText = "All The Hidden Places in this World Map are already acquired.";
	private string alreadyExistText = "You had found this place before.";
	private float delayForRetry;
	private bool isTextCommitable = false;
	private bool flagToStartDelay = false;
	private bool delayFromAlreadyInput = false;
	//EXTENDABLE #1 optionally disable input or just keep player able to input
	//private bool inputSucceeded = false;
	bool preventInputFlag = false;
	//TODO fix this static bool as it is dangerous
	public static bool newlyAdded = false;

	// Use this for initialization
	void Start () {
		mapDict = GameObject.FindObjectOfType<MapDictionary> ();
		inputField = inputFieldGameObj.GetComponent<InputField> ();
		textOfSttText = statusText.GetComponent<Text> ();

		//EXTENDABLE #1 optionally disable input or just keep player able to input
		/*inputSucceeded = mapDict.IsAllSecretMapRevealed();
		if(inputSucceeded){
			TurnOnOffInputFieldContent (true);
			SetSttText(hasExistedText, Color.white);
			inputField.interactable = false;
			preventInputFlag = false;
			return;
		}*/

		inputField.interactable = true;
		TurnOnOffMaptileDisplay (false);
		print (textOfSttText);
		textOfSttText.text = introText;
		delayForRetry = maxDelayForRetry;
	}
	void Update(){
		//EXTENDABLE #1 optionally disable input or just keep player able to input
		/*if (inputSucceeded && preventInputFlag){
			TurnOnOffMaptileDisplay (false);
			TurnOnOffInputFieldContent (true);
			SetSttText(hasExistedText, Color.white);
			inputField.interactable = false;
			preventInputFlag = false;
		}*/

		if (flagToStartDelay) {
			if(!delayFromAlreadyInput){
				SetSttText (wrongPassText, Color.red);
			}

			delayForRetry -= Time.deltaTime;
			if (delayForRetry <= 0) {
				SetSttText (introText, Color.white);
				inputField.interactable = true;
				flagToStartDelay = false;
				delayFromAlreadyInput = false;
			}
		}
	}

	public bool GetIsTextCommitable(){
		return isTextCommitable;
	}

	public void CheckInputIsFullfilled ()
	{
		//if input enough number, commit and compare
		if (inputField.text.Length >= inputField.characterLimit) {
			inputPassword = inputField.text;
			//compare here
			CompareWithPasswordList();
			isTextCommitable = true;
			return;
		}
		isTextCommitable = false;
	}

	private void CompareWithPasswordList(){
		//try to get mapname based on the password key input
		string getableMapName = mapDict.GetMapName(inputPassword);
		Debug.Log("getableMapName " + getableMapName);
		//if the input is correct, or the mapname exists in the dictionary
		if(getableMapName!= MapDictionary.WRONGINPUT){
			//and if the map had been acquired before
			if(mapDict.CheckAlreadyAcquired(getableMapName)){
				inputField.text = "";
				delayForRetry = maxDelayForRetry;
				flagToStartDelay = true;
				inputField.interactable = false;
				delayFromAlreadyInput = true; 
				SetSttText (alreadyExistText, Color.yellow);
			}else{
				acquireMapName = getableMapName;
				//Change status text
				SetSttText(correctPassText, Color.yellow);
				//Hide Image and Input field component to gain space for secret map tile
				TurnOnOffInputFieldContent(false);
				//turn on found secret map tile display
				TurnOnOffMaptileDisplay(true);
				//turn on corresponding dot on world map
				newlyAdded = true;
				//Add the corresponding map to the bottom end of Campaign list
				mapDict.SetMapDictValue(getableMapName);

				//if this is the last secret of the current world, disable input
				//EXTENDABLE #1 optionally disable input or just keep player able to input
				/*if(mapDict.IsAllSecretMapRevealed()){
					print("inputSucceeded " + inputSucceeded + "all map ok");
					inputSucceeded = true;
					inputField.interactable = false;
				}*/
				// Saving the input map
				SaveLoadSystem.SaveGame(PlayerProgress.playerData);
			}
		//if the input is not correct
		} else {
			inputField.text = "";
			print ("Incorrect Password");
			delayForRetry = maxDelayForRetry;
			flagToStartDelay = true;
			inputField.interactable = false;
		}
	}

	void SetSttText(string text, Color collor){
		textOfSttText.text = text;
		textOfSttText.color = collor;
	}

	void TurnOnOffInputFieldContent (bool onOrOff){
		inputField.gameObject.SetActive (onOrOff);
	}

	void TurnOnOffMaptileDisplay(bool onOrOff){
		if(onOrOff == true){
			for(int i = 0; i<mapList.transform.childCount; i++){
				Transform mapChild = mapList.transform.GetChild(i);
				if(acquireMapName == mapChild.GetComponent<MapTile>().GetMapName()){
					representMap.GetComponent<Image>().sprite = mapChild.GetComponent<Image>().sprite;
					break;
				}
			}
		}
		representMap.SetActive (onOrOff);
	}

	public void ResetNewlyInputState(){
		//once input is successful, no longer accepts input
		//EXTENDABLE #1 optionally disable input or just keep player able to input
		/*if (inputSucceeded){
			preventInputFlag = true;
			TurnOnOffMaptileDisplay (false);
			TurnOnOffInputFieldContent (false);
			SetSttText(hasExistedText, Color.white);
		}*/
		//whenever click on the input button, refresh it
		inputField.text = "";
		SetSttText (introText, Color.white);
		TurnOnOffMaptileDisplay (false);
		TurnOnOffInputFieldContent (true);


	}
}
	

