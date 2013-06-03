using UnityEngine;
using System.Collections;

public class EnemyInfoPanels : MonoBehaviour {
	
	public Texture2D plebTexture;
	public Texture2D testudoTexture;
	public Texture2D helicopterTexture;
	public Texture2D planeTexture;
	public Texture2D aircraftCarrierTexture;
	public Texture2D toadTexture;
	
	public static bool displayInstructions;
	
	private Texture2D currentlyDisplaying;
	
	private Pause pause; 
	
	// Use this for initialization
	void Start () {
		GameObject gui = GameObject.FindGameObjectWithTag("Gui");
		pause = gui.GetComponent<Pause>();
	}
	
	public void displayPleb()
	{
		currentlyDisplaying = plebTexture;
		
	}
	
	public void displayTestudo() 
	{
		currentlyDisplaying = testudoTexture;
	}
	
	public void displayHelicopter() 
	{
		currentlyDisplaying = helicopterTexture;
	}
	
	public void displayPlane() 
	{
		currentlyDisplaying = planeTexture;
	}
	
	public void displayAircraftCarrier() 
	{
		currentlyDisplaying = aircraftCarrierTexture;
	}
	
	public void displayToad() 
	{
		currentlyDisplaying = toadTexture;
	}
	
	void OnGUI() {
		if (currentlyDisplaying == null) return;
		
		pause.enabled = false;
		Time.timeScale = 0;
		
		Screen.showCursor = true;
        Screen.lockCursor = false;
		
		//display the instructions texture and a button
		//when the button is clicked, enable Pause script, destroy this, then set timestep back to 1
		int w = Screen.width;
		int h = Screen.height;
		
		GUI.DrawTexture(new Rect(w * 0.1f, h * 0.1f, w * 0.8f, h * 0.8f), currentlyDisplaying);
		
		Rect r = new Rect((w - 100) / 2.0f, h * 0.77f, 100.0f, 30.0f);
		if (GUI.Button (r, "Okay!")) {
			currentlyDisplaying = null;
			pause.enabled = true;
			Time.timeScale = 1;
            Screen.showCursor = false;
            Screen.lockCursor = true;
		}
	}
}
