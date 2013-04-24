using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour {


    bool tongueOut = false;
    bool tongueRetracting = false;

    public float tongueExtensionTime = 1.2f;

    public Transform frog;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        bool shoot = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);

        if (!Pause.isPaused)
        {

            if (shoot && !tongueOut)
            {
                frog.GetComponent<MouseLook>().enabled = false;
                transform.rotation = frog.rotation;
                tongueOut = true;
            }
            else if (shoot && tongueOut && !tongueRetracting)
            {
                //tongue.position += tongue.transform.forward * tongueExtensionTime * 5 * Time.deltaTime;
                Vector3 s = transform.localScale;
                //
                transform.localScale = new Vector3(s.x, s.y, s.z + 0.2f);
                Vector3 p = transform.position;
                transform.position += transform.forward * 0.1f;
                frog.LookAt(new Vector3(transform.position.x, 0f, transform.position.z));
            }
            else if (tongueOut && (!shoot || tongueRetracting))
            {
                tongueRetracting = true;
                transform.LookAt(frog.position + new Vector3(0f, 0.35f, 0f));
                //tongue.position += tongue.transform.forward * tongueExtensionTime * 5 * Time.deltaTime;
                Vector3 s = transform.localScale;
                transform.localScale = new Vector3(s.x, s.y, s.z - 0.2f);
                Vector3 p = transform.position;
                transform.position += transform.forward * 0.1f;

                if (transform.localScale.z <= 0.1)
                {
                    tongueRetracting = false;
                    tongueOut = false;
                    transform.position = frog.position + new Vector3(0f, 0.35f, 0.2f);
                    transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
                frog.LookAt(new Vector3(transform.position.x, 0f, transform.position.z));

            }
            else if (!tongueOut && !shoot)
            {
                tongueRetracting = false;
                frog.GetComponent<MouseLook>().enabled = true;
                transform.position = frog.position + new Vector3(0f, 0.35f, 0.2f);
                transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }

        }
	}
	
	void OnTriggerEnter (Collider other) {	
		if (other.tag == "Enemy") {
			Player.addScore(10 + Player.combo);
			Player.currentEnergy += 5;
			Player.combo++;
			Destroy(other.gameObject);
			//Destroy (gameObject);
		}
    }
}
