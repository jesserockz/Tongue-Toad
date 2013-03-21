using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public float startingZOffset = 0f;
	public float startingYOffset = 0f;
	public float speed = 0.05f;

	void Start () {
		float xPos = Random.Range(-3.5f,3.5f);
		transform.position += new Vector3(xPos,startingYOffset,startingZOffset);
	}

	void Update () {
		transform.position -= new Vector3(0,0,speed);
		if(transform.position.z<-5) Destroy(gameObject);
	}
}
