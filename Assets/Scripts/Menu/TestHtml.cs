using UnityEngine;
using System.Collections;

public class TestHtml : MonoBehaviour {
	
	public static bool loading;
	public static bool error = true;
	public static string contents;
	private string url = "http://www.troyshaw.co.nz/toad/hs.php?mode=get";
	
    IEnumerator Start() {
        WWW www = new WWW(url);
        yield return www;
		if (www.error != null) {
			Debug.Log (www.error);
		} else {
			//Debug.Log (www.text);
		}
    }
	
	
	public void reloadCoroutine ()
	{
		loading = true;
		
		StartCoroutine(WaitForRequest());
	}
	
	private IEnumerator WaitForRequest ()
	{
		WWW w = new WWW(url);
        yield return w;
		
		loading = false;
		error = w.error != null;
		contents = null;
		
		if (!error) {
			contents = w.text;
		}
	}
}
