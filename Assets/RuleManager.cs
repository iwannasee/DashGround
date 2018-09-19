using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour {
	private int[] requireScoreToPassLevel = new int[] {
		500, 		// to pass level 1 
		1700, 		// more to pass level 2
		3500,		// more to pass level 3 
		5500,		// more to pass level 4 
		8000,		// more to pass level 5 
		12500,		// more to pass level 6 
		17800,		// more to pass level 7 
		25000,		// more to pass level 8 
		35000,		// more to pass level 9
		50000		// more to pass level 10 
	};

	private int currentLevel = 0;
	public bool bIsRescueMode = false; // TODO Make private later
	public bool bIsAimGameMode = true; // TODO Make private later

	private GameController gameController;

	// Use this for initialization
	void Start () { 
		gameController = FindObjectOfType<GameController>();
	}

	public bool CheckLevelPassableBaseOnScore(int score){
		if(score >= requireScoreToPassLevel[currentLevel]){
			//currentLevel++;
			return true; //Now can move to next level
		}else{
			return false;
		}

	}

	public int GetCurrentLevel(){
		return currentLevel;
	}

    public int GetRequireScoreOfLevel(int level)
    {
        if (level < requireScoreToPassLevel.Length)
        {
            return requireScoreToPassLevel[level];
        }
        else
        {
            return 99999;
        }
        
    }

}
