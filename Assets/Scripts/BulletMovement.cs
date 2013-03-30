using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.AddForce(transform.forward*2000);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z>20) {
			Player.combo = 0;
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter (Collider other) {	
		if (other.tag == "Enemy") {
			Player.score += 10 + Player.combo;
			Player.currentEnergy += 5;
			Player.combo++;
			Destroy(other.gameObject);
			Destroy (gameObject);
		}
    }
}
