using UnityEngine;
using System.Collections.Generic;
using System;

public class MainGui : MonoBehaviour
{
	//main heading
	public Texture2D titleTex;
	
	//individual buttons
	public Texture2D newGameTex, tutTex, instructionsTex, highScoreTex, optionsTex, creditsTex, exitGameTex;
	public GUISkin guiSkin;
	public Font otherFont, hssFont, hssUnderfont;
	public Texture2D hsRefresh, hsReturn;
	
	//options menu textures
	public Texture2D playMusic, muteMusic, playSFX, muteSFX, returnToMenu;
	
	//panels texture
	public Texture2D creditPanelTex, highscorePanelTex;
	
	//private stuff
	private List<ButtonItem> buttons = new List<ButtonItem> ();
	private Mode mode;
	private TestHtml hs;
	
	// Use this for initialization
	void Start ()
	{
		//GameObject o = GameObject.Find ("HighScoreInterface");
		
		//hs = o.GetComponent<TestHtml>();
		
		mode = Mode.Menu;
		
		buttons.Add (new ButtonItem (newGameTex, newGame));
		buttons.Add (new ButtonItem (tutTex, tutorial));
		buttons.Add (new ButtonItem (highScoreTex, highScore));
		buttons.Add (new ButtonItem (optionsTex, options));
		buttons.Add (new ButtonItem (creditsTex, credits));
		//only add exit button if not a web plugin
		if (!Application.isWebPlayer)
			buttons.Add (new ButtonItem (exitGameTex, exit));
	}
	
	private void newGame ()
	{
		Application.LoadLevel ("Game");
	}
	
	private void tutorial()
	{
		
	}
		
	private void highScore ()
	{
		mode = Mode.Highscore;
		//hs.reloadCoroutine ();
	}
	 
	private void options ()
	{
		mode = Mode.Options;
	}
	
	private void credits ()
	{
		mode = Mode.Credits;
	}
	
	private void exit ()
	{
		Application.Quit ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape))
			mode = Mode.Menu;
	}
	
	void OnGUI ()
	{
		if (guiSkin != null)
			GUI.skin = guiSkin;
		
		switch (mode) {
		case Mode.Menu:
			drawMenu ();
			break;
		case Mode.Highscore:
			drawHighscore ();
			break;
		case Mode.Options:
			drawOptions ();
			break;
		case Mode.Credits:
			drawCredits ();
			break;
		}
	}
	
	private void drawMenu ()
	{
		float w = 120;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.height * 0.03f;

		float scale = 0.7f;
		
		float padding = 5;
		
		GUI.DrawTexture (new Rect (centerX (titleTex.width * scale), y - 3 * padding, titleTex.width * scale, titleTex.height * scale), titleTex);

		y += titleTex.height * scale;

		
		float current = y + ((Screen.height - y) - buttons.Count * (buttons [0].texture.height + padding)) / 2 - padding * 4;
		
		for (int i = 0; i < buttons.Count; i++) {
			ButtonItem bi = buttons [i];
			Texture2D tex = bi.texture;
			
			if (GUI.Button (new Rect (centerX (tex.width), current, tex.width, tex.height), tex))
				bi.action ();
			
			current += tex.height + padding;
		}
	}
	
	private void drawHighscore ()
	{
		float y = Screen.height * 0.75f;
		
		GUIStyle style = new GUIStyle ();
		style.font = hssFont;
		
		Vector2 loadLen = GUI.skin.GetStyle ("label").CalcSize (new GUIContent ("Loading from highscore server..."));
		float loadLength = loadLen.x;
		
		Vector2 loadLen2 = GUI.skin.GetStyle ("label").CalcSize (new GUIContent ("Couldn't connect to highscore server"));
		float conLength = loadLen2.x;

		float maxWidth = style.CalcSize (new GUIContent ("MMMMMMMMMMMM")).x;
		float numWidth = GUI.skin.GetStyle ("label").CalcSize (new GUIContent ("10.")).x;
			
		float dy = 33;
		float iy = y - 10 * dy - 50;
		
		//location of the backing panel. Y coordinate of first name is 73 pixels down into panel 
		Rect r = new Rect(centerX (highscorePanelTex.width * 0.9f), iy - 73, highscorePanelTex.width, highscorePanelTex.height);
		r.width *= 0.9f;
		r.height *= 0.8f; 
		
		GUI.DrawTexture(r, highscorePanelTex);
		
		for (int i = 0; i < 10; i++) {
			string name = "-";
			string score = "0";

			if (i < LocalHighscore.highscores.Count) {
				name = LocalHighscore.highscores[i].name;
				score = LocalHighscore.highscores[i].score.ToString();
			}
				
			//display the score number
			GUI.Label (new Rect (centerX (maxWidth) - numWidth - 5, iy + dy * i, 200, 35), (i + 1) + ". ");
				
			//green underlay of name
			style.normal.textColor = Color.green;
			GUI.Label (new Rect (centerX (maxWidth), iy + dy * i + 8, 200, 30), name, style);
				 
			//yellow overtop of name
			style.normal.textColor = Color.yellow;
			GUI.Label (new Rect (centerX (maxWidth), iy + dy * i + 8 - 1, 200, 30), name, style);
				
			//display their score
			GUI.Label (new Rect (centerX (maxWidth) + maxWidth + 5, iy + dy * i, 200, 35), score);
		}
		
		
		if (GUI.Button (new Rect (centerX (hsRefresh.width), iy + dy * 11, hsRefresh.width, hsRefresh.height), hsRefresh)) {
			//hs.reloadCoroutine ();
		}
		
		if (GUI.Button (new Rect (centerX (hsReturn.width), iy + dy * 11 + hsRefresh.height, hsReturn.width, hsReturn.height), hsReturn)) {
			mode = Mode.Menu;
		}
	}
	
	private void drawOptions ()
	{
		//firstPadding goes between music slider and sfx button
		float firstPadding = 10;
		
		//secondPadding goes after sfx slider
		float secondPadding = 20;
		
		//slider dimensions
		float sliderWidth = 358;
		float sliderHeight = 27;
		
		//sliderWidth = 
		
		//used for centering components
		float totalHeight = playMusic.height + playSFX.height + sliderHeight * 2 + returnToMenu.height + firstPadding + secondPadding;
		float current = (Screen.height - totalHeight) / 2;
		
		//music items
		Rect musicR = new Rect (centerX (playMusic.width), current, playMusic.width, playMusic.height);
		current += playMusic.height;
		
		Rect musicSliderR = new Rect (centerX (sliderWidth), current, sliderWidth, sliderHeight);
		current += sliderHeight + firstPadding;
		
		//sfx items
		Rect sfxR = new Rect (centerX (playMusic.width), current, playSFX.width, playSFX.height);
		current += playSFX.height;
		
		Rect sfxSliderR = new Rect (centerX (sliderWidth), current, sliderWidth, sliderHeight);
		current += sliderHeight + secondPadding;
		
		//menu button
		Rect menuR = new Rect (centerX (returnToMenu.width), current, returnToMenu.width, returnToMenu.height);
		
		//now draw them
		if (GUI.Button (musicR, SoundManager.muteMusic ? playMusic : muteMusic))
			SoundManager.toggleMusic ();
		
		SoundManager.setMusic (GUI.HorizontalSlider (musicSliderR, SoundManager.musicLevel, 0, 1));
		
		if (GUI.Button (sfxR, SoundManager.muteEffects ? playSFX : muteSFX))
			SoundManager.toggleEffects ();
		
		SoundManager.setEffects (GUI.HorizontalSlider (sfxSliderR, SoundManager.effectsLevel, 0, 1));
		
		if (GUI.Button (menuR, returnToMenu))
			mode = Mode.Menu;
	}
	
	private void drawCredits ()
	{
		float pad = 20;
		float totalY = creditPanelTex.height + returnToMenu.height + pad;
		
		Rect creditRect = new Rect(centerX (creditPanelTex.width), (Screen.height - totalY) / 2, creditPanelTex.width, creditPanelTex.height);
		Rect buttonRect = new Rect (centerX (returnToMenu.width), (Screen.height - totalY) / 2 + creditPanelTex.height + pad, returnToMenu.width, returnToMenu.height);
		
		GUI.DrawTexture(creditRect, creditPanelTex);
		
		if (GUI.Button (buttonRect, returnToMenu)) {
			mode = Mode.Menu;
		}
	}
	
	// Returns the X value that is the left of object with given width
	private float centerX (float width)
	{
		return (Screen.width - width) / 2.0f;
	}
}

enum Mode
{
	Menu,
	Options,
	Highscore,
	Credits
};

struct ButtonItem
{
	public Action action;
	public Texture2D texture;
			
	public ButtonItem (Texture2D texture, Action action)
	{
		this.texture = texture;
		this.action = action;
	}
}

