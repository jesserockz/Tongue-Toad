using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	
	Player player;
	Tongue tongue;
	
	float deathTime = float.MaxValue;
    bool dying = false;
    bool shooting = false;

    string shootStartAnim = "";
	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();

        animation["ShootStart"].wrapMode = WrapMode.Once;
        animation["ShootStart2"].wrapMode = WrapMode.Once;
        animation["ShootStart3"].wrapMode = WrapMode.Once;
        animation["ShootEnd"].wrapMode = WrapMode.Once;
        animation["Death"].wrapMode = WrapMode.Once;
        animation["ShootEnd"].speed = 4f;
		animation["ShootStart"].speed = 4f;
        animation["ShootStart2"].speed = 4f;
        animation["ShootStart3"].speed = 4f;
	}
	
	// Update is called once per frame
    void Update()
    {
        animation["ShootEnd"].speed = 4f * TripMode.bonuses[TripMode.attackSpeed];
        animation["ShootStart"].speed = 4f * TripMode.bonuses[TripMode.attackSpeed];
        animation["ShootStart2"].speed = 4f * TripMode.bonuses[TripMode.attackSpeed];
        animation["ShootStart3"].speed = 4f * TripMode.bonuses[TripMode.attackSpeed];

        

        if (!dying && !shooting)
        {
            float x = Input.GetAxis("Horizontal");
            if (x == 0) animation.CrossFadeQueued("MainIdleNew");
            else if (x == 1) animation.CrossFadeQueued("RightHold");
            else if (x == -1) animation.CrossFadeQueued("LeftHold");
            else if (x < 0 && x > -1 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) animation.CrossFade("LeftStart");
            else if (x < 0 && x > -1) animation.CrossFade("LeftEnd");
            else if (x > 0 && x < 1 && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) animation.CrossFade("RightStart");
            else if (x > 0 && x < 1) animation.CrossFade("RightEnd");

        }
        if (Player.State == PlayerState.Dying && !dying)
        {
            int r = Random.Range(0, 2);
            if (r == 0)
                animation.CrossFade("Death");
            else if (r == 1)
                animation.CrossFade("Death2");
            deathTime = Time.time + 6.05f;
            dying = true;
        }
        else if (dying)
        {
            //Debug.Log("Time till death: " + (deathTime - Time.time));
            if (deathTime <= Time.time)
            {
                Player.State = PlayerState.Dead;
            }
        }
        else if (Player.State == PlayerState.TongueStarting && !shooting)
        {
            int r = Random.Range(0, 3);
            if (r == 0)
                shootStartAnim = "ShootStart";
            else if (r == 1)
                shootStartAnim = "ShootStart2";
            else if (r == 2)
                shootStartAnim = "ShootStart3";
            animation.CrossFade(shootStartAnim);
            shooting = true;
        }
        else if (Player.State == PlayerState.TongueStarting && animation[shootStartAnim].time >= 0.5f)
        {
            Player.State = PlayerState.TongueOut;
            //animation.CrossFadeQueued("ShootHold2");
            //shooting = true;
        }
        else if (Player.State == PlayerState.TongueOut && !animation.isPlaying)
        {
            animation.Play("ShootHold2");
            shooting = true;
        }
        else if (Player.State == PlayerState.TongueEnding && !animation.isPlaying)
        {
            //tongue just came in
            //stop any combos
            player.resetCombo();
            animation.CrossFade("MainIdleNew");
            Player.State = PlayerState.TongueIn;
            shooting = false;

        }
        else if (Player.State == PlayerState.TongueEnding)
        {
            if (Player.LastState == PlayerState.TongueStarting)
            {
                //animation["ShootEnd"].time = 0.5f * animation["ShootEnd"].length;
                Player.State = PlayerState.TongueEnding;
            }
            animation.CrossFade("ShootEnd");
            shooting = true;
        }
    }
			
	public enum PlayerState{
        TongueIn,
		TongueStarting,
		TongueOut,
		TongueEnding,
        Dying,
        Dead
	}
}

