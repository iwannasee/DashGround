using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour {

    private GameController gameController;
    private RuleManager ruleMng;

    private Slider slider;	
	private bool bCanUpdate = true;
    private bool canRenewTarget = false;
       
	private int scoreObtained;
	private int currentLevel; 

    //Implement on Inspector
    public Image fillImage;
    public Text levelText;
    public Animator poppingScoreAnimator;
	public float fillingEffectDuration = 1f;

	public AudioClip newLevelClip;
	public AudioClip scoreRaisingClip;
	AudioSource audioSource;

    // Use this for initialization
    void Start () {
		levelText.text =  "Level " + (currentLevel + 1).ToString();

        gameController = FindObjectOfType<GameController>();
        ruleMng = FindObjectOfType<RuleManager>();
		audioSource =  GetComponent<AudioSource>();
        slider = GetComponent<Slider>();
    }

    private void PlayAudioClip(AudioClip clipToPlay){
		audioSource.clip = clipToPlay;
		audioSource.loop = false;
        audioSource.Play();
    }

    void Update(){

        if (bCanUpdate)
        {
            fillingEffectDuration -= Time.deltaTime;

            int currentLevelRequireScore = ruleMng.GetRequireScoreOfLevel(currentLevel);

            float valueToPutInSlider = (float)scoreObtained / (float)currentLevelRequireScore;


            slider.value += Time.deltaTime * valueToPutInSlider;
			audioSource.volume = valueToPutInSlider;
            if (slider.value >= 1f)
            {
            	//Play separated sound of level up
				Play2DClipAtPoint(newLevelClip);

                canRenewTarget = true;

                slider.value = 0f;
                currentLevel++;
                gameController.RewardDashOnLevelUp(currentLevel);
                levelText.text = "Level " + (currentLevel + 1).ToString();
            }


            fillImage.color = new Color(fillImage.color.r,
                                        fillImage.color.g,
                                        slider.value,
                                        fillImage.color.a);

            if (fillingEffectDuration <= 0f)
            {
                fillingEffectDuration = 1;
                bCanUpdate = false;
				audioSource.Stop();

            }
        }
    } 
     
    public void UpdateSlider( int ScoreToUpdate) 
    { 
		PlayAudioClip(scoreRaisingClip);
		scoreObtained = ScoreToUpdate;
		bCanUpdate = true;
    }

    public void PopTheScore(int scoreToPop)
    {
        Text scoreText = poppingScoreAnimator.GetComponent<Text>();
        scoreText.text = "+" + scoreToPop.ToString();
        poppingScoreAnimator.SetTrigger("popScoreTrigger");

    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public bool GetCanRenewTarget()
    {
        return canRenewTarget;
    }

    public void DisableRenewTarget()
    {
        canRenewTarget = false;
    }

	public void Play2DClipAtPoint(AudioClip clip)
	{
	    //  Create a temporary audio source object
	    GameObject tempAudioSource = new GameObject("TempAudio");

	    //  Add an audio source
	    AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();

	    //  Add the clip to the audio source
	    audioSource.clip = clip;

	    //  Set the volume
	    audioSource.volume = 1f;

	    //  Set properties so it's 2D sound
	    audioSource.spatialBlend = 0.0f;

	    //  Play the audio
	    audioSource.Play();

	    //  Set it to self destroy
	    Destroy(tempAudioSource, clip.length);

	}
}
