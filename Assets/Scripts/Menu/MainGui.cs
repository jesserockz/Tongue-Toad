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
	
	private List<ButtonItem> buttons = new List<ButtonItem> ();
	
	private Mode mode;
	
	private TestHtml hs;
	
	
	private enum Mode {
		Menu,
		Options,
		Highscore,
		Credits
	};
	
	// Use this for initialization
	void Start ()
	{
		GameObject o = GameObject.Find("HighScoreInterface");
		
		hs = (TestHtml)o.GetComponent("TestHtml");
		
		mode = Mode.Menu;
		ButtonItem b1 = new ButtonItem("New Game", newGameTex, newGame);
		ButtonItem b2 = new ButtonItem("Highscores", highScoreTex, highScore);
		ButtonItem b3 = new ButtonItem("Options", optionsTex, options);
		ButtonItem b4 = new ButtonItem("Credits", creditsTex, credits);
		ButtonItem b5 = new ButtonItem("Exit", exitGameTex, exit);
		
		buttons.Add (b1);
		buttons.Add (b2);
		buttons.Add (b3);
		buttons.Add (b4);
		//only add exit button if not a web plugin
		if (!Application.isWebPlayer) buttons.Add (b5);
	}
	
	private void newGame() {
		Application.LoadLevel("Game");
	}
		
	private void highScore() {
		Debug.Log(hs);
		mode = Mode.Highscore;
		hs.reloadCoroutine();
	}
	 
	private void options() {
		mode = Mode.Options;
	}
	
	private void credits() {
		mode = Mode.Credits;
	}
	
	private void exit() {
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) mode = Mode.Menu;
	}
	
	void OnGUI ()
	{
		if (guiSkin != null) GUI.skin = guiSkin;
		
		switch (mode) {
		case Mode.Menu: drawMenu(); break;
		case Mode.Highscore: drawHighscore(); break;
		case Mode.Options: drawOptions(); break;
		case Mode.Credits: drawCredits(); break;
		}
	}
	
	private void drawMenu() {
		float w = 120;
		float h = 20;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.height * 0.03f;
		float iy = 40;
		//float dx = h + 10;
		
		float scale = 0.7f;
		
		GUI.DrawTexture(new Rect((Screen.width - titleTex.width * scale) / 2, y, titleTex.width * scale, titleTex.height * scale), titleTex);
		
		y += titleTex.height * scale;
		
		for (int i = 0; i < buttons.Count; i++) {
			ButtonItem bi = buttons[i];
			Texture2D tex = bi.texture;
			
			if (GUI.Button(new Rect((Screen.width - tex.width) / 2, y, tex.width, tex.height), tex)) bi.action();
			
			y += tex.height;
		}
	}
	
	private void drawHighscore() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		
		//highscore drawing code goes here
		
		float w = 200;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.height * 0.7f;
		//float iy = 40;
		//float dx = h + 10;
		
		//Font oldFont = GUI.skin.font;
		//GUI.skin.font = hsFont;
		GUIStyle style = new GUIStyle();
		style.font = hssFont;
		
		Vector2 loadLen = GUI.skin.GetStyle("label").CalcSize (new GUIContent("Loading from highscore server..."));
		float loadLength = loadLen.x;
		
		Vector2 loadLen2 = GUI.skin.GetStyle("label").CalcSize (new GUIContent("Couldn't connect to highscore server"));
		float conLength = loadLen2.x;
		
		if (TestHtml.loading) GUI.Label (new Rect((Screen.width - loadLength) / 2, Screen.height * 0.4f, loadLength, 50), "Loading from highscore server...");
		else if (TestHtml.error) GUI.Label (new Rect((Screen.width - conLength) / 2, Screen.height * 0.4f, conLength, 50), "Couldn't connect to highscore server");
		else {
			string[] names = TestHtml.contents.Split('\n');

			
			for (int i = 2; i < names.Length && i < 12 && names[i].Length != 0; i++) {
				string[] info = names[i].Split(' ');
				string name = info[0].Split('=')[1];
				string score = info[1].Split('=')[1];
				
				//GUI.skin.font = oldFont;
				GUI.Label (new Rect(Screen.width / 2 - 100, 50 + 25 * i, 200, 35), (i - 1) + ". ");
				
				Vector2 size = GUI.skin.GetStyle("label").CalcSize (new GUIContent((i - 1) + ". "));
				
				
				
				
				//green underlay
				style.normal.textColor = Color.green;
				GUI.TextArea (new Rect(Screen.width / 2 - 100 + 40, 50 + 25 * i - 1 + 5, 200, 30), name, style);
				
				//yellow overtop
				style.normal.textColor = Color.yellow;
				GUI.Label (new Rect(Screen.width / 2 - 100 + 40, 50 + 25 * i + 5, 200, 30), name, style);
				
				
				
				//GUI.skin.font = oldFont;
				GUI.Label (new Rect(Screen.width / 2 + 100, 50 + 25 * i, 200, 35), score);
			}
		}
		
		//GUI.skin.font = oldFont;
		
		if(GUI.Button(new Rect((Screen.width - hsRefresh.width) / 2, y - 30, hsRefresh.width, hsRefresh.height), hsRefresh)) {
			hs.reloadCoroutine();
		}
		
		if(GUI.Button(new Rect((Screen.width - hsReturn.width) / 2, y - 30 + hsRefresh.height, hsReturn.width, hsReturn.height), hsReturn)) {
			mode = Mode.Menu;
		}
	}
	
	private void drawOptions() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Options");
		
		float w = 120;
		float h = 20;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.width * 0.3f;
		float iy = 30;
		//float dx = h + 10;
		int i = 0;
		//music
		
		if(GUI.Button(new Rect(x, y + iy * i++, w, h), SoundManager.muteMusic ? "Play Music" : "Mute Music")) {
			SoundManager.toggleMusic();
		}
		
		SoundManager.setMusic(GUI.HorizontalSlider(new Rect(x, y + iy * i++, w, h + 20), SoundManager.musicLevel, 0, 1));
		
		//sound effects
		
		if(GUI.Button(new Rect(x, y + iy * i++, w, h), SoundManager.muteEffects ? "Play Effects" : "Mute Effects")) {
			SoundManager.toggleEffects();
		}
		
		SoundManager.setEffects(GUI.HorizontalSlider(new Rect(x, y + iy * i++, w, h), SoundManager.effectsLevel, 0, 1));
		
		if(GUI.Button(new Rect(x, y + iy * i++, w, h), "Return to menu")) {
			mode = Mode.Menu;
		}
	}
	
	private void drawCredits() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Credits");
		
		float w = 200;
		float h = 25;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.width * 0.2f;
		float iy = 20;
		//float dx = h + 10;
		int i = 0;
		
		List<string> credits = new List<string>();
		credits.Add("Programmer: Troy Shaw");
		credits.Add("Programmer: Jesse Hills");
		credits.Add("Artist: Kiran Matthews");
		credits.Add("Artist: Jordan Dai");
		credits.Add("Artist: Bat Mandoza");
		credits.Add("Artist: Loxy Reid");
		
		for (i = 0; i < credits.Count; i++) {
			GUI.Label (new Rect(x, y + i * iy, w, h), credits[i]);
		}
		
		if(GUI.Button(new Rect(x, y + iy * (i + 3), w, h), "Return to menu")) {
			mode = Mode.Menu;
			
		}
	}
}

struct ButtonItem
{
	public String name;
	public Action action;
	public Texture2D texture;
			
	public ButtonItem (String name, Texture2D texture, Action action)
	{
		this.name = name;
		this.texture = texture;
		this.action = action;
	}
}

