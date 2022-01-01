﻿using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

	public GameObject target;
	public float delayTime;

	private float timeSinceLastCollision = 0;
	private bool collision = false;

	void OnTriggerEnter2D(Collider2D col){
		if((col.gameObject.name=="RightPlayer" | col.gameObject.name=="LeftPlayer") && col.GetType() == typeof(CircleCollider2D)){
			this.GetComponent<Renderer>().enabled = false;
			target.SetActive(false);
			collision = true;
			timeSinceLastCollision = 0;
		}
	}



	void OnTriggerExit2D(Collider2D col) {
		if((col.gameObject.name=="RightPlayer" | col.gameObject.name=="LeftPlayer") && col.GetType() == typeof(CircleCollider2D)){
			collision = false;

		}
	}
	

	void Update (){
		if (!collision) {
			timeSinceLastCollision += Time.deltaTime;  
			if (timeSinceLastCollision > delayTime) {
				this.GetComponent<Renderer>().enabled = true;
				target.SetActive(true);
				timeSinceLastCollision = 0;
			}
		}

	}
}