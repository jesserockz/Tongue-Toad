using UnityEngine;
using System.Collections.Generic;
using System;

public class MainGui : MonoBehaviour
{
	
	private List<ButtonItem> buttons = new List<ButtonItem> ();
	
	private Mode mode;
	
	private enum Mode {
		Menu,
		Options,
		Highscore
	};
	
	
	
	// Use this for initialization
	void Start ()
	{
		mode = Mode.Menu;
		ButtonItem b1 = new ButtonItem("New Game", newGame);
		ButtonItem b2 = new ButtonItem("Highscores", highScore);
		ButtonItem b3 = new ButtonItem("Options", options);
		ButtonItem b4 = new ButtonItem("Exit", exit);
		
		Debug.Log(Application.platform);
		
		Debug.Log (Application.platform);
		
		buttons.Add (b1);
		buttons.Add (b2);
		buttons.Add (b3);
		//only add exit button if not a web plugin
		if (!Application.isWebPlayer) buttons.Add (b4);
	}
	
	private void newGame() {
		Application.LoadLevel("Game");
	}
		
	private void highScore() {
		mode = Mode.Highscore;
	}
	
	private void options() {
		mode = Mode.Options;
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
		switch (mode) {
		case Mode.Menu: drawMenu(); break;
		case Mode.Highscore: drawHighscore(); break;
		case Mode.Options: drawOptions(); break;
		}
	}
	
	private void drawMenu() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Main Menu");
		
		float w = 120;
		float h = 20;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.width * 0.3f;
		float iy = 40;
		float dx = h + 10;
		
		for (int i = 0; i < buttons.Count; i++) {
			ButtonItem bi = buttons[i];
			if (GUI.Button(new Rect(x, y + iy * i, w, h), bi.name)) bi.action();
		}
	}
	
	private void drawHighscore() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Highscore");
		
		//highscore drawing code goes here
	}
	
	private void drawOptions() {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Options");
		
		float w = 120;
		float h = 20;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.width * 0.3f;
		float iy = 30;
		float dx = h + 10;
		int i = 0;
		//music
		
		if(GUI.Button(new Rect(x, y + iy * i++, w, h), SoundManager.muteMusic ? "Play Music" : "Mute Music")) {
			SoundManager.toggleMusic();
		}
		
		SoundManager.setMusic(GUI.HorizontalSlider(new Rect(x, y + iy * i++, w, h), SoundManager.musicLevel, 0, 1));
		
		//sound effects
		
		if(GUI.Button(new Rect(x, y + iy * i++, w, h), SoundManager.muteEffects ? "Play Effects" : "Mute Effects")) {
			SoundManager.toggleEffects();
		}
		
		SoundManager.setEffects(GUI.HorizontalSlider(new Rect(x, y + iy * i++, w, h), SoundManager.effectsLevel, 0, 1));
		
		if(GUI.Button(new Rect(x, y + iy * i++, w, h), "Return to menu")) {
			mode = Mode.Menu;
		}
		
	}
}

struct ButtonItem
{
	public String name;
	public Action action;
			
	public ButtonItem (String name, Action action)
	{
		this.name = name;
		this.action = action;
	}
}