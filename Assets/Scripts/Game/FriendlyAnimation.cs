using UnityEngine;
using System.Collections;

public class FriendlyAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int i = Random.Range(0, 3);
        if (i == 0 && !animation.isPlaying)
        {
            animation.Play("Idle2");
        }
        else if (i != 0 && !animation.isPlaying)
        {
            animation.Play("Idle1");
        }
	}
}
