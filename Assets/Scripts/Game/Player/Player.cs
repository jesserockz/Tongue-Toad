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
	public int shells;

    private static PlayerAnimator.PlayerState _state = PlayerAnimator.PlayerState.TongueIn;
    public static PlayerAnimator.PlayerState State
    {
        get { return _state; }
        set
        {
            LastState = _state;
            _state = value;
        }
    }
            
    public static PlayerAnimator.PlayerState LastState = PlayerAnimator.PlayerState.TongueIn;

	//currently we're just gonna 1 hit anything. If we want to add in multihits later, adjust this appropriately
	private int tongueDamage = 1000;

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

    Tongue tongue;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        aCalib = Input.acceleration;
        score = 0;
        combo = 0;
        controller = GetComponent<CharacterController>();
        //Pause.setPause(false);
        
        //cheat detect
        hasCheated = false;
        scoreCheck = cheatOffset;
        numTimesIncrementScore = 0;
        changingScore = false;
        tongue = GameObject.Find("TongueTip").GetComponent<Tongue>();
    }

    void OnTriggerEnter(Collider c)
    {
        string tag = c.gameObject.tag;
        if (tag == "Enemy")
        {
            //currentHealth = Mathf.Max(currentHealth - 10, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && State != PlayerAnimator.PlayerState.Dying && State != PlayerAnimator.PlayerState.Dead)
        {
            State = PlayerAnimator.PlayerState.Dying;
            GetComponent<MouseFollower>().enabled = false;
            tongue.enabled = false;
            controller.enabled = false;
            tongue.tongue.renderer.enabled = false;
        }
        if (currentHealth <= 0 && State == PlayerAnimator.PlayerState.Dead)
        {
            Debug.Log("GameOver");
            initiateGameOver();
        }
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

    //damages the player (and maybe calls gameover or something like that)
    public void damage(int damage)
    {
        //nothing currently
    }
	
		public void addShell() {
		shells++;
	}
	
	public void addShell(int amount) {
		shells += amount;
	}
	
	public int getTongueDamage()
	{
		return tongueDamage;
	}
	
	//Called when the player attacks an enemy. Only do stuff related to the player here, enemy stuff is handled by the enemy
	public void attackEnemy(Enemy enemy)
	{
		Player.addScore(10 + Player.combo);
        Player.currentEnergy += 5;
        Player.combo++;
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
	
    private void initiateGameOver()
    {
        Screen.lockCursor = false;
        Screen.showCursor = true;
        Application.LoadLevel("GameOver");
    }

    public static int getScore()
    {
        return score;
    }
	
	public int getHealth()
	{
		return currentHealth;
	}
	
	public int getEnergy()
	{
		return currentEnergy;	
	}
	
	public int getCombo()
	{
		return combo;
	}
	

	
	public int getShells()
	{
		return shells;
	}

}