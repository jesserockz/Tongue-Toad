using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//used for creating bullet
	public Rigidbody Bullet;
	
	//movement variables
	public float speed = 0.15f;
	public float maxMovement = 5.5f;
	
	//user statistics
	public int maxHealth;
	public static int currentHealth;
	
	public int maxEnergy;
	public static int currentEnergy;
	
	public static int score;
	
	public static int combo;
	
	bool l, r;
	
	Vector3 accel;
	Vector3 aCalib;
	
	CharacterController controller;
	
	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		currentEnergy = maxEnergy;
		aCalib = Input.acceleration;
		score = 0;
		combo = 0;
		controller = GetComponent<CharacterController>();
		Pause.setPause(false);
	}
	
	void OnCollisionEnter (Collision c) {
		string tag = c.gameObject.tag;
		if (tag == "Enemy") {
			currentHealth = Mathf.Max (currentHealth - 5, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//l = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
		//r = Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D);
		bool shoot = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
	
		#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR || UNITY_WEBPLAYER
		accel = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
		#elif UNITY_ANDROID || UNITY_IPHONE
		Vector3 v = Input.acceleration;
		accel = new Vector3(Mathf.Clamp((v.x-aCalib.x)*2,-1f,1f),Mathf.Clamp((v.z-aCalib.z)*2,-1f,1f),Mathf.Clamp((v.z-aCalib.z)*2,-1f,1f));
		#endif
		
		if (shoot && canShoot()) {
			//subtract energy from bullet shot
			currentEnergy = Mathf.Max(currentEnergy - 5, 0);
			Instantiate(Bullet,transform.position+new Vector3(0,0.2f,1f),Quaternion.identity);
		}
	}
	
	void FixedUpdate(){
		//evaluates l and r as if they were directional and then adds them 
		//int dir = (l ? -1 : 0) + (r ? 1 : 0);
		//transform.position += new Vector3(dir * speed,0,0);
		controller.Move(new Vector3(accel.x*speed*Time.deltaTime,0f,0f));
		transform.position = new Vector3(transform.position.x,0f,0f);
	}
	
	/// <summary>
	/// Returns true if the player can currently shoot (e.g cannot shoot if gun/ tongue is timing out)
	/// </summary>
	private bool canShoot() {
		return currentEnergy > 0 && !Pause.isPaused;
	}
	
	//damages the player (and maybe calls gameover or something like that)
	public void damage(int damage) {
		//nothing currently
	}
	
	//temp method atm just to have stats on screen
	//draws health/ energy/ score
	void OnGUI() {
		int lX = 25;
		int lY = 40;
		int i = 0;
		//GUI.Box(new Rect(0, 0, 50, 50), "Statistics");
		GUI.Box(new Rect(10,10,100,200), "Stats");
		GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "health: " + currentHealth);
		GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "energy: " + currentEnergy);
		GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "score: " + score);
		GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "combo: " + combo);
		GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "X: " + accel.x);
		GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "Y: " + accel.y);
		GUI.Label(new Rect(lX, lY + i++ * 20, 100, 100), "Z: " + accel.z);
		
		if (currentEnergy == 0) {
			float w = 220; 
			float h = 23;
			float x = (float) (Screen.width - w) / 2;
			float y = (float) (Screen.height * 0.75);
#if UNITY_IPHONE || UNITY_ANDROID
			GUI.Box(new Rect(x,y,w,h), "Shake to renew energy!");
#else
			GUI.Box(new Rect(x,y,w,h), "Hit Enter/ End to renew energy!");
#endif
		}
	}
}
