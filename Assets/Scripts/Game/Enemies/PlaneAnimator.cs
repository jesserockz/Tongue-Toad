using UnityEngine;
using System.Collections;

/// <summary>
/// Class handles animations for the snails.
/// </summary>
public class PlaneAnimator : MonoBehaviour {
	
	private Enemy enemy;
	
	private AnimationState idle, death;
    private string idleAnim = "";
    private string deathAnim = "";
	private string[] idleString = {};
	private string[] deathString = {};
	
	bool playedDeath = false;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Enemy>();
        //idleAnim = idleString[Random.Range(0, idleString.Length)];
        //deathAnim = deathString[Random.Range(0, deathString.Length)];

		//idle = animation[idleAnim];
		//death = animation[deathAnim];
		
		//death.wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy.getState() == Enemy.EnemyState.IDLE)
		{
			//animation.Play (idleAnim);
		} else if (enemy.getState () == Enemy.EnemyState.DYING && !playedDeath)
		{
			//animation.Play (deathAnim);	
			playedDeath = true;
		}
	}
	
	
}
