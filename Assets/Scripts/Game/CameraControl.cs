using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public float countToRotate;
	public float timeToRotate;
	public float rotateAcceleration;
	public float cameraZoomSpeed;
	public float maxCameraZoomOut;
	
	public bool rotatemode = false;
    public float curRotation = 0;
    public bool slidingOut = false;
    public bool slidingIn = false;
    public float slideInStart = float.MaxValue;
	
	Vector3 lastaccel;
	
	
	// Use this for initialization
	void Start () {
		lastaccel = Input.acceleration;
        SoundEngine.Get().PlayMusic("tongue toad",true);
	}
	
	// Update is called once per frame
	void Update () {
		bool spinKey = Input.GetKey (KeyCode.End) || Input.GetKey (KeyCode.Return);
		if(spinKey){
            activateSpin();
		}
		shake();
	}

    public void activateSpin()
    {
        if (!rotatemode)
        {
            rotatemode = true;
            slidingOut = true;
        }
    }
	
	void shake(){
		Vector3 accel = Input.acceleration;
		if((accel-lastaccel).magnitude>1f && !rotatemode){
			rotatemode = true;
			Player.currentEnergy = 100;
			slidingOut = true;
		}
		lastaccel = accel;
	}
	
	
	
	//FixedUpdate is called a fixed number of times per second
	void FixedUpdate(){
		zoom ();
		rotate ();
	}
	
	void zoom(){
		
		if(camera.fieldOfView<maxCameraZoomOut && slidingOut)
				camera.fieldOfView+=cameraZoomSpeed;
		else if(camera.fieldOfView>=maxCameraZoomOut && slidingOut){
			slidingOut = false;
			slideInStart = (countToRotate*360)-curRotation;
		}

		if(camera.fieldOfView>52 && slidingIn)
			camera.fieldOfView-=cameraZoomSpeed;
		else if(camera.fieldOfView<=52 && slidingIn){
			slidingIn = false;
			curRotation = 0;
			slideInStart = float.MaxValue;
		}
		
		if(curRotation>=slideInStart)
			slidingIn = true;
	}
		
	
	void rotate(){
		if(rotatemode){
			if(curRotation<countToRotate*360/2) 
				timeToRotate -= rotateAcceleration;
			else if(curRotation>countToRotate*360/2) 
				timeToRotate += rotateAcceleration;
			float rot = Time.deltaTime / timeToRotate * countToRotate*360;
			transform.RotateAround (new Vector3(0,2,2), Vector3.left, -rot);
			curRotation += rot;
			if (curRotation >= countToRotate*360) {
				//ensure the player is back at correct point
				transform.RotateAround (new Vector3(0,2,2), Vector3.left, (curRotation - countToRotate*360));
                curRotation = 0;
				rotatemode = false;
			}
		}
	}
}
