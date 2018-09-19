using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class MyUIHoverListener : MonoBehaviour {
	private bool isMouseHovering;
	// Use this for initialization
	//---------------------------------------------------------------
	void OnMouseOver(){
		isMouseHovering = true;
	}
	//---------------------------------------------------------------
	void OnMouseExit(){
		isMouseHovering = false;
	}
	//---------------------------------------------------------------
	public bool GetIsMouseHovering(){
		return isMouseHovering;
	}
}
