using UnityEngine;
using System.Collections;

public class FlyingSnailAnimator : MonoBehaviour {

	private Enemy enemy;
	
	private AnimationState idle, death;
	private string idleString = "FlyingSnailIdle";
	//private string deathString = "Death1";
	
	bool playedDeath = false;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Enemy>();
		
		idle = animation[idleString];
		//death = animation[deathString];
		idle.time = Random.Range(0.0f, idle.length);

		//death.wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (enemy.getState() == Enemy.EnemyState.IDLE)
		{
			animation.Play (idleString);
		} else if (enemy.getState () == Enemy.EnemyState.DYING && !playedDeath)
		{
			animation.Play (idleString);	
			playedDeath = true;
		}
	}
		
}
