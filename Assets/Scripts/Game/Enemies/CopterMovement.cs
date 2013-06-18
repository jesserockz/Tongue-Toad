using UnityEngine;
using System.Collections;

public class CopterMovement : MonoBehaviour {
	float lastChange = 0f;
	float nextChange = 0f;
    bool inUtoadpia = false;

	void Start () {
		float randomForceX = Random.Range(-75f,75f);
		float randomForceZ = Random.Range(-200f,-125f);
		rigidbody.AddForce(randomForceX,0f,randomForceZ);
		lastChange = Time.time;
		nextChange = Random.Range(1.0f,3.0f);
	}

	void Update () {
		//transform.position -= new Vector3(0,0,speed);
		if(Time.time - lastChange > nextChange){
			float randomForceX = Random.Range(-20f,20f);
			float randomForceZ = Random.Range(-1f,0f);
			rigidbody.AddForce(randomForceX,0f,randomForceZ);
            lastChange = Time.time;
            nextChange = Random.Range(1.0f, 3.0f);
		}
        if (transform.position.x > 4.6f) //right wall
        {
            rigidbody.AddForce(-5f, 0f, 0f);
        }
        else if (transform.position.x < -4.1f) //left wall
        {
            rigidbody.AddForce(5f, 0f, 0f);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject o = other.gameObject;
        if (o.tag == "Terrain" && transform.position.x<0) //left wall
        {
            Debug.Log("Copter Hit left");
            rigidbody.AddForce(100f, 0f, 0f);

        }
        if (o.tag == "Terrain" && transform.position.x > 0) //right wall
        {
            Debug.Log("Copter Hit right");
            rigidbody.AddForce(-100f, 0f, 0f);

        }
    }
}
