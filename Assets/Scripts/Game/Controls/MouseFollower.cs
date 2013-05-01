using UnityEngine;
using System.Collections;

/// <summary>
/// Class gives a control mode for the character.
/// This mode makes the toad rotate towards where the mouse is onscreen.
/// </summary>
public class MouseFollower : MonoBehaviour {
	
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
	
	// Use this for initialization
	void Start () {
		//constructs a plane facing up that is the same level as the water
		//GameObject water = GameObject.Find ("Water");
		
		horoPlane = new Plane(Vector3.up, Vector3.zero);
		verticalPlane = new Plane(Vector3.forward, new Vector3(0, 0, 78));

        //Get tongue object script to control rotation
        tongue = GameObject.FindGameObjectWithTag("Tongue").GetComponent<Tongue>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            cursorVisible = !cursorVisible;
            Screen.showCursor = cursorVisible;
            //Screen.lockCursor = !cursorVisible;
        }
		float distance;
		Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
		
		//we'll first try for the horozontal plane
		if (horoPlane.Raycast(ray, out distance))
		{
			//we got it, so look at it
			vector = ray.GetPoint(distance); 
			//toad.LookAt(vector);
		} 
		else 
		{
			//otherwise we have to use vertical plane
			verticalPlane.Raycast (ray, out distance);
			vector = ray.GetPoint(distance);
			//make sure we don't make the toad look up/ down
			vector.y = 0;
			//toad.LookAt (vector);
		}
		
		//Debug.DrawLine(toad.position, vector,Color.red);
		Vector3 angle = toad.rotation.eulerAngles;
		float y = angle.y;
		
		if (y > 180) {
			y = Mathf.Clamp(y, 280f, 360f);
		} else {
			y = Mathf.Clamp (y, 0f, 80f);
		}
		
		// Smoothly rotates towards target
		Vector3 normalized = vector - transform.position;
		//TODO this is a cheap hack, there's probably a better way to do this
		if (normalized.z < 0.2) normalized.z = -Mathf.Clamp (normalized.z, -2f, 0f);
		Quaternion targetRotation = Quaternion.LookRotation(normalized, Vector3.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
            Time.deltaTime * rotationSpeed*
            ((tongue.maxExtension-tongue.transform.position.z)/tongue.maxExtension));
	}
}
