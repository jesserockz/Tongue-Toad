using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public Transform EnemyPrefab;
	public Rigidbody Bullet;
	public float speed = 0.07f;
	public float maxMovement = 3.5f;
	void OnCollisionEnter (Collision c) {
		Debug.Log (c.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {		
		bool l = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
		bool r = Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D);
		bool shoot = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
		
		if(l && transform.position.x>-maxMovement) {
			transform.position -= new Vector3(speed,0,0);
		}
		if(r && transform.position.x<maxMovement) {
			transform.position += new Vector3(speed,0,0);
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			Instantiate(EnemyPrefab,new Vector3(0,0,15f),Quaternion.identity);
		}
		if(shoot){
			Instantiate(Bullet,transform.position+new Vector3(0,0.2f,1f),Quaternion.identity);
		}
	}
}
