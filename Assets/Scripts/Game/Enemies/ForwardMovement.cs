using UnityEngine;
using System.Collections;

/// <summary>
/// Simple movement script that causes an enemy to move forward.
/// </summary>
public class ForwardMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.velocity = new Vector3(0, 0, -5);
	}
	
	// Update is called once per frame
	void Update () {
		if (rigidbody.velocity.z > -5) rigidbody.AddForce (Random.Range(-5, 5), 0, -5);
		//rigidbody.AddForce (0, 0, -2);
	}
}
