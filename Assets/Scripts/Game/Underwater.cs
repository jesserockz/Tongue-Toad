using UnityEngine;
using System.Collections;
 
public class Underwater : MonoBehaviour {
 
	//This script enables underwater effects. Attach to main camera.
 
    //Define variable
    public int underwaterLevel = 0;
 
    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material defaultSkybox;
    private Material noSkybox;
	public  BlurEffect blur;
 
    void Start () {
	    //Set the background color   
	    //camera.backgroundColor = new Color(0, 0.4f, 0.7f, 1);
		//defaultFog = RenderSettings.fog;
		//defaultFogColor = RenderSettings.fogColor;
		//defaultFogDensity = RenderSettings.fogDensity;
		//defaultSkybox = RenderSettings.skybox;
		blur.enabled = false;
    }
 
    void Update () {
        if (transform.position.y < underwaterLevel)
        {
            //RenderSettings.fog = true;
            //RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
            //RenderSettings.fogDensity = 0.09f;
            //RenderSettings.skybox = noSkybox;
			blur.enabled = true;
        }
        else
        {
            //RenderSettings.fog = defaultFog;
            //RenderSettings.fogColor = defaultFogColor;
            //RenderSettings.fogDensity = defaultFogDensity;
            //RenderSettings.skybox = defaultSkybox;
			blur.enabled =  false;
        }
    }
}