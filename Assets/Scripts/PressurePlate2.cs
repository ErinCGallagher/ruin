using UnityEngine;
using System.Collections;

public class PressurePlate2 : MonoBehaviour {
	
	public GameObject target;
	public GameObject target2;
	public float delayTime;
	
	private float timeSinceLastCollision = 0;
	private bool collision = false;
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.name=="RightPlayer" | col.gameObject.name=="LeftPlayer"){
			this.GetComponent<Renderer>().enabled = false;
			target.SetActive(false);
			target2.SetActive (false);
			collision = true;
			timeSinceLastCollision = 0;
		}
	}
	
	
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.name=="RightPlayer" | col.gameObject.name=="LeftPlayer"){
			collision = false;
			
		}
	}
	
	
	void Update (){
		if (!collision) {
			timeSinceLastCollision += Time.deltaTime;  
			if (timeSinceLastCollision > delayTime) {
				this.GetComponent<Renderer>().enabled = true;
				target.SetActive(true);
				target2.SetActive(true);
				timeSinceLastCollision = 0;
			}
		}
		
	}
}