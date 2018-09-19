using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionController : MonoBehaviour {

	public Slider volumeSlider;
	public Slider difficultySlider;
	public LevelManager levelMng;

	private MusicManager musicMng;
	// Use this for initialization

	void Start () {
		musicMng = GameObject.FindObjectOfType<MusicManager> ();
		volumeSlider.value = PlayerPrefManager.GetMasterVolume ();
		difficultySlider.value = PlayerPrefManager.GetDifficulty ();
	}

	// Update is called once per frame
	void Update () {
		if (musicMng) {
			musicMng.ChangeVolume (volumeSlider.value);
		} else {
			Debug.LogError ("No Music Manager Found");
		}
	}

	public void SaveOption (){
		PlayerPrefManager.SetMasterVolume (volumeSlider.value);
		PlayerPrefManager.SetDifficulty (difficultySlider.value);
	}

	public void SetDefault(){
		volumeSlider.value = 0.5f;
		difficultySlider.value = 2f;

	}
}
