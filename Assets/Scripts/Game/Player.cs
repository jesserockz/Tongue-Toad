using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //movement variables
    public float speed = 5f;
    public float maxMovement = 5.5f;

    //user statistics
    public int maxHealth;
    public static int currentHealth;

    public int maxEnergy;
    public static int currentEnergy;

    private static int score;

    public static int combo;

    //stuff to detect cheating
    //at start of game we set scoreCheck to cheatOffset
    //each point we get we multiply by cheatMultiplier then add to our check
    //each increment we also add the increment value
    //we recalculate and check each time we get score to see if the new calculated and running are the same
    private static bool changingScore = false;
    private static int cheatOffset = -12349;
    private static int cheatMultiplier = 453;
    private static int cheatIncrementValue = -47;
    private static int scoreCheck;
    private static int numTimesIncrementScore;
    //if true it means the game has detected the player has artificially changed their score during play
    public static bool hasCheated = false;

    bool l, r;


    Vector3 accel;
    Vector3 aCalib;

    CharacterController controller;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        aCalib = Input.acceleration;
        score = 0;
        combo = 0;
        controller = GetComponent<CharacterController>();
        Pause.setPause(false);

        //cheat detect
        hasCheated = false;
        scoreCheck = cheatOffset;
        numTimesIncrementScore = 0;
        changingScore = false;

        //tongue = ((GameObject)Instantiate(Bullet, frog.position, frog.rotation)).transform;
    }

    void OnTriggerEnter(Collider c)
    {
        string tag = c.gameObject.tag;
        if (tag == "Enemy")
        {
            currentHealth = Mathf.Max(currentHealth - 10, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) initiateGameOver();

        //currently not using this as it's not tested thoroughly and don't want to accuse users of cheating when they're not
        //detectCheating();

        

#if UNITY_IPHONE || UNITY_ANDROID
            //device with accelerometer
            Vector3 v = Input.acceleration;
            accel = new Vector3(Mathf.Clamp((v.x - aCalib.x) * 2, -1f, 1f), Mathf.Clamp((v.z - aCalib.z) * 2, -1f, 1f), Mathf.Clamp((v.z - aCalib.z) * 2, -1f, 1f));
#else
        accel = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
#endif

        
    }

    void FixedUpdate()
    {
        controller.Move(new Vector3(accel.x * speed * Time.deltaTime, 0f, 0f));
        transform.position = new Vector3(transform.position.x, 0f, 0f);
    }

    /// <summary>
    /// Returns true if the player can currently shoot (e.g cannot shoot if gun/ tongue is timing out)
    /// </summary>
    private bool canShoot()
    {
        return currentEnergy > 0 && !Pause.isPaused;
    }

    //damages the player (and maybe calls gameover or something like that)
    public void damage(int damage)
    {
        //nothing currently
    }

    //method adds to highscore, also incrementing our scorecheck value to ensure no cheating happens
    public static void addScore(int value)
    {
        changingScore = true;

        score += value;

        numTimesIncrementScore++;

        //add to our scorecheck
        scoreCheck += cheatIncrementValue;
        scoreCheck += cheatMultiplier * value;

        changingScore = false;
    }

    private static void detectCheating()
    {
        //some basic synchronization to ensure we don't cheat detect while changing score
        if (changingScore) return;

        //recalculate it, if it's different, they've cheated
        int check = cheatOffset + cheatMultiplier * score + numTimesIncrementScore * cheatIncrementValue;

        if (scoreCheck != check)
        {
            //if they are different the user cheated
            Debug.Log("Cheated");
            hasCheated = true;
            //reset score back to real value
            score = (scoreCheck - cheatOffset - numTimesIncrementScore * cheatIncrementValue) / cheatMultiplier;
        }
    }

    public static int getScore()
    {
        return score;
    }


    //temp method atm just to have stats on screen
    //draws health/ energy/ score
    void OnGUI()
    {
        int lX = 25;
        int lY = 40;
        int i = 0;
        //GUI.Box(new Rect(0, 0, 50, 50), "Statistics");
        GUI.Box(new Rect(10, 10, 100, 220), "Stats");
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "health: " + currentHealth);
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "energy: " + currentEnergy);
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "score: " + score);
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "combo: " + combo);
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "X: " + accel.x);
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "Y: " + accel.y);
        GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "Z: " + accel.z);
        //tongueExtensionTime = GUI.HorizontalSlider(new Rect(lX, lY + i++ * 20, 100, 100), tongueExtensionTime, 0, 10);

        //anticheat box
        if (Player.hasCheated) GUI.Box(new Rect(Screen.width - 150 - 10, lY, 150, 30), "Cheating was detected");

        if (currentEnergy == 0)
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

    private void initiateGameOver()
    {
        Screen.lockCursor = false;
        Screen.showCursor = true;
        Application.LoadLevel("GameOver");
    }
}