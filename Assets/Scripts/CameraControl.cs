using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	float angleToRotate = 360;
	float timeToRotate = 3.0f;
	
	bool rotatemode = false;
	float curRotation = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.End) && !rotatemode){
			rotatemode = true;
			curRotation = 0;
		}
		
		Debug.Log(transform.name);
	}
	
	//FixedUpdate is called a fixed number of times per second
	void FixedUpdate(){
		if(rotatemode){
			float rot = Time.deltaTime / timeToRotate * angleToRotate;
			transform.RotateAround (Vector3.zero, Vector3.forward, rot);
			curRotation += rot;
			
			if (curRotation >= angleToRotate) {
				//ensure the player is back at correct point
				transform.RotateAround (Vector3.zero, Vector3.forward, -(curRotation - angleToRotate));
				rotatemode = false;
			}
		}	
		
		
	}
}
