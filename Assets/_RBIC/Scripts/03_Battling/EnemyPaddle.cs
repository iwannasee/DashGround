using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaddle : MonoBehaviour {
	//how fast the paddle movement is
	public float defendSpeed;
	//how far of the harming shot so that the paddle starts to defend
	public float defendDistance;
	private GameObject prisonerPaddle;
	private GameObject[] ProjectileShots;
	private GameObject nearestShot;
	private float nearestDistance;
	private float distanceToPrisoner;
	//---------------------------------------------------------------
	// Use this for initialization
	void Start () {
		prisonerPaddle = GameObject.FindGameObjectWithTag("Prisoner Paddle");
		if(prisonerPaddle){
			distanceToPrisoner = Mathf.Abs (transform.position.y - prisonerPaddle.transform.position.y);
		}else{
			Debug.Log("Could not find prisoner paddle");
		}
	}
	//---------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		FindShotsInScene ();
		if (ProjectileShots.Length != 0) {
			DefendWithSpeed ();
		}
	}
	//---------------------------------------------------------------
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.GetComponent<SkillShot>()||
			collider.gameObject.GetComponent<ProjectileBall>()){
		   		//nearestShot = collider.gameObject;
		   }
	}
	//---------------------------------------------------------------
	//Find shot from prisoners
	void FindShotsInScene(){
		ProjectileShots = GameObject.FindGameObjectsWithTag ("Projectile Shot");
	}
	//---------------------------------------------------------------
	Vector3 GetPositionOfNearestShot ()
	{	
		FindShotsInScene ();
		//IF there is only one shot in scene, and it it BELOW this paddle, return it
		if ((ProjectileShots.Length == 1)) {
			return ProjectileShots[0].transform.position;
		}
		//if not, look through all shot
		float tempNearestDistance = Mathf.Abs (ProjectileShots [0].transform.position.y - transform.position.y);
		for (int shotIndex = 0; shotIndex < ProjectileShots.Length; shotIndex++) {
			//get the distance of every shot to this paddle
			float shotDistance = Mathf.Abs (ProjectileShots[shotIndex].transform.position.y - transform.position.y);
			//if the distance is closer than the distance of the first shot, select it
			if (tempNearestDistance > shotDistance) {
				tempNearestDistance = shotDistance;
				nearestShot = ProjectileShots [shotIndex];
			} else { //select the default first shot
				nearestShot = ProjectileShots [0];
			}
		}
		return nearestShot.transform.position;
	}
	//---------------------------------------------------------------
	void DefendWithSpeed ()
	{
		Vector3 nearestPos = GetPositionOfNearestShot ();
		float distance = Mathf.Abs (nearestPos.y - transform.position.y);
		float defendLevel = distance / distanceToPrisoner;
		float step = 1;
		if (defendLevel >= 0.9f) {
			step /= 5;
		} else if (defendLevel >= 0.4f && defendLevel < 0.9f) {
			step /= 2;
		}
		transform.position = Vector3.MoveTowards (transform.position, 
			new Vector3 (Mathf.Clamp (nearestPos.x, -2.65f, 2.65f), transform.position.y, 0), 
			defendSpeed *step* Time.deltaTime);
	}
}
