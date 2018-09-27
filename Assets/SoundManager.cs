using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	/*public AudioClip stabHit;
	public AudioClip levelUp;
	public AudioClip scoreRise;
	public AudioClip airSlide;
	public AudioClip miss;
	public AudioClip newGame;
	public AudioClip gameOver;
	public AudioClip buttonTouch;
	public AudioClip bgMusic;*/

	public AudioClip[] audioclips;

	public enum clips{
		STAB,
		LVUP,
		SCORERISE,
		AIRSLIDE,
		MISS,
		NEWGAME,
		GAMEOVER,
		BTNTOUCH,
		BGM
	}
	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public static void Play2DClipAtPoint(AudioClip clipToPlay)
	{
	    //  Create a temporary audio source object
		GameObject tempAudioSource = new GameObject(clipToPlay.name);

	    //  Add an audio source
	    AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();

	    //  Add the clip to the audio source
		audioSource.clip = clipToPlay;

	    //  Set the volume
	    audioSource.volume = 1f;

	    //  Set properties so it's 2D sound
	    audioSource.spatialBlend = 0.0f;

	    //  Play the audio
	    audioSource.Play();

	    //  Set it to self destroy
		Destroy(tempAudioSource, clipToPlay.length);

	}

}
