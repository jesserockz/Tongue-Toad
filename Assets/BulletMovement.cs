using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddForce(transform.forward*2000);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z>20) Destroy(gameObject);
	}
	
	void OnTriggerEnter (Collider other) {
        Destroy(other.gameObject);
		Destroy (gameObject);
    }
}
