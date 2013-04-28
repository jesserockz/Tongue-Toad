using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour
{	
	public bool tongueOut = false;
	public bool tongueRetracting = false;
	public float tongueExtensionTime = 1.2f;
	
	public Transform toad;
	public Transform tongue;

	// Use this for initialization
	void Start ()
	{
        
	}
	
	// Update is called once per frame
	void Update ()
	{
		//return if paused
		if (Pause.isPaused)
			return;
		
		bool shoot = Input.GetMouseButton (0) || Input.GetKey (KeyCode.Space);
		
		if (shoot && !tongueOut) { //User has started tongue extension
			toad.GetComponent<MouseLook> ().enabled = false;
			transform.rotation = toad.rotation;
			tongue.rotation = toad.rotation;
			tongueOut = true;
		} else if (shoot && tongueOut && !tongueRetracting) { //Tongue extension continuing out
			//Move tonguetip in direction of frog
			//transform.rotation = frog.rotation;
			transform.position += transform.forward * 0.2f;
			toad.LookAt (new Vector3 (transform.position.x, 0f, transform.position.z));
                
			//Move and stretch tongue into position
			float newX = ((toad.position.x - transform.position.x) / 2) + transform.position.x;
			float newZ = ((toad.position.z - transform.position.z) / 2) + transform.position.z;
			Vector3 newTonguePos = new Vector3 (newX, toadPosition ().y, newZ);
			float distance = Vector3.Distance (transform.position, toadPosition ());
			
			tongue.position = newTonguePos;
			tongue.localScale = new Vector3 (0.1f, 0.1f, distance);
			tongue.LookAt (transform);
		} else if (tongueOut && (!shoot || tongueRetracting)) { 
			//Tongue retracting back into mouth
			tongueRetracting = true;
			transform.LookAt (toadPosition ());
			transform.position += transform.forward * 0.2f;

			float newX = ((toad.position.x - transform.position.x) / 2) + transform.position.x;
			float newZ = ((toad.position.z - transform.position.z) / 2) + transform.position.z;
			Vector3 newTonguePos = new Vector3 (newX, toadPosition ().y, newZ);
			float distance = Vector3.Distance (transform.position, toadPosition ());
			
			tongue.position = newTonguePos;
			tongue.localScale = new Vector3 (0.1f, 0.1f, distance);
			tongue.LookAt (transform);

			if (Vector3.Distance (transform.position, toadPosition ()) <= 0.2) {
				tongueRetracting = false;
				tongueOut = false;
				transform.position = toadPosition ();
				tongue.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
				tongue.position = toadPosition ();
			}

			toad.LookAt (new Vector3 (transform.position.x, 0f, transform.position.z));
		} else if (!tongueOut && !shoot) {
			//Tongue dormant inside mouth waiting to ATTACK!!!
			tongueRetracting = false;
			toad.GetComponent<MouseLook> ().enabled = true;
			transform.position = toadPosition ();
			tongue.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
			tongue.position = toadPosition ();
		}
	}

	Vector3 toadPosition ()
	{
		return toad.position + new Vector3 (0f, 0.35f, 0f);
	}
	
	void OnTriggerEnter (Collider other)
	{	
		if (other.tag == "Enemy") {
			Player.addScore (10 + Player.combo);
			Player.currentEnergy += 5;
			Player.combo++;
			Destroy (other.gameObject);
		} else if (other.tag == "Terrain") {
			Debug.Log ("Tongue hit terrain");
			tongueRetracting = true;
		}
	}
}
