using UnityEngine;
using System.Collections;

public class CameraScale : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		GetComponent<Camera>().orthographicSize = Screen.height/2f;
	}
}
