using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour {

    private GameController gameController;
    private RuleManager ruleMng;
    private Slider slider;	
	private bool bCanUpdate = true;
	private int scoreObtained;
	private int currentLevel; 

    //Implement on Inspector
    public Image fillImage;
    public Text levelText;
    public Animator poppingScoreAnimator;
	public float fillingEffectDuration = 1f;



    // Use this for initialization
    void Start () {
		levelText.text =  "Level " + (currentLevel + 1).ToString();

        gameController = FindObjectOfType<GameController>();
        ruleMng = FindObjectOfType<RuleManager>();
        slider = GetComponent<Slider>();
    }

    void Update(){

		if(bCanUpdate){
			fillingEffectDuration-= Time.deltaTime;

			int currentLevelRequireScore = ruleMng.GetRequireScoreOfLevel(currentLevel);

			float valueToPutInSlider = (float)scoreObtained / (float) currentLevelRequireScore;

			slider.value += Time.deltaTime*valueToPutInSlider;
			if(slider.value >= 1f){

				slider.value = 0f;
				print("slider value over 1, reset to " + slider.value);
				currentLevel++;
				gameController.RewardDashOnLevelUp(currentLevel);
				levelText.text =  "Level " + (currentLevel + 1).ToString();
			}

			fillImage.color = new Color(fillImage.color.r, 
	                                    fillImage.color.g,
	                                    slider.value,
	                                    fillImage.color.a);

			if(fillingEffectDuration<= 0f){
				fillingEffectDuration = 1;
				bCanUpdate = false;

				print(scoreObtained + " " + currentLevelRequireScore);
			}
	      }
    } 
     
    public void UpdateSlider( int ScoreToUpdate) 
    { 
		scoreObtained = ScoreToUpdate;
		bCanUpdate = true;
    }

    public void PopTheScore(int scoreToPop)
    {
        Text scoreText = poppingScoreAnimator.GetComponent<Text>();
        scoreText.text = "+" + scoreToPop.ToString();
        poppingScoreAnimator.SetTrigger("popScoreTrigger");

    }

    public bool GetIsFillingEffect(){
    	return bCanUpdate;
    }
}
