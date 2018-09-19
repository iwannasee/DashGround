using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour {

	//TODO implement name for maps later
	public string mapName;

	//---------------------------------------------------------------
	void OnMouseUp(){
		ScrollSnapRect scrollSnapRect = GameObject.FindObjectOfType<ScrollSnapRect>();
		if (!scrollSnapRect) {//in case displayed in Input Section
			return;
		}
		if(scrollSnapRect.GetDragStatus() == false){
			MapSelectManager mapMng = GameObject.FindObjectOfType<MapSelectManager>();
			mapMng.DisplayGoButton();
			PlayerPrefManager.SetCurrentLevelName(mapName);
		}
	}
	//---------------------------------------------------------------
	public void SetMapName(string t_mapName){
		mapName = t_mapName;
	}
	//---------------------------------------------------------------
	public string GetMapName(){
		return mapName;
	}
}
