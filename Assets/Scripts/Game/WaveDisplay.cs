using UnityEngine;
using System.Collections;

public class WaveDisplay : MonoBehaviour
{
	
	public Texture2D displayTex;
	private bool displaying = false;
	private float displayed;
	private float toDisplay = 5;
	private float transparency = 1;
	private Rect r;
	
	// Use this for initialization
	void Start ()
	{
		r = new Rect ();
	}
	
	void OnGUI ()
	{
		if (displaying) {
			GUI.depth = -3;
			
			float f = 0.75f;
			
			float w = displayTex.width * f;
			float h = displayTex.height * f; 
			
			r.width = w;
			r.height = h;
			r.x = (Screen.width - w) / 2;
			r.y = 0;
			
			
			
			displayed += Time.deltaTime;
			
			if (displayed > toDisplay) {
				Color c = GUI.color;
				c.a = transparency;
				
				transparency -= Time.deltaTime * 1f; 
				GUI.color = c;
		
				if (transparency <= 0) {
					displaying = false;
				}
			}
			
			if (displaying) GUI.DrawTexture (r, displayTex);
		}
	}
	
	public void displaySign ()
	{
		displaying = true;
		displayed = 0;
		
		//set alpha back to 1 
		transparency = 1;
	}
		
}
