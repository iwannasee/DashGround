using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScroller : MonoBehaviour {
	/// <summary>
	/// Public Variable
	/// </summary>
	[Tooltip("the amount of left-right movement before the state become static, or is not dragging ")]
	public float offsetToStationary;
	[Tooltip("How many prisoner avar can be displayed in the view when masked")]
	public int numberOfAvarToDisplay;
	[Tooltip("the GameObject for holding playable character avar list")]
	public GameObject charListFrame;
	[Tooltip("the list of total playable characters of the whole game")]
	public GameObject[] totalCharAvars;

	//the list of actual playable characters
	private GameObject[] charAvar;

	private RectTransform scrollerFrame;
	//the rect frame for holding characters in charAvar Array
	private RectTransform charFrameRect;
	private float maxWidth;
	private float xPosOfFirst;
	static private bool isDragging = false;
	// Use this for initialization
	void Start () {
		FilterAvailableChars();
		//Connect to proper Object Components
		charFrameRect = charListFrame.GetComponent<RectTransform> ();
		scrollerFrame = GetComponent<RectTransform> ();
		
		//Get the width of one char avar which is allowed to displayed in scroll
		float widthOfOneAva = scrollerFrame.rect.width / numberOfAvarToDisplay;

		//Set the max width of char avar frame
		//Calculate the max width to be set
		if (numberOfAvarToDisplay > charAvar.Length) {
			maxWidth = scrollerFrame.rect.width;
		}else{
			maxWidth = widthOfOneAva * (charAvar.Length);}
		//Set the width to the frame
		charFrameRect.sizeDelta = new Vector2 (maxWidth, charAvar [0].GetComponent<RectTransform> ().rect.height);

		//Set the X position of the first avar
		xPosOfFirst = -charFrameRect.rect.width/2 + widthOfOneAva/2;

		//Set the root position of char avar frame so that the first avar will be displayed on the left edge
		charFrameRect.anchoredPosition = new Vector2 (xPosOfFirst + widthOfOneAva * charAvar.Length, 0f);

		//Instantiate all avars repeatly
		for(int i = 0; i <charAvar.Length; i++){
			GameObject thisAvar = Instantiate(charAvar[i]) as GameObject; 
			thisAvar.transform.position = new Vector3 (xPosOfFirst, 0, 0);
			thisAvar.transform.SetParent(charListFrame.transform, false) ;

			//set the next position for the next char avar instantiating
			xPosOfFirst += widthOfOneAva;
		}
	} 

	void Update(){
		if((- offsetToStationary <= charFrameRect.anchoredPosition.x) &&
		(charFrameRect.anchoredPosition.x <= offsetToStationary)){
			isDragging = false;
		}else{
			isDragging = true;
		}
	}

	public static bool GetIsDragging(){
		return isDragging;
	} 

	private void FilterAvailableChars(){
		int dataTotalNumberOfChars = PlayerProgress.playerData.availableCharacters.Count; 
		charAvar = new GameObject[dataTotalNumberOfChars];
		for (int i = 0; i < dataTotalNumberOfChars; i++){
			string nameInData = PlayerProgress.playerData.availableCharacters[i];
			for(int j = 0; j < totalCharAvars.Length; j ++){  
				string nameInList = totalCharAvars[j].GetComponent<CharacterAvarToSelect>().GetPrisonerPrefabName();
				if(nameInData == nameInList){ 
					print(nameInList +" found");
					charAvar[i] = totalCharAvars[j];
				}
			}
		}
	}
}
