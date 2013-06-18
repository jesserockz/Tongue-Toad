using UnityEngine;
using System.Collections;

/// <summary>
/// Class handles animations for the flying snails.
/// </summary>
public class CopterAnimator : MonoBehaviour {

	private Enemy enemy;
	
	private AnimationState idle, death;
    private string idleAnim = "";
	private string[] idleString = {"FlyingIdleNew","flyingIdle2"};
	private string deathString = "Death";
	
	bool playedDeath = false;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Enemy>();
        idleAnim = idleString[Random.Range(0,idleString.Length)];
		death = animation[deathString];
		//idle.time = Random.Range(0.0f, idle.length);

		death.wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy.getState() == Enemy.EnemyState.IDLE && !animation.isPlaying)
		{
            animation.CrossFade(idleAnim);
		} 
        else if (enemy.getState () == Enemy.EnemyState.DYING && !playedDeath)
		{
            animation.CrossFade(deathString);	
			playedDeath = true;
		}
	}
		
}
