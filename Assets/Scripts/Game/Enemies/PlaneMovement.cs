using UnityEngine;
using System.Collections;

/// <summary>
/// Simple movement script that causes an enemy to move forward.
/// </summary>
public class PlaneMovement : MonoBehaviour {
	
	public float baseSpeed = 0.1f;
	float accel = 2;
    public bool carrier = false;
    public float xpos = 0f;
	public float accelIncrement = 0.04f;
	
	// Use this for initialization
	void Start () {
		//rigidbody.velocity = new Vector3(0, 0, -baseSpeed + randSpeed);
		rigidbody.AddForce(0f,0f,-accel);
        if (carrier)
        {
            //xpos = Random.Range(-2f, 5f);
            Debug.Log("XPos: " + xpos);
			accelIncrement = 0.06f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		//if (rigidbody.velocity.z > -5) rigidbody.AddForce (Random.Range(-5, 5), 0, -5);
		//rigidbody.AddForce (0, 0, -2);
        if (!Pause.isPaused)
        {
            accel += accelIncrement; 
            rigidbody.AddForce(0f, 0f, -accel * TripMode.bonuses[TripMode.enemySpeed]);
            if (carrier)
            {
                //-2 to 5
                if (transform.position.z < 39f)
                {
                    Vector3 pos = transform.position;
                    if(transform.position.y > 0.6f){
                    pos.y -= 1f * Time.deltaTime;
                    }
                    //if (xpos < 2.5f && transform.position.x > xpos)
                    //{
                    //    pos.x -= 4f * Time.deltaTime;
                    //}
                    //else if (xpos > 2.5f && transform.position.x < xpos)
                    //{
                    //    pos.x += 4f * Time.deltaTime;
                    //}
                    transform.position = pos;
                }
            }
        }
	}
}
