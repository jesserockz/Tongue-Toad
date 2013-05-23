using UnityEngine;
using System.Collections;

/// <summary>
/// Simple movement script that causes an enemy to move forward.
/// </summary>
public class PlaneMovement : MonoBehaviour {
	
	public float baseSpeed = 0.1f;
	float accel = 2;
	
	// Use this for initialization
	void Start () {
		//rigidbody.velocity = new Vector3(0, 0, -baseSpeed + randSpeed);
		rigidbody.AddForce(0f,0f,-accel);
	}
	
	// Update is called once per frame
	void Update () {
		//if (rigidbody.velocity.z > -5) rigidbody.AddForce (Random.Range(-5, 5), 0, -5);
		//rigidbody.AddForce (0, 0, -2);
		accel+=0.07f;
		rigidbody.AddForce(0f,0f,-accel);
	}
}
