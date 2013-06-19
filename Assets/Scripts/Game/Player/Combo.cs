using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour
{
	
	public GameObject[] comboSigns;
	public Texture2D[] comboNumTextures;
	private int currentDisplay;
	private float timeLeft;
	private float disappearTime = 1;
	private float alpha = 0;
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnGUI ()
	{
		if (alpha <= 0) return;
		
		timeLeft -= Time.deltaTime;
		
		if (timeLeft < 0) {
			alpha -= Time.deltaTime * (1.0f / disappearTime);
			
			Color c = GUI.color;
			c.a = alpha;
			GUI.color = c;
		}
		
		Texture2D tex = comboNumTextures[Mathf.Clamp(currentDisplay - 2, 0, comboSigns.Length - 1)];
		
		float h = comboNumTextures[0].height * 0.6f;
		float w = comboNumTextures[0].width * 0.6f;
		
		Rect r = new Rect(5, 180, w, h);
		GUI.DrawTexture(r, tex);
	}
	
	public void spawnCombo (Enemy enemy, int combo)
	{
		//bring up combo gui thing
		alpha = 1;
		timeLeft = 4;
		currentDisplay = combo;
		
		//spawn enemy signs
		int i = Mathf.Clamp (combo - 2, 0, comboSigns.Length - 1);
		
		GameObject o = comboSigns [i];
		
		Vector3 pos = enemy.gameObject.transform.position;
		Instantiate (o, pos, Quaternion.Euler (90, -180, 0));
	}
}
