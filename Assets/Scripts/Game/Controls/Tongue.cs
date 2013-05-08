using UnityEngine;
using System.Collections;

public class Tongue : MonoBehaviour
{	
	public bool tongueOut = false;
	public bool tongueRetracting = false;
	public float tongueNormalSpeed = 1.5f;
    float tongueSpeed = 0.0f;
	float retractSpeed = 0.65f;

    public float maxExtension = 15f;
	
	public Transform toad;
	public Transform tongue;
	
	private Player player;
	private PlayerSounds playerSounds;
	
	// Use this for initialization
	void Start ()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.GetComponent<Player>();
		playerSounds = p.GetComponent<PlayerSounds>();
		
        tongueSpeed = tongueNormalSpeed;
	}
	
	// Update is called once per frame
    void Update()
    {
        //return if paused
        if (Pause.isPaused || Time.timeScale == 0)
            return;

        bool shoot = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);

        if (shoot && !tongueOut)
        { //User has started tongue extension
            toad.GetComponent<MouseFollower> ().enabled = false;
            transform.rotation = toad.rotation;
            tongue.rotation = toad.rotation;
            tongueOut = true;
			playerSounds.tongueStartSource.Play();
			playerSounds.tongueStretchSource.Play ();
        }
        else if (shoot && tongueOut && !tongueRetracting)
        { //Tongue extension continuing out
            //Move tonguetip in direction of frog
            //transform.rotation = frog.rotation;
            transform.position += transform.forward * tongueSpeed;
            toad.LookAt(new Vector3(transform.position.x, 0f, transform.position.z));

            //Move and stretch tongue into position
            float newX = ((toad.position.x - transform.position.x) / 2) + transform.position.x;
            float newZ = ((toad.position.z - transform.position.z) / 2) + transform.position.z;
            Vector3 newTonguePos = new Vector3(newX, toadPosition().y, newZ);
            float distance = Vector3.Distance(transform.position, toadPosition());

            tongue.position = newTonguePos;
            tongue.localScale = new Vector3(0.1f, 0.1f, distance);
            tongue.LookAt(transform);
            if (tongue.localScale.z >= maxExtension)
            {
                tongueRetracting = true;
                //tongueSpeed = 0.2f;
                //Debug.Log(tongue.localScale.z);
            }
            //else if (tongue.localScale.z >= (maxExtension - 4f) && tongueSpeed > 0.2f)
            //{
            //    tongueSpeed -= 0.1f;
            //}

        }
        else if (tongueOut && (!shoot || tongueRetracting))
        {
            //Tongue retracting back into mouth
            tongueRetracting = true;
			
			
			float mult = (maxExtension  - Vector3.Distance(transform.position, toadPosition())) / maxExtension;
			//keeps the speed between 0.5-0.9, the tongue getting faster as it approaches
            tongueSpeed = Mathf.Min (Mathf.Max (0.6f, mult * 1.5f), 1f);
            //tongueSpeed = 0.8f;
			transform.LookAt(toadPosition());
            transform.position += transform.forward * tongueSpeed;

            float newX = ((toad.position.x - transform.position.x) / 2) + transform.position.x;
            float newZ = ((toad.position.z - transform.position.z) / 2) + transform.position.z;
            Vector3 newTonguePos = new Vector3(newX, toadPosition().y, newZ);
            float distance = Vector3.Distance(transform.position, toadPosition());

            tongue.position = newTonguePos;
            tongue.localScale = new Vector3(0.1f, 0.1f, distance);
            tongue.LookAt(transform);
			
			//if the distance is less tha
            if (transform.position.z < toadPosition().z || Vector3.Distance(transform.position, toadPosition()) <= 0.2)
            {
                tongueRetracting = false;
                tongueOut = false;
                transform.position = toadPosition();
                tongue.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                tongue.position = toadPosition();
                tongueSpeed = tongueNormalSpeed;
            }

            toad.LookAt(new Vector3(transform.position.x, 0f, transform.position.z));
        }
        else if (!tongueOut && !shoot)
        {
						playerSounds.tongueStretchSource.Stop ();

            //Tongue dormant inside mouth waiting to ATTACK!!!
            tongueRetracting = false;
            toad.GetComponent<MouseFollower> ().enabled = true;
            transform.position = toadPosition();
            tongue.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            tongue.position = toadPosition();
            tongueSpeed = tongueNormalSpeed;
        }
    }

	Vector3 toadPosition ()
	{
		return toad.position + new Vector3 (0f, 0.25f, 0f);
	}
	
	void OnTriggerEnter (Collider other)
	{
		GameObject o = other.gameObject;
		
        if (o.tag == "Enemy")
        {
			//get the enemy
			Enemy enemy = o.GetComponent<Enemy>();
			
			//direction of impact
			Vector3 dir = Vector3.Normalize(-(toadPosition() - transform.position));
			
			//fling the enemy back
			o.GetComponent<Rigidbody>().velocity = (1.0f * dir);
			
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
        else if (other.tag == "Terrain")
        {
			tongueRetracting = true;
		}
	}
}
