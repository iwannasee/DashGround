using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour {
	private bool isRotating = false;
	private Vector3 objectPosition;
	private Vector3 mousePosition;
	private float angle;

	// Update is called once per frame
	void Update () {
		if (isRotating) {
			RotateBaseOnMouse ();
		}
	}

	void OnMouseDown(){
		// rotating flag
		isRotating = true;
	}

	void OnMouseUp(){
		// rotating flag
		isRotating = false;
	}

	void OnMouseExit(){
		isRotating = false;
	}

	void RotateBaseOnMouse ()
	{
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();
		float rotation_z = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rotation_z);
	}
}
