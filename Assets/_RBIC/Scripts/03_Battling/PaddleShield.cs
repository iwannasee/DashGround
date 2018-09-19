using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleShield : MonoBehaviour {
	public float moveSpeed;
	public bool isPlayWithMouse;
	public GameObject inventoryGameObj;
	private Inventory inventory;
	void Start(){
		inventory = inventoryGameObj.GetComponent<Inventory>();
	}
	//---------------------------------------------------------------
	void Update () {
	
	//Can control paddle if game is not paused by skill shooting
		if (!Prisoner.GetIsCasingSkill() //PlayerPrefManager.GetSkillPause()
		&& (Time.timeScale == 1)) {
			PlayWithMouse ();
		}
	}

	//---------------------------------------------------------------
	void PlayWithArrow(){
		Vector3 newPos = new Vector3 (transform.position.x + Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime, transform.position.y, 0);
		this.transform.position = newPos;
	}

	//---------------------------------------------------------------
	void PlayWithMouse(){
		float MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		Vector3 paddleShieldPos = new Vector3 (Mathf.Clamp (MousePos, -2.65f, 2.65f), this.transform.position.y, 0f);
		this.transform.position = paddleShieldPos;
	}

	//---------------------------------------------------------------
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.GetComponent<GoldCoin>()) {
			col.GetComponent<GoldCoin>().CoinDestroy();
			inventory.AddCoin();
		}
	}
}
