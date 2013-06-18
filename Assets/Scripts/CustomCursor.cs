using UnityEngine;
using System.Collections;

public class CustomCursor : MonoBehaviour
{

	public Texture2D cursorImage;
	private Rect rect;
     
	void Start ()
	{
		Screen.showCursor = false;
		
		rect = new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y, 32, 32);
		
		//DontDestroyOnLoad (this);
	}
     
	void OnGUI ()
	{
		rect.x = Input.mousePosition.x;
		rect.y = Screen.height - Input.mousePosition.y;
		
		GUI.depth = -10;
		GUI.DrawTexture (rect, cursorImage);
	}
	
	void OnDestroy ()
	{
		//Screen.showCursor = true;
	}
}
