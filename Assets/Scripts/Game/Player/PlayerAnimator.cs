using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	
	Player player;
	Tongue tongue;
	
	float deathTime = float.MaxValue;
    bool dying = false;
    bool shooting = false;
	
	
	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();

        animation["ShootStart"].wrapMode = WrapMode.Once;
        animation["ShootEnd"].wrapMode = WrapMode.Once;
        animation["Death"].wrapMode = WrapMode.Once;
        animation["ShootEnd"].speed = 4f;
		animation["ShootStart"].speed = 4f;
	}
	
	// Update is called once per frame
    void Update()
    {

        if (!dying && !shooting)
        {
            float x = Input.GetAxis("Horizontal");
            if (x == 0) animation.CrossFadeQueued("Idle1");
            else if (x == 1) animation.CrossFadeQueued("RightHold");
            else if (x == -1) animation.CrossFadeQueued("LeftHold");
            else if (x < 0 && x > -1 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) animation.CrossFade("LeftStart");
            else if (x < 0 && x > -1) animation.CrossFade("LeftEnd");
            else if (x > 0 && x < 1 && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) animation.CrossFade("RightStart");
            else if (x > 0 && x < 1) animation.CrossFade("RightEnd");

        }
        if (Player.State == PlayerState.Dying && !dying)
        {
            animation.CrossFade("Death");
            deathTime = Time.time + 6.05f;
            //Debug.Log("Dying at: " + deathTime);
            dying = true;
        }
        else if (dying)
        {
            //Debug.Log("Time till death: " + (deathTime - Time.time));
            if (deathTime <= Time.time)
            {
                //Debug.Log("Dead");
                Player.State = PlayerState.Dead;
            }
        }
        else if (Player.State == PlayerState.TongueStarting && !shooting)
        {
            animation.CrossFade("ShootStart");
            shooting = true;
        }
        else if (Player.State == PlayerState.TongueStarting && animation["ShootStart"].time>=0.3f)
        {
            Player.State = PlayerState.TongueOut;
            animation.CrossFade("ShootHold");
            shooting = true;
        }
        else if (Player.State == PlayerState.TongueEnding && !animation.isPlaying)
        {
            animation.Play("Idle1");
            Player.State = PlayerState.TongueIn;
            shooting = false;
        }
        else if (Player.State == PlayerState.TongueEnding)
        {
            if (Player.LastState == PlayerState.TongueStarting)
            {
                animation["ShootEnd"].time = 0.5f * animation["ShootEnd"].length;
                Debug.Log("Give up time " + animation["ShootEnd"].time);
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

