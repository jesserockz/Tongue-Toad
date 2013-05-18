using UnityEngine;
using System.Collections;

public class ShellShard : MonoBehaviour {
	
	//time before the shard disappears, safeguard to ensure they don't stay around forever
	public float timeToDisappear = 15;
	private float currentlyAround;
	
	
	public float waterLevel = 0.2f, floatHeight;
	public Vector3 buoyancyCentreOffset = Vector3.zero;
	public float bounceDamp = 0.1f;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentlyAround += Time.deltaTime;
		if (currentlyAround > timeToDisappear) {
			Destroy (gameObject);
		}
		
		float y = rigidbody.position.y;
		y = Mathf.Max (y, 1.0f);
		
		//rigidbody.position = new Vector3(rigidbody.position.x, y, rigidbody.position.z);
		//if (rigidbody.position.y < 0) 
		//rigidbody.
		//rigidbody.AddForce(0, (-rigidbody.position.y) *  10, 0);
	}
	
	void FixedUpdate () {
		Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
		float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);
		
		if (forceFactor > 0f) {
			Vector3 uplift = -Physics.gravity * (forceFactor - rigidbody.velocity.y * bounceDamp);
			rigidbody.AddForceAtPosition(uplift, actionPoint);
		}
		
		if (transform.position.y < 0.2 && rigidbody.velocity.z < 1) {
			rigidbody.AddForce(0, 0, -5);
		}
	}
	
	void OnCollisionEnter(Collision info) {
		GameObject o = info.gameObject;
		
		if (o.tag == "Player") {
			Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter(Collider info) {
		GameObject o = info.gameObject;
		
		if (o.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
