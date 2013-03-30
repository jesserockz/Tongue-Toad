using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public float startingZOffset = 0f;
	public float startingYOffset = 0f;
	public float speed = 0.05f;
	float lastChange = 0f;
	float nextChange = 0f;

	void Start () {
		float xPos = Random.Range(-3.5f,3.5f);
		transform.position += new Vector3(xPos,startingYOffset,startingZOffset);
		float randomForceX = Random.Range(-100f,100f);
		float randomForceZ = Random.Range(-250f,-100f);
		rigidbody.AddForce(randomForceX,0f,randomForceZ);
		lastChange = Time.time;
		nextChange = Random.Range(1.0f,3.0f);
	}

	void Update () {
		//transform.position -= new Vector3(0,0,speed);
		if(Time.time - lastChange > nextChange){
			float randomForceX = Random.Range(-10f,10f);
			float randomForceZ = Random.Range(-1f,0f);
			rigidbody.AddForce(randomForceX,0f,randomForceZ);
		}
		if(transform.position.z<-5f) Destroy(gameObject);
	}
}
