using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public static float musicLevel = 1.0f;
	public static float effectsLevel = 1.0f;
	
	public static bool muteMusic = false;
	public static bool muteEffects = false;
	
	
	public static void setMusic(float level) {
		musicLevel = level;
		AudioListener.volume = muteMusic ? 0 : musicLevel;
	}
	
	public static void setEffects(float level) {
		effectsLevel = level;
		//need to figure out a way to tag audio so we can alter effects level
	}
	
	public static void toggleMusic() {
		muteMusic = !muteMusic;
		setMusic (musicLevel);
	}
	
	public static void toggleEffects() {
		muteEffects = !muteEffects;
		setEffects(effectsLevel);
	}
}
