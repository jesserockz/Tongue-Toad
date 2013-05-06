using UnityEngine;
using System.Collections;

public class TongueBody : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
			
            Player.addScore(10 + Player.combo);
            Player.currentEnergy += 5;
            Player.combo++;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Friendly")
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>().activateSpin();
            Destroy(other.gameObject);
        }
    }
}
