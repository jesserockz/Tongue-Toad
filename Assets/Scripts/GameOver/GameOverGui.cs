using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public class GameOverGui : MonoBehaviour
{
	public GUISkin guiSkin;
	public Texture2D yourScore, send, sendPanel, namePanel;
	private string currentName = "";
	private float incorrectTime = 0;
	private bool submitted = false;
	private SubmitHighscore hs;
	
	//sending true while data is being sent, sent is true when the result comes back
	private bool sending, sent;
	//time we sent the score
	private float sendTime;
	//max time we'll wait before we declare bad connection
	private float timeout = 6, displayFinal = 3;
	//string to display in final bit
	private string displayString;
	
	void Start ()
	{
		hs = GetComponent<SubmitHighscore> ();
	}
	 
	void OnGUI ()
	{
		if (guiSkin != null) GUI.skin = guiSkin;
		
		if (sent) {
			sendTime += Time.deltaTime;
			
			if (sendTime > displayFinal || Input.GetKey (KeyCode.Escape)) {
				Application.LoadLevel ("Menu");
			}
			
			
			displayCenter("Congratulations, you've come in position " + SubmitHighscore.placing + "!");
			
		} else if (sending) {
			string s = "Submitting highscore to server";
			for (int j = 0; j < (sendTime % 3); j++)
				s += ".";
			
			displayCenter(s);
			
			sendTime += Time.deltaTime;
			
			if (sendTime > timeout || !SubmitHighscore.loading) {
				sent = true;
				sendTime = 0;
			}
		} else {
			draw ();
		}
	}
	
	private void draw ()
	{
		//padding between each component
		float padding = 10;
		
		//used for centering components
		//yourScore.height * 2 because score is same height as the "Your Score" image
		float totalHeight = yourScore.height * 2 + namePanel.height + sendPanel.height + 3 * padding;
		float current = (Screen.height - totalHeight) / 2;
		
		//score
		Rect scoreR = new Rect (centerX (yourScore.width), current, yourScore.width, yourScore.height);
		current += yourScore.height + padding;
		
		//score number
		float width = GUI.skin.GetStyle ("label").CalcSize (new GUIContent (Player.getScore ().ToString ())).x;
		Rect scoreActualR = new Rect (centerX (width), current, yourScore.width, yourScore.height);
		current += yourScore.height + padding;
		
		//name panel
		Rect nameR = new Rect (centerX (namePanel.width), current, namePanel.width, namePanel.height);
		Rect inputR = new Rect (nameR.x + nameR.width * 0.43f, nameR.y + nameR.height * 0.308f, nameR.width * 0.5f, nameR.height * 0.4f);
		current += namePanel.height + padding;
		
		Rect sendR = new Rect (centerX (sendPanel.width), current, sendPanel.width, sendPanel.height);
		
		//now draw them
		GUI.DrawTexture (scoreR, yourScore);
		GUI.Label (scoreActualR, Player.getScore ().ToString ());
		//GUI.Label ();
		
		GUI.DrawTexture (nameR, namePanel);
		//name stuff
		GUI.SetNextControlName ("text");
		currentName = GUI.TextField (inputR, currentName, 12);
		while (currentName.Length > 0 && !Regex.IsMatch(currentName, "^[a-zA-Z0-9]+$")) {
			incorrectTime = 4.5f;
			currentName = currentName.Substring (0, currentName.Length - 1);
		}
		GUI.FocusControl ("text");
	
		//draw name
		GUI.DrawTexture (sendR, sendPanel);
		if (GUI.Button (sendR, send)) {
			if (currentName.Length != 0) {
				hs.submitHighscore (currentName, Player.getScore ());
				sending = true;
			} else {
				Application.LoadLevel ("Menu");
			}
		}
	}
	
	private void displayCenter(string s) 
	{
		float width = GUI.skin.GetStyle ("label").CalcSize (new GUIContent (s)).x + 5;
		GUI.Label (new Rect (centerX(width), Screen.height * 0.4f, width, 40), s);
	}
	
	// Returns the X value that is the left of object with given width
	private float centerX (float width)
	{
		return (Screen.width - width) / 2.0f;
	}
}
