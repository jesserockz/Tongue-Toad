using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{

    public static bool isPaused = false;

    void Start()
    {
        Screen.showCursor = false;
        Screen.lockCursor = true;
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
            float w = 200;
            float h = 100;
            float x = (Screen.width - w) / 2.0f; ;
            float y = (Screen.height - h) / 2.0f;

            GUI.Box(new Rect(x, y, w, h), "Paused!");
			
			if (GUI.Button(new Rect(x + 20, y + h - 60, w - 40, 20), "Unpause")) setPause (false);
            if (GUI.Button(new Rect(x + 20, y + h - 30, w - 40, 20), "Exit")) Application.LoadLevel("Menu");
        }
    }

    //use this function to pause/ unpause the game because it sets variables
    public static void setPause(bool pause)
    {
        isPaused = pause;
        if (pause)
        {
            Screen.showCursor = true;
            Screen.lockCursor = false;
        }
        else
        {
            Screen.showCursor = false;
            Screen.lockCursor = true;
        }
		
        Time.timeScale = isPaused ? 0 : 1.0f;
    }
}