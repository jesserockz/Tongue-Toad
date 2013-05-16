using UnityEngine;
using System.Collections;

public class EnvironmentAnimator : MonoBehaviour {

    public float animationSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        animation["animation"].speed = animationSpeed;
		animation["animation"].time = Random.Range(0,animation["animation"].length);
	}
	
	// Update is called once per frame
	void Update () {
        animation.Play("animation");
	}
}
