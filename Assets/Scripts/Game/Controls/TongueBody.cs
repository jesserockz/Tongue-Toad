using UnityEngine;
using System.Collections;

public class TongueBody : MonoBehaviour {
	
	Player player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision c) 
	{
		Debug.Log (c.gameObject.tag);
	}
	
    void OnTriggerEnter(Collider collider)
    {
		Debug.Log ("tongue body collided: " + collider.gameObject.tag);
		GameObject o = collider.gameObject;
        if (o.tag == "Enemy")
        {
			//get the enemy
			Enemy enemy = o.GetComponent<Enemy>();
			Vector3 dir = o.transform.position - transform.position;
			
			
			o.GetComponent<Rigidbody>().AddForce((o.transform.position - transform.position) * 10);
			//attack the enemy... death, animations, etc, are handled there.
			enemy.attack(player.getTongueDamage());
			
			//now tell the player they've attacked an enemy. That stuff is handled there
            player.attackEnemy(enemy);
        }
        else if (o.tag == "Friendly")
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>().activateSpin();
            Destroy(o);
        }
    }
}
