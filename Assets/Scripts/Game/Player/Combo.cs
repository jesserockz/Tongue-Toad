using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour {
	
	public GameObject[] comboSigns;
	public Texture2D[] comboNumTextures;
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void spawnCombo(Enemy enemy, int combo)
	{
		int i = Mathf.Clamp (combo - 1, 0, comboSigns.Length - 1);
		
		GameObject o = comboSigns[i];
		
		Vector3 pos = enemy.gameObject.transform.position;
		Instantiate (o, pos, Quaternion.Euler (90, -180, 0));
	}
}
