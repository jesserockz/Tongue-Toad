using UnityEngine;
using System.Collections;

public class EnvironmentAnimator : MonoBehaviour {

    public float animationSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        animation["animation"].speed = animationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        animation.Play("animation");
	}
}
