using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOfStage : MonoBehaviour {
	// Use this for initialization
	private int WaveBlockCount;
	public GameObject CoinPref;
	public GameObject specialItemPref;
	public int maxHit;
	private int timesHit;
	private GameObject ItemContainer;
	//---------------------------------------------------------------
	void Start () {
		timesHit = 0;
	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.GetComponent <ProjectileBall>() ||
			collision.gameObject.GetComponent<SkillShot>()) {
			timesHit++;
		}
		
		if (timesHit >= maxHit) {
			GetComponent<ItemDropper>().DropItem();
			BlockDestroy ();
		}
	}
	//---------------------------------------------------------------
	void BlockDestroy(){
		ParticleManager vfx = GameObject.FindGameObjectWithTag("Particle Manager").GetComponent<ParticleManager>();
		//Find how many block in this current wave
		WaveBlockCount = GameObject.FindObjectsOfType<BlockOfStage>().Length;
		WaveBlockCount--;
		//EXTENDABLE if this wave have block regen, check the isRegen bool of block container 
		//Destroy whole block container if this is the last block
		if(WaveBlockCount<=0){
			Instantiate(vfx.brickBroken, transform.position, Quaternion.identity);
			Destroy(transform.parent.gameObject);
		}
		Instantiate(vfx.brickBroken, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	//---------------------------------------------------------------
}
