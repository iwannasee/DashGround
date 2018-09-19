using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public GameObject sodierShot;
	public GameObject sentryShooter;
	public float maxCoolDownTime;
	public float randomCoolDownTweak;
	public int maxHealth;

	private int enemyCount;
	private float shotCoolDownTime;
	private GameObject enemyShotContainer;
	private GameObject[] Prisoners;
	private Vector3 shotTarget;
	private float actualRandomCoolDown;
	private int currentHealth;


	//---------------------------------------------------------------
	void Start () {
		currentHealth = maxHealth;
		actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
		shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
		//Create container of Enemy shots
		if(!GameObject.Find("Enemy Shot Container")){
			enemyShotContainer = new GameObject("Enemy Shot Container");
			enemyShotContainer.AddComponent<ClearUpChidren>();
		}else{
			enemyShotContainer = GameObject.Find("Enemy Shot Container");
		}

		Prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
	}
		
	//---------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		if(EnemyWaveController.GetWaveHasStarted()){
			shotCoolDownTime -= Time.deltaTime;
			if(shotCoolDownTime<= 0f && !Prisoner.GetAllPrisonerDead()){
				Fire();
				actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
				shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
			}	
		}
	}

	//---------------------------------------------------------------
	void Fire(){
		if (!enemyShotContainer) {
			return;
		}
		if(Prisoners.Length<=0){
			Debug.Log("prisoners are not found. now refind the prisoners");
			Prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
		}
		//Generate a Shot
		GameObject shot = Instantiate (sodierShot, sentryShooter.transform.position, Quaternion.identity) as GameObject;
		shot.transform.parent = enemyShotContainer.transform;
		//Get a random target for the shot

		int rdNum = Random.Range (0, Prisoners.Length);
		Rigidbody2D shotRgBody = shot.GetComponent<Rigidbody2D> ();
		//Set the direction to aim the shot
		if(Prisoners.Length > 0){
			Vector3 dir = (Prisoners [rdNum].transform.position - transform.position).normalized;
			//Actual Shot
			shotRgBody.velocity = dir * shot.GetComponent<AimingShot> ().speed;
			print(Prisoners[rdNum].name + " " + shotRgBody.velocity);
		} else{
			print("no prisoner found");
		}
	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.GetComponent<ProjectileBall> () ||
		   collision.gameObject.GetComponent<SkillShot> ()) {
			currentHealth--;
			if (currentHealth <= 0) {
				EnemyDestroy ();
			}
		}
	}
	//---------------------------------------------------------------
	void EnemyDestroy(){
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		enemyCount--;
		//EXTENDABLE if this wave have Enemy regen, check the isRegen bool of Enemy container 
		//Destroy whole Enemy container if this is the last Enemy
		if(enemyCount<=0){
			GameObject waveController = GameObject.FindGameObjectWithTag("Wave Controller");
			waveController.GetComponent<EnemyWaveController>().ClearPresentWave();
			Destroy(transform.parent.gameObject);
		}
		Destroy(gameObject);
	}
	//---------------------------------------------------------------
}
