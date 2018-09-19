using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
	public float fadeInTime;

	private Image fadePanel;
	private Color currentColor = Color.black;
	private bool fadeInCompleted = false;
	// Use this for initialization 
	void Start () {
		fadePanel = GetComponent<Image> ();
		fadePanel.color = currentColor;
	}
	
	// Update is called once per frame
	void Update () {
		if (!fadeInCompleted) {
			float alphaChange = Time.deltaTime / fadeInTime;
			currentColor.a -= alphaChange;
			fadePanel.color = currentColor;
			if (fadePanel.color.a <= 0f) {
				fadeInCompleted = true;
				fadePanel.enabled = false;
			}
		}
	}
}
