using UnityEngine;
using System.Collections.Generic;

public class LocalHighscore : MonoBehaviour
{
	
	public static List<HighscoreEntry> highscores = new List<HighscoreEntry> ();
	
	static LocalHighscore ()
	{
		addScore ("Jesse", 1566);
		addScore ("Jesserockz", 1267);
		addScore ("zinky", 900);
		addScore ("Troy", 763);
		addScore ("shame", 209);
		addScore ("bob", 150);
		addScore ("Jim", 100);
		addScore ("Zxiox", 87);
		addScore ("JimmyBob", 71);
		addScore ("Cloud", 50);
	}
	
	public static int addScore (string name, int score)
	{
		HighscoreEntry e = new HighscoreEntry (name, score);
		
		highscores.Add (e);
		highscores.Sort ((a,b) => b.score - a.score);
		
		return highscores.IndexOf (e) + 1;
	}
}

public class HighscoreEntry
{
	public string name;
	public int score;
	
	public HighscoreEntry (string name, int score)
	{
		this.name = name;
		this.score = score;
	}
}