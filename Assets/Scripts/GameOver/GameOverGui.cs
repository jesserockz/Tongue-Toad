using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public class GameOverGui : MonoBehaviour
{
	public Texture2D yourScore, send, sendPanel, namePanel;
	private string currentName = "";
	private float incorrectTime = 0;
	private bool submitted = false;
	private SubmitHighscore hs;

	void Start ()
	{
		hs = GetComponent<SubmitHighscore> ();
	}
	
	void OnGUI ()
	{
		if (1 == 1) {
			draw ();
			return;
		}
		int width = 200;
		int x = (Screen.width - width) / 2;
		int y = (int)(Screen.height * 0.3);
		int offset = 40;
		int i = 0;
		
		GUI.Label (new Rect (x + 55, (int)(Screen.height * 0.1), width, 20), "Game Over!");
		
		//haven't click submit yet, so prompt them for name
		if (!submitted) {
			
			GUI.Label (new Rect (x + 50, y + offset * i++ + 5, width, 20), "Send Highscore!");
			GUI.Label (new Rect (x, y + offset * i++ + 10, width, 20), "Score: " + Player.getScore ());
			GUI.Label (new Rect (x, y + offset * i, 45, 20), "Name:");
			currentName = GUI.TextField (new Rect (x + 45, y + offset * i++, width - 45, 20), currentName, 12);
			while (currentName.Length > 0 && !Regex.IsMatch(currentName, "^[a-zA-Z0-9]+$")) {
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
				hs.submitHighscore (currentName, Player.getScore ());
			}
			
		} else {
			if (SubmitHighscore.loading)
				GUI.Label (new Rect (x, Screen.height * 0.4f, width, 20), "Submitting highscore to server...");
			else if (SubmitHighscore.error)
				GUI.Label (new Rect (x, Screen.height * 0.4f, width, 20), "Couldn't connect to highscore server");
			else if (SubmitHighscore.placing == null || SubmitHighscore.placing.StartsWith ("error")) {
				GUI.Label (new Rect (x, Screen.height * 0.4f, width, 20), "Server error submitting score");
			} else {
				GUI.Label (new Rect (x - 30, Screen.height * 0.4f, width + 100, 20), "Congratulations, you've come in position " + SubmitHighscore.placing + "!");
			}
			//they have clicked submit, so now we wait for highscore to submit, or tell them error, etc	
		}
		
		
		 
		if (GUI.Button (new Rect (x, y + offset * 4 + 50, width, 20), "Quit to Main Menu")) {
			Application.LoadLevel ("Menu");
		}
			
	}
	
	private void draw ()
	{
		//firstPadding goes between music slider and sfx button
		float padding = 10;
		
		//used for centering components
		//yourScore.height * 2 because score is same height as the "Your Score" image
		float totalHeight = yourScore.height * 2 + namePanel.height + sendPanel.height + 3 * padding;
		float current = (Screen.height - totalHeight) / 2;
		
		//score
		Rect scoreR = new Rect (centerX (yourScore.width), current, yourScore.width, yourScore.height);
		current += yourScore.height + padding;
		
		//no rect for actual score, but increment height cause it's still displayed
		Rect scoreActualR = new Rect (centerX (yourScore.width), current, yourScore.width, yourScore.height);
		current += yourScore.height + padding;
		
		//name panel
		Rect nameR = new Rect (centerX (namePanel.width), current, namePanel.width, namePanel.height);
		Rect inputR = new Rect(nameR.x + nameR.width * 0.43f, nameR.y + nameR.height * 0.308f, nameR.width * 0.5f, nameR.height * 0.4f);
		current += namePanel.height + padding;
		
		Rect sendR = new Rect (centerX (sendPanel.width), current, sendPanel.width, sendPanel.height);
		
		//now draw them
		GUI.DrawTexture (scoreR, yourScore);
		//GUI.Label ();
		
		GUI.DrawTexture (nameR, namePanel);
		//name stuff
		GUI.SetNextControlName("text");
		currentName = GUI.TextField (inputR, currentName, 12);
		while (currentName.Length > 0 && !Regex.IsMatch(currentName, "^[a-zA-Z0-9]+$")) {
			incorrectTime = 4.5f;
			currentName = currentName.Substring (0, currentName.Length - 1);
		}
		GUI.FocusControl("text");
	
		//draw name
		GUI.DrawTexture (sendR, sendPanel);
		if (GUI.Button (sendR, send)) {
			if (currentName.Length != 0) hs.submitHighscore (currentName, Player.getScore ());
			Application.LoadLevel ("Menu");
		}
	}
	
	private void promptHighscore ()
	{
		
	}
	
	private void submittedHighscore ()
	{
		
	}
	
	// Returns the X value that is the left of object with given width
	private float centerX (float width)
	{
		return (Screen.width - width) / 2.0f;
	}
}
