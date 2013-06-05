using UnityEngine;
using System.Collections;

/// <summary>
/// Simple movement script that causes an enemy to move forward.
/// </summary>
public class ForwardMovement : MonoBehaviour {
	
	public float baseSpeed = 5;
	
	// Use this for initialization
	void Start () {
		float randSpeed = Random.Range (-1.0f, 1.0f);
		rigidbody.velocity = new Vector3(0, 0, -baseSpeed + randSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		//if (rigidbody.velocity.z > -5) rigidbody.AddForce (Random.Range(-5, 5), 0, -5);
		//rigidbody.AddForce (0, 0, -2);
	}
}
