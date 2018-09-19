using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleScroller : MonoBehaviour {
	/// <summary>
	/// Public Variable
	/// </summary>
	[Tooltip("the amount of left-right movement before the state become static, or is not dragging ")]
	public float offsetToStationary;
	[Tooltip("How many vehicle avar can be displayed in the view when masked")]
	public int numberOfAvarToDisplay;
	[Tooltip("the GameObject for holding playable vehicle avar list")]
	public GameObject vehListFrame;
	[Tooltip("the list of total playable characters of the whole game")]
	public GameObject[] totalVehAvars;

	//the list of actual playable characters
	private GameObject[] vehicleAvar;

	private RectTransform scrollerFrame;
	//the rect frame for holding characters in vehicleAvar Array
	private RectTransform vehicleFrameRect;
	private float maxWidth;
	private float xPosOfFirst;
	// Use this for initialization
	void Start () {
		FilterAvailableVehs();
		//Connect to proper Object Components
		vehicleFrameRect = vehListFrame.GetComponent<RectTransform> ();
		scrollerFrame = GetComponent<RectTransform> ();
		
		//Get the width of one char avar which is allowed to displayed in scroll
		float widthOfOneAva = scrollerFrame.rect.width / numberOfAvarToDisplay;

		//Set the max width of char avar frame
		//Calculate the max width to be set
		if (numberOfAvarToDisplay > vehicleAvar.Length) {
			maxWidth = scrollerFrame.rect.width;
		}else{
			maxWidth = widthOfOneAva * (vehicleAvar.Length);}

		//If there is no vehicle at all, quit the process
		if (vehicleAvar.Length <= 0) {
			return;
		}

		//Set the width to the frame
		vehicleFrameRect.sizeDelta = new Vector2 (maxWidth, vehicleAvar [0].GetComponent<RectTransform> ().rect.height);

		//Set the X position of the first avar
		xPosOfFirst = -vehicleFrameRect.rect.width/2 + widthOfOneAva/2;

		//Set the root position of char avar frame so that the first avar will be displayed on the left edge
		vehicleFrameRect.anchoredPosition = new Vector2 (xPosOfFirst + widthOfOneAva * vehicleAvar.Length, 0f);

		//Instantiate all avars repeatly
		for(int i = 0; i <vehicleAvar.Length; i++){
			GameObject thisAvar = Instantiate(vehicleAvar[i]) as GameObject; 
			thisAvar.transform.position = new Vector3 (xPosOfFirst, 0, 0);
			thisAvar.transform.SetParent(vehListFrame.transform, false) ;

			//set the next position for the next char avar instantiating
			xPosOfFirst += widthOfOneAva;
		}
	} 


	private void FilterAvailableVehs(){
		int dataTotalNumberOfVehs = PlayerProgress.playerData.availableVehicles.Count; 
		vehicleAvar = new GameObject[dataTotalNumberOfVehs];
		for (int i = 0; i < dataTotalNumberOfVehs; i++){
			string nameInData = PlayerProgress.playerData.availableVehicles[i];
			for(int j = 0; j < totalVehAvars.Length; j ++){  
				string nameInList = totalVehAvars[j].GetComponent<VehicleAvarToSelect>().GetVehiclePrefabName();
				if(nameInData == nameInList){ 
					vehicleAvar[i] = totalVehAvars[j];
				}
			}
		}
	}
}
