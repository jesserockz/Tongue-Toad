using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
	
	public Texture2D background;
	public Texture2D resumeTex, exitTex;
	
	public GUISkin skin;
	
	
	private bool startedBoss = false;
	
	
    void Start()
    {
        //Screen.showCursor = false;
        //Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown (KeyCode.Escape))
        {
            setPause(!isPaused);
        }
    }

    void OnGUI()
    {
        if (isPaused)
        {
			GUI.skin = skin;
			
            float w = Screen.width;
            float h = Screen.height;
            float x = Screen.width / 2.0f;
            float y = Screen.height / 2.0f;
			
			float bgX = (w - background.width) / 2;
			float bgY = (h - background.height) / 2;
			
            //GUI.Box(new Rect(w , y, w, h), "Paused!");
			GUI.DrawTexture (new Rect( bgX, bgY, background.width, background.height), background);
			
			if (GUI.Button(new Rect((w - resumeTex.width) / 2, h  / 2 - 15, resumeTex.width, resumeTex.height), resumeTex)) setPause (false);
			if (GUI.Button(new Rect(x - 30, h / 2 - 35, 60, 20), (startedBoss ? "Be prepared.." : "BOSS!"))) startBoss();
            if (GUI.Button(new Rect((w - exitTex.width) / 2, h / 2 + 20, exitTex.width, exitTex.height), exitTex)) gotoMenu();
        }
    }
	
	private void startBoss()
	{
		if (!startedBoss) {
			startedBoss = true;
			GameObject.Find ("EnemySpawn").GetComponent<EnemySpawn>().gotoBoss();
		}
	}
	
	private void gotoMenu() 
	{
		setPause (false);
		Application.LoadLevel("Menu");
	}
	
    //use this function to pause/ unpause the game because it sets variables
    public static void setPause(bool pause)
    {
        isPaused = pause;
		
		//Screen.lockCursor = !pause;
		//Screen.showCursor = pause;
		
        Time.timeScale = isPaused ? 0 : 1.0f;
    }
}