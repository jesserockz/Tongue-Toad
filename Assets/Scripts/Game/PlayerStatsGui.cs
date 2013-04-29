using UnityEngine;
using System.Collections;

public class PlayerStatsGui : MonoBehaviour {

	Player player;
	// Use this for initialization
	void Start () {
		GameObject p = GameObject.Find ("Player");
		player = p.GetComponent<Player>();
		Debug.Log (player);
	}
	
	//temp method atm just to have stats on screen
    //draws health/ energy/ score
    void OnGUI()
    {
        int lX = 25;
        int lY = 50;
        int i = 0; 
        GUI.Box(new Rect(10, 10, 100, 150), "Stats");
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "health: " + player.getHealth());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "energy: " + player.getEnergy());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "score: " + Player.getScore ());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "combo: " + player.getCombo());
		 
        //anticheat box
        if (Player.hasCheated) GUI.Box(new Rect(Screen.width - 150 - 10, lY, 150, 30), "Cheating was detected");

        if (player.getEnergy() == 0)
        {
            float w = 220;
            float h = 23;
            float x = (float)(Screen.width - w) / 2;
            float y = (float)(Screen.height * 0.75);
#if UNITY_IPHONE || UNITY_ANDROID
			GUI.Box(new Rect(x,y,w,h), "Shake to renew energy!");
#else
            GUI.Box(new Rect(x, y, w, h), "Hit Enter/ End to renew energy!");
#endif
        }
    }
}
