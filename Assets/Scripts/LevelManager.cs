using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public void LoadLevel(string name){
		SceneManager.LoadScene (name);

	}

	public void QuitRequest(){
	}

	public void LoadNextlevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	public static void SLoadLevel(string name){
		SceneManager.LoadScene (name);

	}

	public void RetryLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
