using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {
	public GameObject BattleBallPrefab;
	public float maxSpawningTime;
	public float maxWaitTimeForLaunched;

	private float spawningTime;
	private float waitTimeForLaunched;
	private bool readyStartCountWaitTime = false;
	private GameObject ball;

	// Use this for initialization
	//---------------------------------------------------------------
	void Start () {
		waitTimeForLaunched = maxWaitTimeForLaunched;
	}
	//---------------------------------------------------------------
	void Update(){
		if(EnemyWaveController.GetWaveHasStarted() && !Prisoner.GetAllPrisonerDead()){
			spawningTime -= Time.deltaTime;
			if (spawningTime <= 0) {
				SpawnBattleBall ();
				spawningTime = maxSpawningTime;
				readyStartCountWaitTime = true;
			}
			if (readyStartCountWaitTime) {
				waitTimeForLaunched -= Time.deltaTime;
				if (waitTimeForLaunched <= 0 && ball) {
					ball.GetComponent<ProjectileBall> ().PushBall ();
					waitTimeForLaunched = maxWaitTimeForLaunched;
					readyStartCountWaitTime = false;
				}
			}
		}
	}
	//---------------------------------------------------------------
	void SpawnBattleBall(){
		ball = Instantiate (BattleBallPrefab, transform.position, Quaternion.identity) as GameObject;
		ball.transform.parent = transform;
	}
	//---------------------------------------------------------------
	public void ResetBallAtWaveStart(){
		spawningTime = 0;
	}
}
