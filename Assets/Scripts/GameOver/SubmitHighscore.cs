using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class SubmitHighscore : MonoBehaviour
{
	public static bool loading;
	public static bool error = true;
	public static string placing;
	private string url = "http://www.troyshaw.co.nz/toad/hs.php?mode=set&";
	private string secretKey = "thisKeyIsSecret";
	
	public void submitHighscore (string name, int score)
	{
		loading = true;
		
		StartCoroutine (WaitForRequest (name, score));
	}
	
	private IEnumerator WaitForRequest (string name, int score)
	{
		string checksum = Md5Sum (name + score + secretKey);
		
		
		WWW w = new WWW (url + "name=" + name + "&score=" + score + "&checksum=" + checksum);
		yield return w;
		
		Debug.Log (w.error);
		loading = false;
		error = w.error != null;
		placing = null;
		
		if (!error) {
			placing = w.text;
		}
	}
	
	public  string Md5Sum (string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding ();
		byte[] bytes = ue.GetBytes (strToEncrypt);
 
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider ();
		byte[] hashBytes = md5.ComputeHash (bytes);
 
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
 
		for (int i = 0; i < hashBytes.Length; i++) {
			hashString += System.Convert.ToString (hashBytes [i], 16).PadLeft (2, '0');
		}
 
		return hashString.PadLeft (32, '0');
	}
}
