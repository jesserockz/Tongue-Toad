using UnityEngine;
using System.Collections;

public class PlayerStatsGui : MonoBehaviour {
	
	private string healthBase = "GUI/health/Health";
	private string healthExtension = "";
	
	public Texture2D healthBaseImage;
	Texture2D[] healthBarImages = new Texture2D[101];
	private Rect healthLocation = new Rect(0, 0, Screen.width * 0.2f, Screen.width * 0.2f);
	private float healthAnimationIndex = 100;
	
	Player player;
	// Use this for initialization
	void Start () {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.GetComponent<Player>();
		
		for (int i = 0; i <= 100; i++) {
			string imageIndex = (i < 10) ? "0" + i : i.ToString();
			healthBarImages[i] = Resources.Load(healthBase + imageIndex + healthExtension) as Texture2D;
		}
	}
	
	//temp method atm just to have stats on screen
    //draws health/ energy/ score
    void OnGUI()
    {
		int wi = Screen.width;
		int hi = Screen.height;
		
		
		//get health, then adjust the animation index
		int health = Mathf.Clamp(player.getHealth(), 0, 100);
		
		if (health < healthAnimationIndex) healthAnimationIndex -= 0.5f;
		else if (health > healthAnimationIndex) healthAnimationIndex += 0.5f;
		
		//draw the actual image
		GUI.DrawTexture(healthLocation, healthBaseImage);
		GUI.DrawTexture (healthLocation, healthBarImages[(int) healthAnimationIndex]);
		
		
        int lX = 25;
        int lY = 350;
        int i = 0; 
        GUI.Box(new Rect(lX - 15, lY - 40, 100, 150), "Stats");
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "health: " + player.getHealth());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "energy: " + player.getEnergy());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "score: " + Player.getScore ());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "combo: " + player.getCombo());
		GUI.Label (new Rect(lX, lY + i++ * 20, 100, 100), "shells: " + player.getShells());
		
		 
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
