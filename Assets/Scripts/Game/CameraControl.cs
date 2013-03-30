using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public float angleToRotate;
	public float timeToRotate;
	
	bool starting = true;
	
	bool rotatemode = false;
	float curRotation = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool spinKey = Input.GetKey (KeyCode.End) || Input.GetKey (KeyCode.Return);
		if(spinKey && !rotatemode){
			rotatemode = true;
			curRotation = 0;
			Player.currentEnergy = 100;
		}
		
		Debug.Log(transform.name);
	}
	
	//FixedUpdate is called a fixed number of times per second
	void FixedUpdate(){
		if(rotatemode){
			if(curRotation<angleToRotate/2) timeToRotate -= 0.07f;
			else if(curRotation>angleToRotate/2) timeToRotate +=0.07f;
			float rot = Time.deltaTime / timeToRotate * angleToRotate;
			transform.RotateAround (new Vector3(0,2,2), Vector3.left, -rot);
			curRotation += rot;
			
			if (curRotation >= angleToRotate) {
				//ensure the player is back at correct point
				transform.RotateAround (new Vector3(0,2,2), Vector3.left, (curRotation - angleToRotate));
				rotatemode = false;
			}
		}	
		
		
	}
}
