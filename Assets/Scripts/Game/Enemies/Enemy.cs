using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all enemies. Specific types of enemies should extend this class and add their own functionaliy
/// </summary>
public class Enemy : MonoBehaviour {
	
	public string DEATH_SOUND = "death";
	private int health = 100;
	public int healthDamage = 5;
	
	private EnemyState state;
	private ShellDrop shellDrop;
	
	// Use this for initialization
	void Start () {
		shellDrop = GetComponent<ShellDrop>();
		state = EnemyState.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case EnemyState.DYING: 
			if (!animation.isPlaying) {
				//means the animation has looped once through, so kill the player now
				Destroy(gameObject);
			}
			break;
		case EnemyState.DEAD: break;
		case EnemyState.IDLE:break;
		}
		
		if(transform.position.z < -5f) {
			Player.currentHealth -= healthDamage;
			Player.combo = 0;
			Destroy(gameObject);
		}
	}
	
	public void attack(int damage)
	{
		//we can only attack an enemy if it isn't already dead
		if (state != EnemyState.IDLE) return;
		
		health -= damage;
		//if their health is 0 or below, they are now dying
		if (health <= 0) {
			//disable collider and forward movement so they stop and don't screw with other physics bodie
			disableCollider();
			disableMovement();
			//delay playing to wait for animation to hit water
			//NOTE: there should be a method PlayDelay() which works in seconds, but my editor can't find it
			//this current method works in like hertz or something, so I just guessed this number...
			GetComponent<AudioSource>().Play(25000);	
			state = EnemyState.DYING;
			
			//spawn some shells
			//ShellDrop.spawnShells(this.rigidbody.position, 5);
			GameObject spawner = GameObject.Find ("ShellShardSpawner");
			spawner.GetComponent<ShellDrop>().spawnShells(rigidbody.position, 5);
		}
	}
			
	public EnemyState getState() 
	{
		return state;
	}
	
	public enum EnemyState {
		DYING,
		DEAD,
		IDLE
	};
	
	private void disableMovement(){
		ForwardMovement fm = GetComponent<ForwardMovement>();
		if(fm!=null) fm.enabled = false;
		PlaneMovement pm = GetComponent<PlaneMovement>();
		if(pm!=null) {
			pm.enabled = false;
			Destroy (pm.gameObject);
		}
	}
	
	private void disableCollider() {
		//CapsuleCollider cc = GetComponent<CapsuleCollider>();
		Collider bx = GetComponent<Collider>();
		
		if (bx != null) bx.enabled = false;
	}
}
