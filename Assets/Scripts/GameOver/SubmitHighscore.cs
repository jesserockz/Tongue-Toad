using UnityEngine;
using System.Collections;

public class SubmitHighscore : MonoBehaviour {

	public static bool loading;
	public static bool error = true;
	public static string placing;
	private string url = "http://www.troyshaw.co.nz/toad/hs.php?mode=set&";	
	
	public void submitHighscore (string name, int score)
	{
		loading = true;
		
		StartCoroutine(WaitForRequest(name, score));
	}
	
	private IEnumerator WaitForRequest (string name, int score)
	{
		WWW w = new WWW(url + "name=" + name + "&score=" + score);
        yield return w;
		
		Debug.Log (w.error);
		loading = false;
		error = w.error != null;
		placing = null;
		
		if (!error) {
			placing = w.text;
		}
	}
}
