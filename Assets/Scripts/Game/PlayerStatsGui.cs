using UnityEngine;
using System.Collections;

public class PlayerStatsGui : MonoBehaviour
{

    //resource location
    private string healthBase = "GUI/health/Health";
    private string numberBase = "GUI/numbers/";

    //health bar variables
    public Texture2D healthBaseImage;
    Texture2D[] healthBarImages = new Texture2D[101];
    private Rect healthLocation = new Rect(Screen.width * 0.8f, 0, Screen.width * 0.2f, Screen.width * 0.2f);
    private float healthAnimationIndex = 100;

    //stat variables
    public Texture2D statBackboardTexture;
    public Texture2D comboStringTexture;
    private Rect backboardLocation;

    private Texture2D[] scoreNumbers = new Texture2D[10];
    private Texture2D[] shellNumbers = new Texture2D[10];

    private float xScale;
    private float yScale;

    private int scoreAni;

    float timeA;
    public int fps;
    public int lastFPS;


    //these variables are needed to know where to start drawing the score numbers from
    //the following numbers are the draw positions from topleft corner of number
    //note: these will be normalized in Start() because the textures aren't drawn at the same size they are saved as
    //height is the height in pixels we want the tallest thing to be drawn at. We can figure the width out from this
    //38 pixels in, 41 down, 0 pixel gap between letters
    private Vector2 scoreDrawPoint = new Vector2(35, 30);
    private float scoreHeight = 40;
    private float scoreGap = 0;
    //87 pixels in, 80 down, -4 pixel gap between letters (draw them closer)
    private Vector2 shellDrawPoint = new Vector2(75, 75);
    private float shellHeight = 25;
    private float shellGap = -4;
    //188 pixels in, 118 down, 0 pixel gap between letters
    private Vector2 comboDrawPoint = new Vector2(180, 115);
    private float comboHeight = 33;
    private float comboGap = 0;

    //this rect can be used during drawing so a new one doesn't need to constantly be created
    private Rect workRect = new Rect(0, 0, 0, 0);

    //used as a slight offset between numbers to make them appear nicer
    private float numberOffset = 10.0f;

    //the player
    private Player player;


    // Use this for initialization
    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<Player>();

        float width = Screen.width * 0.3f;
        backboardLocation = new Rect(0, 0, statBackboardTexture.width, statBackboardTexture.height);

        for (int i = 0; i <= 100; i++)
        {
            string imageIndex = (i < 10) ? "0" + i : i.ToString();
            healthBarImages[i] = Resources.Load(healthBase + imageIndex) as Texture2D;
        }

        for (int i = 0; i < 10; i++)
        {
            scoreNumbers[i] = Resources.Load(numberBase + i) as Texture2D;
            shellNumbers[i] = Resources.Load(numberBase + "snail" + i) as Texture2D;
        }

        //now normalize the string drawing positions
        scoreDrawPoint.Set((scoreDrawPoint.x / 279) * backboardLocation.width, (scoreDrawPoint.y / 189) * backboardLocation.height);
        shellDrawPoint.Set((shellDrawPoint.x / 279) * backboardLocation.width, (shellDrawPoint.y / 189) * backboardLocation.height);
        comboDrawPoint.Set((comboDrawPoint.x / 279) * backboardLocation.width, (comboDrawPoint.y / 189) * backboardLocation.height);

        xScale = 279.0f / backboardLocation.width;
        yScale = 189.0f / backboardLocation.height;

        timeA = Time.timeSinceLevelLoad;
        //DontDestroyOnLoad (this);
    }

    void Update()
    {
        //Debug.Log(Time.timeSinceLevelLoad+" "+timeA);
        if (Time.timeSinceLevelLoad - timeA <= 1)
        {
            fps++;
        }
        else
        {
            lastFPS = fps + 1;
            timeA = Time.timeSinceLevelLoad;
            fps = 0;
        }
    }

    //draws health/ energy/ score
    void OnGUI()
    {
        int wi = Screen.width;
        int hi = Screen.height;

        // -- draw health texture
        GUI.Label(new Rect(250, 5, 30, 30), "" + lastFPS);
        //get health, then adjust the animation index
        int health = Mathf.Clamp(player.getHealth(), 0, 100);

        if (health < healthAnimationIndex) healthAnimationIndex -= 0.5f;
        else if (health > healthAnimationIndex) healthAnimationIndex += 0.5f;

        //draw the actual image
        GUI.DrawTexture(healthLocation, healthBaseImage);
        GUI.DrawTexture(healthLocation, healthBarImages[(int)healthAnimationIndex]);


        // -- draw score/ combo/ shell texture 
        GUI.DrawTexture(backboardLocation, statBackboardTexture);
        GUI.DrawTexture(backboardLocation, comboStringTexture);


        if (scoreAni < Player.getScore()) scoreAni++;
        else if (scoreAni > Player.getScore()) scoreAni--;

        string score = scoreAni.ToString();
        string shells = player.getShells().ToString();
        string combo = player.getCombo().ToString();

        drawString(score, scoreNumbers, scoreDrawPoint, scoreHeight, scoreGap);
        drawString(shells, shellNumbers, shellDrawPoint, shellHeight, shellGap);
        drawString(combo, scoreNumbers, comboDrawPoint, comboHeight, comboGap);


        int lX = 25;
        int lY = 350;
        int i = 0;
        /*
        GUI.Box(new Rect(lX - 15, lY - 40, 100, 150), "Stats");
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "health: " + player.getHealth());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "energy: " + player.getEnergy());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "score: " + Player.getScore ());
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "combo: " + player.getCombo());
        GUI.Label (new Rect(lX, lY + i++ * 20, 100, 100), "shells: " + player.getShells());
         */

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

    private void drawString(string str, Texture2D[] numTexs, Vector2 origin, float neededHeight, float horozontalGap)
    {

        float width = 0;

        float yS;
        float xS;

        for (int j = 0; j < str.Length; j++)
        {
            int digit = str[j] - '0';

            Texture2D tex = numTexs[digit];

            yS = (tex.height * yScale) / scoreHeight;
            xS = yS / xScale;

            workRect.x = origin.x + width + horozontalGap;
            workRect.y = origin.y;
            workRect.width = tex.width;
            workRect.height = tex.height;
            GUI.DrawTexture(workRect, tex);

            width += tex.width + horozontalGap;
        }

    }
}