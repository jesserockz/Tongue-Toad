using UnityEngine;
using System.Collections;

public class ComboRise : MonoBehaviour {
	
	public float visibleTime = 10.0f;
	public float riseVelocity = 1.0f;
	
	private Color c;
	
	void Start() {
		c = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Pause.isPaused || Time.timeScale == 0) return;
		
		c.a -= Time.deltaTime * (1.0f / visibleTime);
		renderer.material.color = c;
		
		transform.Translate(0, 0, -Time.deltaTime * riseVelocity);
		
		if (c.a <= 0) Destroy(gameObject);
	}
}
