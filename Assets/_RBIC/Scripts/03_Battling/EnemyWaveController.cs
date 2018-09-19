using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWaveController : MonoBehaviour {
	//private PlayerProgress playerProgress;
	public GameObject RewardLoot;
	public GameObject enemyWaveSpawner;
	public GameObject pauseMenu;
	public float congratulationTime;
	private string mapName;
	private int waveLength;
	private int currentWaveIndex;
	private Transform currentWaveChild;
	private bool isWaveCleared = false;
	private bool firstChildOn = false;
	private bool saved = false;
	private UITextController uiText;
	private static bool waveHasStarted = false;

	//---------------------------------------------------------------
	// Use this for initialization
	void Start () { 
		waveHasStarted = false;

		pauseMenu.SetActive(false);
		/*GameObject ProgressObj = GameObject.FindGameObjectWithTag("Player Progress");
		if(ProgressObj){
			playerProgress = ProgressObj.GetComponent<PlayerProgress>();
		}else{
			Debug.Log("Progress Holder not found");
		}*/
		mapName = PlayerPrefManager.GetCurrentLevelName();

	//TODO relocate this code
		uiText = GameObject.FindGameObjectWithTag("UI Text").GetComponent<UITextController>();
		currentWaveIndex = 0;
		waveLength = enemyWaveSpawner.transform.childCount;

		//First Set all the child waves inactive in the hierachy (hide them)
		if(waveLength>=0){
			foreach (Transform thisWave in enemyWaveSpawner.transform){
				thisWave.gameObject.SetActive(false);
			}
		}
	}
	//---------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		//Because all the wave childs are deactived at start, now display the first child
		//Display it when the flag JustStart (set if the time from the scene load pass the prepare time) on and no child is turned on yet
		if(uiText.GetJustStart() && !firstChildOn){
			currentWaveChild = enemyWaveSpawner.transform.GetChild(currentWaveIndex); //currenwaveIndex now is still 0
			if (!uiText.GetIsChangeWave ()) {
				currentWaveChild.gameObject.SetActive (true);
				waveHasStarted = true;

				firstChildOn = true; 
			} //turn on child display
		}
		WaveChangeHandling ();
		PauseHanding ();

		//If all the wave were cleared and data was saved, then do congratulation and move to result screen
		if(saved){
			congratulationTime -= Time.unscaledDeltaTime;
			if(congratulationTime<=0){
				MoveToResultScreen();
			}
		}

	}
	//---------------------------------------------------------------
	public int GetCurrentWaveIndex(){
		return currentWaveIndex;
	}
	//---------------------------------------------------------------
	public bool GetIsWaveCleared(){
		return isWaveCleared;
	}
	//---------------------------------------------------------------
	public void ResetIsWaveCleared(){
		isWaveCleared = false;
	}
	
	public static bool GetWaveHasStarted(){
		return waveHasStarted;
	}	
	//---------------------------------------------------------------
	public void WaveChangeHandling (){
		if (currentWaveChild) {
			if (currentWaveChild.childCount <= 0) {
				isWaveCleared = true;
				currentWaveChild.gameObject.SetActive (false);
				//clear the enemy paddle if all the enemy objects are removed
				/*if(enemyPaddleSpawner && enemyPaddleSpawner.transform.childCount>0){ 
					Destroy(enemyPaddleSpawner.transform.GetChild(0).gameObject);
				}*/

				if (currentWaveIndex + 1 < waveLength) {
					currentWaveIndex++;
					PlayerPrefManager.SetUITextStatus (PlayerPrefManager.GUITEXT_STATUS_CHANGING);
					UITextController.SetUITextStatusType (UITextController.DISPLAY_TEXT.WAVE, "");
					currentWaveChild = enemyWaveSpawner.transform.GetChild (currentWaveIndex);
				} else if (currentWaveIndex + 1 >= waveLength && !saved) {
					waveHasStarted = false;
					WinLoseCondition wlCondition = GameObject.FindGameObjectWithTag("Win Lose Condition").GetComponent<WinLoseCondition>();
					wlCondition.Win();

					//Saving data when the stage was cleared
					//store collected gold to player data
					Inventory inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
					PlayerProgress.playerData.gold += inventory.GetCollectedCoin();
					//if the first time completing map
					if(!PlayerProgress.playerData.completedMaps.Contains(mapName)){
						//store in to completed map list
						PlayerProgress.playerData.completedMaps.Add(mapName);
						if(!MapDictionary.CheckIsSecretMap(mapName)){
							CheckForTheNextMap();
						}
					}else{ // this map had been cleared before
						RewardLoot.GetComponent<Reward>().SetHadLooted();
					}

					SaveLoadSystem.SaveGame(PlayerProgress.playerData);
					saved = true;
				}
			}
		}
	}
	//---------------------------------------------------------------
	public void ClearPresentWave(){
		for (int i = 0; i< currentWaveChild.childCount; i++){
			Destroy(currentWaveChild.GetChild(i).gameObject);
		}
	}
	//---------------------------------------------------------------
	public void SetActiveWaveChild(){
		currentWaveChild.gameObject.SetActive (true);
		waveHasStarted = true;
	}
	//---------------------------------------------------------------
	public void ResumeGame ()
	{
		Time.timeScale = 1;
		ExitPauseMenu();
	}
	//---------------------------------------------------------------
	//---------------------------------------------------------------
	private void PauseHanding ()
	{
		if (Input.GetKeyUp (KeyCode.Escape) && (Time.timeScale == 1)) {
			PauseGame ();
		}
		else
			if (Input.GetKeyUp (KeyCode.Escape) && (Time.timeScale == 0 && Prisoner.GetIsCasingSkill ())) {
				PauseGame ();
			}
			else
				if (Input.GetKeyUp (KeyCode.Escape) && (Time.timeScale == 0)) {
					ResumeGame ();
				}
	}
	//---------------------------------------------------------------
	private void PauseGame ()
	{
		Time.timeScale = 0;
		DisplayPauseMenu();
	}
	//---------------------------------------------------------------
	private void DisplayPauseMenu ()
	{
		pauseMenu.SetActive(true);
	}
	//---------------------------------------------------------------
	private void ExitPauseMenu ()
	{
		pauseMenu.SetActive(false);
	}
	//---------------------------------------------------------------
	private void MoveToResultScreen(){
		SceneManager.LoadScene("03A Result");
	}
	//---------------------------------------------------------------
	private void CheckForTheNextMap(){
		string nextMapName = GetNextMapName();
		//If there is still a normal map at current world
		if(nextMapName!=""){
			PlayerProgress.playerData.availableMaps.Add(nextMapName);
		}else{	//this is the last normal map
			//open the next world, make the first map of the next world available to play
			PlayerProgress.playerData.maxReachableWorld++;
			int newestWorldIndex = PlayerProgress.playerData.maxReachableWorld - 1;
			print("newestWorldIndex " + newestWorldIndex);
			string firstMapOfNextWorld = MapDictionary.wholeGameMaps[newestWorldIndex][0];
			PlayerProgress.playerData.availableMaps.Add(firstMapOfNextWorld);
			SaveLoadSystem.SaveGame(PlayerProgress.playerData);
		}
	}
	//---------------------------------------------------------------
	private string GetNextMapName(){
		string nextMapName = "";
		int normalMapLength =  MapDictionary.GetNormalMapsNumOfNowWorld();
		for(int mapIndexOfCurrentWorld = 0; mapIndexOfCurrentWorld < normalMapLength; mapIndexOfCurrentWorld++){ 
			if(mapName == MapDictionary.GetThisWorldLocations()[mapIndexOfCurrentWorld] && 
			//and if this is not the last normal map in this current world
			mapIndexOfCurrentWorld!=(normalMapLength-1)){
			nextMapName = MapDictionary.GetThisWorldLocations()[mapIndexOfCurrentWorld+1];
			return nextMapName;
			}
		}
		return nextMapName;
	}
}