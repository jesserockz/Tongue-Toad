using UnityEngine;
using System.Collections;

/// <summary>
/// Class handles animations for the snails.
/// </summary>
public class SnailAnimator : MonoBehaviour {
	
	private Enemy enemy;
	
	private AnimationState idle, death;
	private string idleString = "Idle1";
	private string deathString = "Death1";
	
	bool playedDeath = false;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Enemy>();
		
		idle = animation[idleString];
		death = animation[deathString];
		
		death.wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy.getState() == Enemy.EnemyState.IDLE)
		{
			animation.Play (idleString);
		} else if (enemy.getState () == Enemy.EnemyState.DYING && !playedDeath)
		{
			animation.Play (deathString);	
			playedDeath = true;
		}
	}
	
	
}
