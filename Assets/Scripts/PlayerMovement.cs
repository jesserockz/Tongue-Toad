using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public Transform EnemyPrefab;
	public Rigidbody Bullet;
	public float speed = 0.07f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x>-3.5)
			transform.position -= new Vector3(speed,0,0);
		if(Input.GetKey(KeyCode.RightArrow) && transform.position.x<3.5)
			transform.position += new Vector3(speed,0,0);
		if(Input.GetKeyDown(KeyCode.Q)){
			Instantiate(EnemyPrefab,new Vector3(0,0,15f),Quaternion.identity);
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			Instantiate(Bullet,transform.position+new Vector3(0,0.2f,1f),Quaternion.identity);
		}
	}
}
