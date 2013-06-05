using UnityEngine;
using System.Collections;

//script populates transitionCamera with correct coordinates 
public class SetTransitionOrigin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject menuCamera = GameObject.Find ("MenuCamera");
		TransitionCamera.originalCameraLocation = menuCamera.transform.position;
		TransitionCamera.originalCameraRotation = menuCamera.transform.rotation;
		TransitionCamera.isTransitioning = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
