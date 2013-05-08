using UnityEngine;
using System.Collections;

//class handles doing the overlay at the start for game instructions
public class InstructionsOverlay : MonoBehaviour {
	public bool enabled = true;
	
	public Texture2D overlayTexture;

	private Pause pause; 
	
	// Use this for initialization
	void Start () {
		if (!enabled)
		{
			Destroy (this);
			return;
		}
		
		GameObject gui = GameObject.FindGameObjectWithTag("Gui");
		pause = gui.GetComponent<Pause>();
		
		//stop them going into pause mode cause it could get screwy otherwise
		pause.enabled = false;
		
		//stop the time-step, otherwise enemies will move
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void OnGUI () {
		Time.timeScale = 0;
		//display the instructions texture and a button
		//when the button is clicked, enable Pause script, destroy this, then set timestep back to 1
		int w = Screen.width;
		int h = Screen.height;
		GUI.DrawTexture(new Rect(w * 0.1f, h * 0.1f, w * 0.8f, h * 0.8f), overlayTexture);
		Rect r = new Rect((w - 100) / 2.0f, h * 0.83f, 100.0f, 30.0f);
		if (GUI.Button (r, "Start Game!")) {
			pause.enabled = true;
			Time.timeScale = 1;
			Destroy (this);
		}
	}
}
