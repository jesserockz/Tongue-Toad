using UnityEngine;
using System.Collections;

/// <summary>
/// Class gives a control mode for the character.
/// This mode makes the toad rotate towards where the mouse is onscreen.
/// </summary>
public class MouseFollower : MonoBehaviour
{
	
	private bool follow = true;
	
	float rotationY;
	float sensitivity = 2.0f;
	
	public float rotationSpeed = 4.0f;
	public Transform toad;
	Tongue tongue;
	
	//plane on the level of the water
	private Plane horoPlane;
	//vertical plane at the edge of where you can see (needed for when the mouse hovers quite high onto the horizon
	private Plane verticalPlane;
		
	//vector of where we're looking
	private Vector3 vector = Vector3.zero;
	bool cursorVisible = true;
	
	private Pause pause;
	
	// Use this for initialization
	void Start ()
	{
		//used to know when to allow movement
		GameObject gui = GameObject.FindGameObjectWithTag("Gui");
		pause = gui.GetComponent<Pause>();
		
		//constructs a plane facing up that is the same level as the water
		//GameObject water = GameObject.Find ("Water");
		horoPlane = new Plane (Vector3.up, new Vector3(0, 0.5f, 0));
		verticalPlane = new Plane (Vector3.forward, new Vector3 (0, 0, 78));

		//Get tongue object script to control rotation
		//tongue = GameObject.FindGameObjectWithTag ("Tongue").GetComponent<Tongue> ();
		tongue = GameObject.Find ("TongueTip").GetComponent<Tongue> ();

        
        //Starts with old control style
        follow = false;
        
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		
		float distanceMult = (tongue.maxExtension - tongue.transform.position.z) / tongue.maxExtension;
		
		if (Input.GetKeyDown (KeyCode.F1)) {
			//change the mode
			follow = !follow;
			
			Screen.showCursor = follow;
			Screen.lockCursor = !follow;
		}
		
		if (Input.GetKeyDown (KeyCode.F2) && false) {
			//set mouse invisible
			cursorVisible = !cursorVisible;
			Screen.showCursor = cursorVisible;
			Screen.lockCursor = !cursorVisible;
		}
		
		if (Input.GetKeyDown (KeyCode.KeypadPlus) || Input.GetKeyDown (KeyCode.Plus)) {
			sensitivity += 0.2f;
		} else if (Input.GetKeyDown (KeyCode.KeypadMinus) || Input.GetKeyDown (KeyCode.Minus)) {
			sensitivity -= 0.2f;
		}
		
		//don't allow movement in any of these situations
		if (Time.timeScale == 0 || Pause.isPaused || !pause.enabled) return;
		
		if (!follow) {
			rotationY += Input.GetAxis("Mouse X") * sensitivity * distanceMult;
			rotationY = Mathf.Clamp (rotationY, -60f, 60f);
			
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, 0);
			
		} else {
			float distance;
			Ray ray = Camera.mainCamera.ScreenPointToRay (Input.mousePosition);
		
			//we'll first try for the horozontal plane
			if (horoPlane.Raycast (ray, out distance)) {
				//we got it, so look at it
				vector = ray.GetPoint (distance);
				vector.y = 0f;
				//toad.LookAt(vector);
			} else {
				//otherwise we have to use vertical plane
				verticalPlane.Raycast (ray, out distance);
				vector = ray.GetPoint (distance);
				//make sure we don't make the toad look up/ down
				vector.y = 0;
				//toad.LookAt (vector);
			}
		
			//Debug.DrawLine(toad.position, vector,Color.red);
			Vector3 angle = toad.rotation.eulerAngles;
			float y = angle.y;
		
			if (y > 180) {
				y = Mathf.Clamp (y, 280f, 360f);
			} else {
				y = Mathf.Clamp (y, 0f, 80f);
			}
		
			// Smoothly rotates towards target
			Vector3 normalized = vector - transform.position;
			//TODO this is a cheap hack, there's probably a better way to do this
			if (normalized.z < 0.2)
				normalized.z = -Mathf.Clamp (normalized.z, -2f, 0f);
			Quaternion targetRotation = Quaternion.LookRotation (normalized, Vector3.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, 
            Time.deltaTime * rotationSpeed *
            ((tongue.maxExtension - tongue.transform.position.z) / tongue.maxExtension));
		}
	}
}
