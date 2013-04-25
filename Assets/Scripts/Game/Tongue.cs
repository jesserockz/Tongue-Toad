using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour {


    public bool tongueOut = false;
    public bool tongueRetracting = false;

    public float tongueExtensionTime = 1.2f;

    public Transform frog;
    public Transform tongue;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        bool shoot = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
        //Debug.Log(tongueRetracting);
        if (!Pause.isPaused)
        {
            if (shoot && !tongueOut) //User has started tongue extension
            {
                Debug.Log("Fire ze Tongue");
                frog.GetComponent<MouseLook>().enabled = false;
                transform.rotation = frog.rotation;
                tongue.rotation = frog.rotation;
                tongueOut = true;
            }
            else if (shoot && tongueOut && !tongueRetracting) //Tongue extension continuing out
            {
                //Debug.Log("Tongue Extending");
                //Move tonguetip in direction of frog
                //transform.rotation = frog.rotation;
                transform.position += transform.forward * 0.2f;
                frog.LookAt(new Vector3(transform.position.x, 0f, transform.position.z));
                
                //Move and stretch tongue into position
                float newX = ((frog.position.x - transform.position.x) / 2) + transform.position.x;
                float newZ = ((frog.position.z - transform.position.z) / 2) + transform.position.z;
                Vector3 newTonguePos = new Vector3(newX, frogPosition().y, newZ);
                float distance = Vector3.Distance(transform.position, frogPosition());
                tongue.position = newTonguePos;
                tongue.localScale = new Vector3(0.1f, 0.1f, distance);
                tongue.LookAt(transform);
                

            }
            else if (tongueOut && (!shoot || tongueRetracting)) //Tongue retracting back into mouth
            {
                //Debug.Log("Tongue Retracting");
                tongueRetracting = true;
                transform.LookAt(frogPosition());
                transform.position += transform.forward * 0.2f;

                float newX = ((frog.position.x - transform.position.x) / 2) + transform.position.x;
                float newZ = ((frog.position.z - transform.position.z) / 2) + transform.position.z;
                Vector3 newTonguePos = new Vector3(newX, frogPosition().y, newZ);
                float distance = Vector3.Distance(transform.position, frogPosition());
                tongue.position = newTonguePos;
                tongue.localScale = new Vector3(0.1f, 0.1f, distance);
                tongue.LookAt(transform);

                if (Vector3.Distance(transform.position, frogPosition()) <= 0.2)
                {
                    tongueRetracting = false;
                    tongueOut = false;
                    transform.position = frogPosition();
                    tongue.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    tongue.position = frogPosition();
                }

                frog.LookAt(new Vector3(transform.position.x, 0f, transform.position.z));
            }
            else if (!tongueOut && !shoot) //Tongue dormant inside mouth waiting to ATTACK!!!
            {
                //Debug.Log("Boring...");
                tongueRetracting = false;
                frog.GetComponent<MouseLook>().enabled = true;
                transform.position = frogPosition();
                tongue.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                tongue.position = frogPosition();
            }

        }
	}

    Vector3 frogPosition()
    {
        return frog.position + new Vector3(0f, 0.35f, 0f);
    }
	
	void OnTriggerEnter (Collider other) {	
		if (other.tag == "Enemy") {
			Player.addScore(10 + Player.combo);
			Player.currentEnergy += 5;
			Player.combo++;
			Destroy(other.gameObject);
        }
        else if (other.tag == "Terrain")
        {
            Debug.Log("Tongue hit terrain");
            tongueRetracting = true;
        }
    }
}
