using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

	public AudioClip[] levelClips;

	private AudioSource audioSource;
	private bool isOptScreen = false;

	void Awake(){
		DontDestroyOnLoad (gameObject);
		audioSource = GetComponent<AudioSource> ();
	}

	void Start () {
		
		PlayerPrefManager.SetMasterVolume (audioSource.volume);
	}

	// Update is called once per frame
	void Update () {
		ChangeVolume( PlayerPrefManager.GetMasterVolume ());
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += MusicPlayer;
	}

	void MusicPlayer(Scene scene, LoadSceneMode mode)
	{
		int level = scene.buildIndex;

		if (level == 2) {
			isOptScreen = true;
			return;
		}
		if (level == 1 && isOptScreen) {return;}

		//Set music corresponding to level index normally
		AudioClip thisLevelMusic = levelClips [0];
		//set range of gameplay levels
		if (level >= 5 && level <= 8) {
			 thisLevelMusic = levelClips [4];
		} else{
			 thisLevelMusic = levelClips [level];
		} 

		if (thisLevelMusic) {
			print(audioSource);
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}else{
			Debug.Log("no music found");
		}
	}

	public void ChangeVolume(float volume)
	{
		audioSource.volume = volume;
	}
	/*
	void OnLevelWasLoaded(int level)
	{
		//Set music transitting between home and option not interrupted
		if (level == 2) {
			isOptScreen = true;
			return;
		}
		if (level == 1 && isOptScreen) {return;}

		//Set music corresponding to level index normally
		AudioClip thisLevelMusic = levelClips [0];
		//set range of gameplay levels
		if (level >= 4 && level <= 8) {
			 thisLevelMusic = levelClips [4];
		} else{
			 thisLevelMusic = levelClips [level];
		} 
		if (thisLevelMusic) {
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}*/


}
