using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour {

	public Texture2D cursorImage;
     
     
    void Start()
    {
      	Screen.showCursor = false;
    }
     
     
    void OnGUI()
    {
      GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 32, 32), cursorImage);
    }
	
	void OnDestroy()
	{
		Screen.showCursor = true;
	}
}
