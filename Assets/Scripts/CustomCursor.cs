using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {

	public Texture2D cursorImage;
     
     
    void Start()
    {
      	Screen.showCursor = false;
		DontDestroyOnLoad(this);
    }
     
     
    void OnGUI()
    {
		GUI.depth = -10;
      GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 32, 32), cursorImage);
    }
	
	void OnDestroy()
	{
		//Screen.showCursor = true;
	}
}
