using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all enemies. Specific types of enemies should extend this class and add their own functionaliy
/// </summary>
public class Enemy : MonoBehaviour {
	
	public string DEATH_SOUND = "death";
	
	private EnemyState state;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case EnemyState.DYING: break;
		case EnemyState.DEAD: break;
		case EnemyState.FLOATING:break;
		}
	}
			
			
			
	enum EnemyState {
		DYING,
		DEAD,
		FLOATING
	};
}
