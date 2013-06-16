using UnityEngine;
using System.Collections;

public class FriendlyMovement : MonoBehaviour {
	public float startingZOffset = 0f;
	public float startingYOffset = 0f;
	public float speed = 0.05f;
	float lastChange = 0f;
	float nextChange = 0f;
	public int regenerateHealth = 5;

	void Start () {
		float xPos = Random.Range(-3.5f,3.5f);
		//transform.position += new Vector3(xPos,startingYOffset,startingZOffset);
		float randomForceX = Random.Range(-250f,100f);
		float randomForceZ = Random.Range(-100f,-50f);
		//rigidbody.AddForce(randomForceX,0f,randomForceZ);
		lastChange = Time.time;
		nextChange = Random.Range(1.0f,3.0f);
	}

	void Update () {
		//transform.position -= new Vector3(0,0,speed);
		if(false && Time.time - lastChange > nextChange){
			float randomForceX = Random.Range(-10f,10f);
			float randomForceZ = Random.Range(-1f,0f);
			rigidbody.AddForce(randomForceX,0f,randomForceZ);
		}
		
		if(transform.position.z < -5f) {
			Player.currentHealth = Mathf.Min(100, Player.currentHealth + regenerateHealth);
			Player.addScore(10);
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.name == "TongueTip" || other.name == "TongueBody") Debug.Log ("tongue trigger");
	}
	
	void OnCollisionEnter(Collision c)
	{
		GameObject other = c.gameObject; 
		if (other.name == "TongueTip" || other.name == "TongueBody") Debug.Log ("tongue collide");
	}
}
