using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	

	public float countToRotate;
	public float timeToRotate;
	public float rotateAcceleration;
	public float cameraZoomSpeed;
	public float maxCameraZoomOut;
	public bool rotating = false;
	public float curRotation = 0;
	public bool slidingOut = false;
	public bool slidingIn = false;
	public float slideInStart = float.MaxValue;
	public float fieldOfView = 0f;
	Vector3 lastaccel;
	public static int UNDERWATERMODE = 0;
	public static int BARRELROLLMODE = 1;
	//public static fixed int NEWMODE= 2;

	int currentRotationMode = BARRELROLLMODE;
	
	Player player;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		fieldOfView = camera.fieldOfView;
		lastaccel = Input.acceleration;
		//SoundEngine.PlayMusic("tongue toad",true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool spinKey = Input.GetKey (KeyCode.End) || Input.GetKey (KeyCode.Return);
		if (spinKey) {
			activateSpin ();
		}
		shake ();
	}
	
	//FixedUpdate is called a fixed number of times per second
	void FixedUpdate ()
	{
		checkCameraEffects ();
		zoom ();
		rotate ();
	}
	
	private void checkCameraEffects ()
	{

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MotionBlur> ().enabled = (rotating || slidingIn || player.tripMode.blur);

	}
	
	public void activateSpin ()
	{
		if (!rotating && !slidingIn) {
			rotating = true;
			slidingOut = true;
		}
	}
	
	void shake ()
	{
		Vector3 accel = Input.acceleration;
		if ((accel - lastaccel).magnitude > 1f && !rotating) {
			rotating = true;
			Player.currentEnergy = 100;
			slidingOut = true;
		}
		lastaccel = accel;
	}
	
	void zoom ()
	{
		if (currentRotationMode == BARRELROLLMODE)
			maxCameraZoomOut = 40f;

		

		if (camera.fieldOfView > fieldOfView && slidingIn)
			camera.fieldOfView -= cameraZoomSpeed;
		else if (camera.fieldOfView <= fieldOfView && slidingIn) {
			slidingIn = false;
			curRotation = 0;
			slideInStart = float.MaxValue;
		}

		if (camera.fieldOfView < maxCameraZoomOut && slidingOut)
			camera.fieldOfView += cameraZoomSpeed;
		else if (camera.fieldOfView >= maxCameraZoomOut && slidingOut) {
			slidingOut = false;
			slideInStart = (countToRotate * 360) - curRotation;
		}

		if (curRotation >= slideInStart)
			slidingIn = true;
	}
	
	void rotate ()
	{
		if (rotating) {
			if (currentRotationMode == UNDERWATERMODE) {
				if (curRotation < countToRotate * 360 / 2)
					timeToRotate -= rotateAcceleration;
				else if (curRotation > countToRotate * 360 / 2)
					timeToRotate += rotateAcceleration;
				float rot = Time.deltaTime / timeToRotate * countToRotate * 360;
				transform.RotateAround (new Vector3 (0, 2, 2), Vector3.left, -rot);
				curRotation += rot;
				if (curRotation >= countToRotate * 360) {
					//ensure the player is back at correct point
					transform.RotateAround (new Vector3 (0, 2, 2), Vector3.left, (curRotation - countToRotate * 360));
					curRotation = 0;
					rotating = false;
					//player.setTripping(false);
                    player.tripMode.finishCameraSpin();
				}
			} else if (currentRotationMode == BARRELROLLMODE) {
				if (curRotation < countToRotate * 360 / 2)
					timeToRotate -= rotateAcceleration;
				else if (curRotation > countToRotate * 360 / 2)
					timeToRotate += rotateAcceleration;
				float rot = Time.deltaTime / timeToRotate * countToRotate * 360;
				//transform.RotateAround(new Vector3(0, 2, 2), Vector3.left, -rot);
				transform.Rotate (Vector3.forward, rot);
				curRotation += rot;
				//transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, curRotation, transform.rotation.w);
				if (curRotation >= countToRotate * 360) {
					//ensure the player is back at correct point
					//transform.RotateAround(new Vector3(0, 2, 2), Vector3.left, (curRotation - countToRotate * 360));
					curRotation = 0;
					transform.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, curRotation, transform.rotation.w);
					rotating = false;
                    
					//player.setTripping(false);
                    player.tripMode.finishCameraSpin();
				}
			}
		}
	}
}
