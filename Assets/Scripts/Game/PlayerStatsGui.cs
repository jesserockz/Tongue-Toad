using UnityEngine;
using System.Collections;

public class PlayerStatsGui : MonoBehaviour
{
	//resource location
	private string healthBase = "GUI/game/health/healthbar_";
	private string numberBase = "GUI/game/score numbers/";

	//health bar variables
	public Texture2D healthBaseImage;
	Texture2D[] healthBarImages = new Texture2D[101];
	private Rect healthLocation = new Rect (Screen.width * 0.8f, 0, Screen.width * 0.2f, Screen.width * 0.2f);
	private float healthAnimationIndex = 0;

	//stat variables
	public Texture2D statBackboardTexture;
	private Rect backboardLocation;
	
	//face textures
	public Texture2D[] rockyFaces;
	public Texture2D[] tripFaces;
	public Texture2D deadFace;
	
	//current face variables
	private Texture2D currentFace, previousFace;
	private float currentAlpha, previousAlpha;
	private float transitionTime = 3;
	private int lastHealth = 100;
	
	//gameover stuff
	public Texture2D gameoverTexture;
	private float gameoverAlpha = 0;
	private float gameoverSpeed = 4.0f;
	private float finalDislpayTime = 2.0f;
	private float accumulatedGameover = 0;
	
	//textures
	private Texture2D[] scoreNumbers = new Texture2D[10];
	private Texture2D[] shellNumbers = new Texture2D[10];
	private float xScale;
	private float yScale;
	private int scoreAni;

	//these variables are needed to know where to start drawing the score numbers from
	//the following numbers are the draw positions from topleft corner of number
	//note: these will be normalized in Start() because the textures aren't drawn at the same size they are saved as
	//height is the height in pixels we want the tallest thing to be drawn at. We can figure the width out from this
	//38 pixels in, 41 down, 0 pixel gap between letters
	private Vector2 scoreDrawPoint = new Vector2 (35, 30);
	private float scoreHeight = 40;
	private float scoreGap = 0;
	//87 pixels in, 80 down, -4 pixel gap between letters (draw them closer)
	private Vector2 shellDrawPoint = new Vector2 (75, 75);
	private float shellHeight = 25;
	private float shellGap = -4;
	//188 pixels in, 118 down, 0 pixel gap between letters
	private Vector2 comboDrawPoint = new Vector2 (180, 115);
	private float comboHeight = 33;
	private float comboGap = 0;

	//this rect can be used during drawing so a new one doesn't need to constantly be created
	private Rect workRect = new Rect (0, 0, 0, 0);

	//used as a slight offset between numbers to make them appear nicer
	private float numberOffset = 10.0f;

	//the player
	private Player player;
	
	

	// Use this for initialization
	void Start ()
	{
		GameObject p = GameObject.FindGameObjectWithTag ("Player");
		player = p.GetComponent<Player> ();

		float width = Screen.width * 0.3f;
		backboardLocation = new Rect (0, 0, statBackboardTexture.width, statBackboardTexture.height);

		for (int i = 0; i <= 100; i++) {
			//images are labeled backwards for some reason
			int index = 100 - i;
			healthBarImages [i] = Resources.Load (healthBase + index) as Texture2D;
		}

		for (int i = 0; i < 10; i++) {
			scoreNumbers [i] = Resources.Load (numberBase + i) as Texture2D;
			shellNumbers [i] = Resources.Load (numberBase + "snail" + i) as Texture2D;
		}

		//now normalize the string drawing positions
		scoreDrawPoint.Set ((scoreDrawPoint.x / 279) * backboardLocation.width, (scoreDrawPoint.y / 189) * backboardLocation.height);
		shellDrawPoint.Set ((shellDrawPoint.x / 279) * backboardLocation.width, (shellDrawPoint.y / 189) * backboardLocation.height);
		comboDrawPoint.Set ((comboDrawPoint.x / 279) * backboardLocation.width, (comboDrawPoint.y / 189) * backboardLocation.height);

		xScale = 279.0f / backboardLocation.width;
		yScale = 189.0f / backboardLocation.height;
		
		//face stuff
		currentFace = rockyFaces [rockyFaces.Length - 1];
		previousFace = currentFace;
		
		currentAlpha = 1.0f;
		previousAlpha = 0.0f;
	}
	
	private float current, shellTime = 0.1f;
	
	//draws health/ energy/ score
	void OnGUI ()
	{
		if (Input.GetKeyDown (KeyCode.Minus))
			player.addHealth (-10);
		
		if (player.deadOrDying ()) {
			//transition down the shells
			if (player.getShells () == 0) {
				//we've finished moving shells
				
				if (Input.GetKeyDown (KeyCode.Escape)) {
					Application.LoadLevel ("GameOver");
				}
				
				//transition the alpha
				gameoverAlpha += Time.deltaTime * (1.0f / gameoverSpeed);
				gameoverAlpha = Mathf.Clamp (gameoverAlpha, 0.0f, 1.0f);
				
				//check if we've finished death animation/ fading
				if (gameoverAlpha >= 0.9f) {
					
					if (player.dead ())
						accumulatedGameover += Time.deltaTime;
					
					if (accumulatedGameover >= finalDislpayTime) {
						Application.LoadLevel ("GameOver");
					}
				}
				//fade out main gui components, fade in "game over"
			} else {
				//transition shells to score
				
				current += Time.deltaTime;
				
				if (current >= shellTime) {
					player.transitionShell ();
					current = 0;
				}
				
			}
		}

		Color c = GUI.color;
		
		c.a = (1 - gameoverAlpha);
		GUI.color = c;
		
		int wi = Screen.width;
		int hi = Screen.height;

		// -- draw health texture
		//get health, then adjust the animation index
		int health = Mathf.Clamp (player.getHealth (), 0, 100);

		if (health < healthAnimationIndex)
			healthAnimationIndex -= 0.5f * Time.timeScale;
		else if (health > healthAnimationIndex)
			healthAnimationIndex += 0.5f * Time.timeScale;

		//draw the health backing, current bar, then rocky's face
		healthLocation = new Rect (Screen.width - healthBaseImage.width * 0.7f, 0, healthBaseImage.width * 0.7f, healthBaseImage.height * 0.7f);
		
		GUI.DrawTexture (healthLocation, healthBaseImage);
		GUI.DrawTexture (healthLocation, healthBarImages [(int)healthAnimationIndex]);
		
		//face stuff
		updateFaces ();
		drawFaces ();

		// -- draw score/ combo/ shell texture 
		GUI.DrawTexture (backboardLocation, statBackboardTexture);

		if (scoreAni < Player.getScore ())
			scoreAni++;
		else if (scoreAni > Player.getScore ())
			scoreAni--;

		string score = scoreAni.ToString ();
		string shells = player.getShells ().ToString ();
		string combo = player.getCombo ().ToString ();

		drawString (score, scoreNumbers, scoreDrawPoint, scoreHeight, scoreGap);
		drawString (shells, shellNumbers, shellDrawPoint, shellHeight, shellGap);
		drawString (combo, scoreNumbers, comboDrawPoint, comboHeight, comboGap);
		
		
		//draw the death stuff
		c.a = (gameoverAlpha);
		GUI.color = c;
		
		Rect r = new Rect ((Screen.width - gameoverTexture.width) / 2, (Screen.height - gameoverTexture.height) / 2, gameoverTexture.width, gameoverTexture.height);
		GUI.DrawTexture (r, gameoverTexture);
	}
	
	//checks if we need to change face. If we do, changes the current to that face, and the previous to the correct face
	//also updates the alpha values
	private void updateFaces ()
	{
		//update alpha values
		if (player.deadOrDying ()) {
			currentAlpha = gameoverAlpha;
			previousAlpha = gameoverAlpha;
		} else {
			currentAlpha += Time.deltaTime * (1.0f / transitionTime);
			previousAlpha -= Time.deltaTime * (1.0f / transitionTime);
		
			currentAlpha = Mathf.Clamp (currentAlpha, 0.0f, 1.0f);
			previousAlpha = Mathf.Clamp (previousAlpha, 0.0f, 1.0f);
		}
		
		
		
		//check if we need to change face
		
		//can't change once it's dead
		if (currentFace == deadFace)
			return;
		
		if (lastHealth <= 0) {
			previousFace = currentFace;
			currentFace = deadFace;
			
			previousAlpha = currentAlpha;
			currentAlpha = 0.0f;
			
			return;
		}
		
		int iCur = Mathf.Clamp (player.getHealth () * 3 / 100, 0, 2);
		int iLast = Mathf.Clamp (lastHealth * 3 / 100, 0, 2);
		
		if (iCur != iLast) {
			//we've transitioned
			previousFace = currentFace;
			currentFace = rockyFaces [iCur];
			
			previousAlpha = currentAlpha;
			currentAlpha = 0.0f;
		}
		
		//then cache health
		lastHealth = player.getHealth ();
	}
	
	//draws the faces at the correct transparency levels
	private void drawFaces ()
	{
		Color c;
		
		if (player.deadOrDying ()) {
			c = GUI.color;
		
			c.a = (1 - gameoverAlpha);
			GUI.color = c;
			GUI.DrawTexture (healthLocation, currentFace);
			return;
		}
		
		c = GUI.color;
		
		c.a = previousAlpha;
		GUI.color = c;
		GUI.DrawTexture (healthLocation, previousFace);
		
		c.a = currentAlpha;
		GUI.color = c;
		GUI.DrawTexture (healthLocation, currentFace);
		
		//be sure to reset gui's alpha back to 1 so we draw other things properly
		c.a = 1;
		GUI.color = c;
	}
	
	private void drawString (string str, Texture2D[] numTexs, Vector2 origin, float neededHeight, float horozontalGap)
	{

		float width = 0;

		float yS;
		float xS;

		for (int j = 0; j < str.Length; j++) {
			int digit = str [j] - '0';

			Texture2D tex = numTexs [digit];

			yS = (tex.height * yScale) / scoreHeight;
			xS = yS / xScale;

			workRect.x = origin.x + width + horozontalGap;
			workRect.y = origin.y;
			workRect.width = tex.width;
			workRect.height = tex.height;
			GUI.DrawTexture (workRect, tex);

			width += tex.width + horozontalGap;
		}

	}
}