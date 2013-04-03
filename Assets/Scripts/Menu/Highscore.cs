using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// A highscore system.
/// Currently only local highscore, but will make online later.
/// </summary>
public class Highscore : MonoBehaviour
{
	
	static Highscore ()
	{
		
		
	}
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	public static List<string> getHighscores ()
	{
		return null;
	}
	
	public List<string> getOnlineHighscores ()
	{
		string url = "http://www.troyshaw.co.nz/hs.php?mode=get";
		string scoreSource = "";
	
		WWW source = new WWW (url);
		StartCoroutine(WaitForRequest(source));;
		
		Debug.Log (source.text);
		
		return null;
	}
	
	private IEnumerator WaitForRequest (WWW www)
	{
		yield return www;
	}
}
