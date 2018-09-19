using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour {

    private GameController gameController;
    private RuleManager ruleMng;
    private Slider slider;
    private int currentLevelRequireScore;
    private int currentScore;
 
    //Implement on Inspector
    public Image fillImage;
    public Animator poppingScoreAnimator;

    // Use this for initialization
    void Start () {
        gameController = FindObjectOfType<GameController>();
        ruleMng = FindObjectOfType<RuleManager>();
        slider = GetComponent<Slider>();
    }
    
    public void UpdateSlider( int currentScore)
    {
        
        currentLevelRequireScore = ruleMng.GetRequireScoreOfLevel(ruleMng.GetCurrentLevel());
        float valueToPutInSlider = (float)currentScore / (float) currentLevelRequireScore;
        if (valueToPutInSlider >= 1)
        {
            slider.value = valueToPutInSlider - 1;
        }
        else
        {
            slider.value = valueToPutInSlider;
        }

        fillImage.color = new Color(fillImage.color.r,
                                    fillImage.color.g,
                                    slider.value,
                                    fillImage.color.a);
    }

    public void PopTheScore(int scoreToPop)
    {
        Text scoreText = poppingScoreAnimator.GetComponent<Text>();
        scoreText.text = "+" + scoreToPop.ToString();
        poppingScoreAnimator.SetTrigger("popScoreTrigger");

    }
}
