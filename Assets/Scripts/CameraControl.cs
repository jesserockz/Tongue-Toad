using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	bool rotatemode = false;
	int rotateleft = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.End)){
			rotatemode = true;
			rotateleft = 180;
		}
		
	}
	
	//FixedUpdate is called a fixed number of times per second
	void FixedUpdate(){
	if(rotatemode && rotateleft!=0){
			transform.RotateAround (Vector3.zero, Vector3.forward, 100 * Time.deltaTime);
			rotateleft--;
		}	
		
	}
}
