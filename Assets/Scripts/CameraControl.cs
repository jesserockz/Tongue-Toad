using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public float countToRotate;
	public float timeToRotate;
	public float rotateAcceleration;
	
	bool starting = true;
	
	bool rotatemode = false;
	public float curRotation = 0;
	public bool slidingOut = false;
	public bool slidingIn = false;
	public float slideInStart = 99999f;
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
			slidingOut = true;
			
		}
		
		Debug.Log(transform.name);
	}
	
	//FixedUpdate is called a fixed number of times per second
	void FixedUpdate(){
		if(rotatemode){
			if(camera.fieldOfView<80 && slidingOut)
				camera.fieldOfView+=0.1f;
			else if(camera.fieldOfView>=80 && slidingOut){
				slidingOut = false;
				slideInStart = (countToRotate*360)-curRotation;
			}
			
			if(camera.fieldOfView>52 && slidingIn)
				camera.fieldOfView-=0.1f;
			else if(camera.fieldOfView<52 && slidingIn)
				slidingIn = false;
			
			if(curRotation>=slideInStart) slidingIn = true;
			
			if(curRotation<countToRotate*360/2) timeToRotate -= rotateAcceleration;
			else if(curRotation>countToRotate*360/2) timeToRotate += rotateAcceleration;
			float rot = Time.deltaTime / timeToRotate * countToRotate*360;
			transform.RotateAround (new Vector3(0,2,2), Vector3.left, -rot);
			curRotation += rot;
			
			if (curRotation >= countToRotate*360) {
				//ensure the player is back at correct point
				transform.RotateAround (new Vector3(0,2,2), Vector3.left, (curRotation - countToRotate*360));
				rotatemode = false;
			}
		}	
		
		
	}
}
