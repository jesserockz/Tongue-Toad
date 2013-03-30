using UnityEngine;
using System.Collections.Generic;
using System;

public class MainGui : MonoBehaviour
{
	
	private List<ButtonItem> buttons = new List<ButtonItem> ();
	
	// Use this for initialization
	void Start ()
	{
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
	Debug.Log ("highscore");
	}
	
	private void options() {
		Debug.Log ("options");
	}
	
	private void exit() {
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnGUI ()
	{
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Main Menu");
		
		float w = 100;
		float h = 20;
		float x = (Screen.width - w) / 2.0f;
		float y = Screen.width * 0.4f;
		float iy = 40;
		float dx = h + 10;
		
		for (int i = 0; i < buttons.Count; i++) {
			ButtonItem bi = buttons[i];
			if (GUI.Button(new Rect(x, y + iy * i, w, h), bi.name)) bi.action();
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