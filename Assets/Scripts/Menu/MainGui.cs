using UnityEngine;
using System.Collections.Generic;
using System;

public class MainGui : MonoBehaviour
{
	//main heading
	public Texture2D titleTex;
	
	//individual buttons
	public Texture2D newGameTex, instructionsTex, highScoreTex, optionsTex, creditsTex, exitGameTex;
	public GUISkin guiSkin;
	public Font otherFont, hssFont, hssUnderfont;
	public Texture2D hsNumbers;
	public Texture2D hsRefresh, hsReturn;
	
	//options menu textures
	public Texture2D playMusic, muteMusic, playSFX, muteSFX, returnToMenu;
	
	//private stuff
	private List<ButtonItem> buttons = new List<ButtonItem> ();
	private Mode mode;
	private TestHtml hs;
	
	// Use this for initialization
	void Start ()
	{
		GameObject o = GameObject.Find ("HighScoreInterface");
		
		hs = o.GetComponent<TestHtml>();
		
		mode = Mode.Menu;
		
		buttons.Add (new ButtonItem (newGameTex, newGame));
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
		
	private void highScore ()
	{
		mode = Mode.Highscore;
		hs.reloadCoroutine ();
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
		
		GUI.DrawTexture (new Rect (centerX (titleTex.width * scale), y, titleTex.width * scale, titleTex.height * scale), titleTex);

		y += titleTex.height * scale;

		float padding = 7;
		
		float current = y + ((Screen.height - y) - buttons.Count * (buttons [0].texture.height + padding)) / 2 - padding * 2;
		
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
		
		if (TestHtml.loading)
			GUI.Label (new Rect (centerX (loadLength), Screen.height * 0.4f, loadLength, 50), "Loading from highscore server...");
		else if (TestHtml.error)
			GUI.Label (new Rect (centerX (conLength), Screen.height * 0.4f, conLength, 50), "Couldn't connect to highscore server");
		else {
			string[] names = TestHtml.contents.Split ('\n');

			float maxWidth = style.CalcSize (new GUIContent ("MMMMMMMMMMMM")).x;
			float numWidth = GUI.skin.GetStyle ("label").CalcSize (new GUIContent ("10.")).x;
			
			float dy = 33;
			float iy = y - 10 * dy - 50;
			
			for (int i = 0; i < 10; i++) {
				string name = "-";
				string score = "0";
				
				if ((i + 2) < names.Length && names [i + 2].Length != 0) {
					string[] info = names [i + 2].Split (' ');
					name = info [0].Split ('=') [1];
					score = info [1].Split ('=') [1];
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
		}
		
		if (GUI.Button (new Rect (centerX (hsRefresh.width), y - 30, hsRefresh.width, hsRefresh.height), hsRefresh)) {
			hs.reloadCoroutine ();
		}
		
		if (GUI.Button (new Rect (centerX (hsReturn.width), y - 30 + hsRefresh.height, hsReturn.width, hsReturn.height), hsReturn)) {
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
		float sliderHeight = 20;
		float sliderWidth = 120;
		
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
		float w = 350;
		float h = 40;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.height * 0.2f;
		float iy = h;
		int i = 0;
		
		List<string> credits = new List<string> ();
		credits.Add ("Programmer: Troy Shaw");
		credits.Add ("Programmer: Jesse Hills");
		credits.Add ("Artist: Kiran Matthews");
		credits.Add ("Artist: Jordan Dai");
		credits.Add ("Artist: Bat Mandoza");
		credits.Add ("Artist: Loxy Reid");
		
		for (i = 0; i < credits.Count; i++) {
			GUI.Label (new Rect (x, y + i * iy, w, h), credits [i]);
		}
		
		if (GUI.Button (new Rect (centerX (returnToMenu.width), y + iy * (i + 2), returnToMenu.width, returnToMenu.height), returnToMenu)) {
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

