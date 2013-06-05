using UnityEngine;
using System.Collections;

public class WaveDisplay : MonoBehaviour {
	
	public Texture2D displayTex;
	
	private bool displaying = false;
	private float displayed;
	
	private float toDisplay = 5;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if (displaying) {
			Rect r = new Rect((Screen.width - displayTex.width) / 2, 0, displayTex.width, displayTex.height);
			GUI.DrawTexture(r, displayTex);
			
			displayed += Time.deltaTime;
			
			if (displayed > toDisplay) {
				displaying = false;
			}
		}
	}
	
	public void displaySign()
	{
		displaying = true;
		displayed = 0;
	}
		
}
