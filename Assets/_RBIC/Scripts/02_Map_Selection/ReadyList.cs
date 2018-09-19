using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyList : MonoBehaviour {
	public GameObject testPuppet;
	public GameObject characterScrollList;
	public int numberOfPlayable = 4;
	private float intervalDistance;

	public static GameObject[] prisonersWillPlay;
	public static GameObject vehicleWillPlay;
	public GameObject[] prisoners;
	// Use this for initialization
	void Start () {
		//renew this array every time the selection scene start. the number [1] is just a random number
		prisonersWillPlay = new GameObject[1];

		intervalDistance = GetComponent<RectTransform>().rect.width / numberOfPlayable;
		float xPosOfFirst = (-GetComponent<RectTransform>().rect.width/2) + intervalDistance/2;
		for(int i = 0; i <numberOfPlayable; i++){
			GameObject thisAvar = Instantiate(testPuppet) as GameObject; 
			thisAvar.transform.position = new Vector3 (xPosOfFirst, 0, 0);
			thisAvar.transform.SetParent(transform, false) ;

			//set the next position for the next char avar instantiating
			xPosOfFirst += intervalDistance;
		}
	}



	public void AddPrisonerImage(Sprite prisonerImg, GameObject prisonerPref){
		for(int i = 0; i<numberOfPlayable; i++){
			Transform childBody = transform.GetChild(i);
			Transform readyPrisonerImageObj = childBody.GetChild(1);
			Image readyPrisonerImage = readyPrisonerImageObj.GetComponent<Image>();
			Transform substractMarkObj = childBody.GetChild(3);

			if(!readyPrisonerImage.sprite || !substractMarkObj.gameObject.activeInHierarchy){
				readyPrisonerImage.enabled = true;
				readyPrisonerImage.preserveAspect = true;
				readyPrisonerImage.sprite = prisonerImg;

				substractMarkObj.GetComponent<Image>().enabled = true;
				childBody.GetComponent<ReadyCharAvar>().SetReadyPrisonerPrefab(prisonerPref);
				return;
			}
		}
	}

	public void AddVehicleImage(Sprite frontImg, Sprite backImg){
		for(int i = 0; i<numberOfPlayable; i++){
			Transform childBody = transform.GetChild(i);

			Transform frontVehImageObj = childBody.GetChild(0);
			Image frontVehImage = frontVehImageObj.GetComponent<Image>();

			Transform backVehImageObj = childBody.GetChild(2);
			Image backVehImage = backVehImageObj.GetComponent<Image>();

			frontVehImage.enabled = true;
			frontVehImage.preserveAspect = true;
			frontVehImage.sprite = frontImg;

			backVehImage.enabled = true;
			backVehImage.preserveAspect = true;
			backVehImage.sprite = backImg;
		}
	}

	public void SubtractReadyPrisoner(Sprite readySprite){
		for(int i = 0; i < characterScrollList.transform.childCount; i++){
			Transform thisChild = characterScrollList.transform.GetChild(i);
			Image thisGameObjImg = thisChild.GetComponent<Image>();
			if(thisGameObjImg.sprite == readySprite){
				Debug.Log("SelectableEffect "+thisChild.name);
				thisChild.GetComponent<CharacterAvarToSelect>().SelectableEffect();
			}
		}
	}

	public void SubtractReadyVehicle(Sprite readySprite){
		for(int i = 0; i < characterScrollList.transform.childCount; i++){
			Transform thisChild = characterScrollList.transform.GetChild(i);
			Image thisGameObjImg = thisChild.GetComponent<Image>();
			if(thisGameObjImg.sprite == readySprite){
				Debug.Log("SelectableEffect "+thisChild.name);
				thisChild.GetComponent<CharacterAvarToSelect>().SelectableEffect();
			}
		}
	}



	public void GetPrisonerWillPlayList(){
		prisonersWillPlay = new GameObject[transform.childCount];
		for(int i = 0; i < transform.childCount; i++){
			prisonersWillPlay[i] = null;
			Transform thisPrisoner = transform.GetChild(i);
			ReadyCharAvar readyChar= thisPrisoner.GetComponent<ReadyCharAvar>();
			prisonersWillPlay[i] = readyChar.GetReadyPrisonerPrefab();
			if(prisonersWillPlay[i]){
				Debug.Log("Playable Char "+prisonersWillPlay[i].name);
			}
		}
	}

	public void GoTheBattle(){
		int actualPrisonerCount = 0;

		for(int i = 0; i < prisonersWillPlay.Length; i++){
			if(prisonersWillPlay[i] != null){
				actualPrisonerCount ++;
			}
		}

		if(actualPrisonerCount <=0){
			//TODO display warning message here	
			Debug.Log("actualPrisonerCount " + actualPrisonerCount +" ->You have to select at least one Prisoner");
			return;
		}
		//only start battle if there is at least one prisoner selected and vehicle selected
		if(vehicleWillPlay){
			string mapToPlay = GameObject.FindObjectOfType<MapSelectManager>().GetSelectedMap();
			int currentWorld = PlayerProgress.presentWorldIndex;
			LevelManager.SLoadLevel("03 World "+currentWorld + " "+ mapToPlay);
		}else{
			//TODO display warning message here
			Debug.Log("You have to select at least one Vehicle");
		}

	}

	public void SetVehicleWillPlay(GameObject vehiclePref){
		vehicleWillPlay = vehiclePref;
		
	}
}
