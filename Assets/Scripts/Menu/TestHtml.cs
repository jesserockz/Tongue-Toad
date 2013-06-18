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
	
	private void loadStaticScore()
	{
		
		loading = false;
		error = false;
		
		contents = 
"version=1"
+ "\nnumscores=16"
+ "\nname=God score=2686"
+ "\nname=Jesse score=1566"
+ "\nname=Jesserockz score=1267"
+ "\nname=zinky score=891"
+ "\nname=Troy score=380"
+ "\nname=shame score=215"
+ "\nname=bob score=50"
+ "\nname=bob score=20"
+ "\nname=bob score=20"
+ "\nname=bob score=20"
+ "\nname=John score=0"
+ "\nname=John score=0"
+ "\nname=Troy score=0"
+ "\nname=Troy score=0"
+ "\nname=John score=0"
+ "\nname=John score=0";
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


