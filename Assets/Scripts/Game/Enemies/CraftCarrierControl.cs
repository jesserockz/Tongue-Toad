using UnityEngine;
using System.Collections;

public class CraftCarrierControl : MonoBehaviour {

    private bool emerging = true;
    private bool submerging = false;
    public GameObject planeSnail;

    private float between = 1f;
    private float lastTime = 0f;
    private int planesLeft = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (submerging && transform.position.y > -1f)
        {
            Vector3 pos = transform.position;
            pos.y -= 1f * Time.deltaTime;
            transform.position = pos;
            return;
        }
        else if (emerging && transform.position.y < 3.8f)
        {
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().Shake();
            Vector3 pos = transform.position;
            pos.y += 1f * Time.deltaTime;
            transform.position = pos;
            return;
        }
		else if (submerging && transform.position.y <= -1f){
			submerging = false;
			GameObject.FindGameObjectWithTag("EnemySpawn").GetComponent<EnemySpawn>().resumeSpawning();
		}
        else
        {
            emerging = false;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ending = true;
        }

        if (planesLeft == 0)
        {
            submerging = true;
			
        }else if (Time.time - lastTime > between)
        {
            Vector3 point = new Vector3(Random.Range(-0.74f,5f), 2f, 40.5f);
            GameObject o = (GameObject)Instantiate(planeSnail, point, Quaternion.Euler(0, -180, 0));
            o.GetComponent<PlaneMovement>().carrier = true;
            lastTime = Time.time;
            planesLeft--;
        }
	}

    private void spawnPlane()
    {
       

    }
}
