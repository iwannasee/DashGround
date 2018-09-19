using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCharAvar : MonoBehaviour {
	private GameObject readyPrisonerPref;
	private GameObject readyList;
	// Use this for initialization
	void Start () {
		readyList = GameObject.FindGameObjectWithTag("Ready List");
	}
	
	public void SubtractFromReadyList(){
		Transform mainReadyAvar = transform.GetChild(1);
		Image thisReadyAvarImg = mainReadyAvar.GetComponent<Image>();
		readyList.GetComponent<ReadyList>().SubtractReadyPrisoner(thisReadyAvarImg.sprite);

		//Disable ReadyState
		thisReadyAvarImg.sprite = null;
		thisReadyAvarImg.enabled = false;
		readyPrisonerPref = null;
		HideSubtractMark();
	} 

	private void HideSubtractMark(){
		Transform substractMark = transform.GetChild(3);
		substractMark.GetComponent<Image>().enabled = false;
	}


	public GameObject GetReadyPrisonerPrefab(){
		return readyPrisonerPref;
	}

	public void SetReadyPrisonerPrefab(GameObject prisonerPref){
		readyPrisonerPref = prisonerPref;
		Debug.Log("Pass prisoner " + readyPrisonerPref+ " success");
	}
}
