using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public class GameOverGui : MonoBehaviour
{
	
	private string currentName = "";
	private float incorrectTime = 0;
	private bool submitted = false;
	
	private SubmitHighscore hs;
	
	void Start() {
		GameObject o = GameObject.Find("SubmitHighscore");
		
		hs = (SubmitHighscore)o.GetComponent("SubmitHighscore");
	}
	
	void OnGUI ()
	{
		int width = 200;
		int x = (Screen.width - width) / 2;
		int y = (int)(Screen.height * 0.3);
		int offset = 40;
		int i = 0;
		
		GUI.Label (new Rect (x + 55, (int)(Screen.height * 0.1), width, 20), "Game Over!");
		
		
		//haven't click submit yet, so prompt them for name
		if (!submitted) {
			
			GUI.Label (new Rect (x + 50, y + offset * i++ + 5, width, 20), "Send Highscore!");
			GUI.Label (new Rect (x, y + offset * i++ + 10, width, 20), "Score: " + Player.score);
			GUI.Label (new Rect (x, y + offset * i, 45, 20), "Name:");
			currentName = GUI.TextField (new Rect (x + 45, y + offset * i++, width - 45, 20), currentName, 12);
			if (currentName.Length > 0 && !char.IsLetterOrDigit (currentName [currentName.Length - 1])) {
				incorrectTime = 4.5f;
				currentName = currentName.Substring (0, currentName.Length - 1);
			}
		
			//print a note saying only can type characters or numbers if they try to enter invalid stuff
			if (incorrectTime > 0) {
				incorrectTime -= Time.deltaTime;
				GUI.Label (new Rect (x + width + 5, y + offset * (i - 1), width, 20), "Only letters and numbers allowed");
			}
		
			//click to cancel highscore
			string text = currentName.Length == 0 ? "Type Name..." : "Send Highscore!";
			if (GUI.Button (new Rect (x, y + offset * i++ - 10, width, 20), text)) {
				if (currentName.Length == 0)
					return;
			
				submitted = true;
				hs.submitHighscore(currentName, Player.score);
			}
			
		} else {
			
			if (SubmitHighscore.loading) GUI.Label (new Rect(x, Screen.height * 0.4f, width, 20), "Submitting highscore to server...");
			else if (SubmitHighscore.error) GUI.Label (new Rect(x, Screen.height * 0.4f, width, 20), "Couldn't connect to highscore server");
			else if (SubmitHighscore.placing == null || SubmitHighscore.placing.StartsWith("error")) {
				GUI.Label(new Rect(x, Screen.height * 0.4f, width, 20), "Server error submitting score");
			} else {
				GUI.Label (new Rect(x - 30, Screen.height * 0.4f, width + 100, 20), "Congratulations, you've come in position " + SubmitHighscore.placing + "!");
			}
			//they have clicked submit, so now we wait for highscore to submit, or tell them error, etc	
		}
		
		
		 
		if (GUI.Button (new Rect (x, y + offset * 4 + 50, width, 20), "Quit to Main Menu")) {
			Application.LoadLevel ("Menu");
		}
			
	}
	
	private void promptHighscore ()
	{
		
	}
	
	private void submittedHighscore ()
	{
		
	}
}
