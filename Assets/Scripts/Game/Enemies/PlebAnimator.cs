using UnityEngine;
using System.Collections;

/// <summary>
/// Class handles animations for the snails.
/// </summary>
public class PlebAnimator : MonoBehaviour {
	
	private Enemy enemy;
	
	private AnimationState idle, death;
    private string idleAnim = "";
    private string deathAnim = "";
	private string[] idleString = {"Idle1","Idle2","Idle3"};
	private string[] deathString = {"Death1","Death2","Death3"};
	
	bool playedDeath = false;
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Enemy>();
        idleAnim = idleString[Random.Range(0, idleString.Length)];
        deathAnim = deathString[Random.Range(0, deathString.Length)];

		idle = animation[idleAnim];
		death = animation[deathAnim];
		
		death.wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy.getState() == Enemy.EnemyState.IDLE)
		{
			animation.Play (idleAnim);
		} else if (enemy.getState () == Enemy.EnemyState.DYING && !playedDeath)
		{
            animation.CrossFade(deathAnim);	
			playedDeath = true;
		}
	}
	
	
}
