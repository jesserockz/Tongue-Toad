using UnityEngine;
using System.Collections;

public class ComboRise : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Pause.isPaused || Time.timeScale == 0) return;
		
		Color c = this.renderer.material.color;
		
		c.a -= Time.deltaTime * 1f;
		
		Vector3 pos = transform.position;
		pos.y += Time.deltaTime * 1f;
		
		transform.position = pos;
		
		renderer.material.color = c;
		
		if (this.renderer.material.color.a <= 0) Destroy(gameObject);
	}
}
