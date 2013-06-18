using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all enemies. Specific types of enemies should extend this class and add their own functionaliy
/// </summary>
public class Enemy : MonoBehaviour {
	
	public AudioClip[] deathSounds;
	private int health = 100;
	public int healthDamage = 5;
	
	//the number of points the player gets for killing this monster
	public int basePoints = 0;
	
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
		case EnemyState.IDLE: break;
		}
		
		if(transform.position.z < -5f && state == EnemyState.IDLE) {
			Player.currentHealth -= healthDamage;
			Player.streak = 0;
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
			killThis ();
			
			//spawn some shells
			//ShellDrop.spawnShells(this.rigidbody.position, 5);
			GameObject spawner = GameObject.Find ("ShellShardSpawner");
			spawner.GetComponent<ShellDrop>().spawnShells(rigidbody.position, 5);
		}
	}
	
	public void collideWithRocky()
	{
		if (state != EnemyState.IDLE) return;
		
		killThis ();
	}
	
	//kills the object, making it play a sound, but doesn't drop any items, etc.
	private void killThis()
	{
		//disable collider and forward movement so they stop and don't screw with other physics bodie
			disableCollider();
			disableMovement();
			playDeathSound();	
			state = EnemyState.DYING;
	}
			
	public EnemyState getState() 
	{
		return state;
	}
	
	public int getBasePoints()
	{
		return basePoints;
	}
	
	/// <summary>
	/// Returns the damage this monster does to rocky.
	/// </summary>
	/// <returns>
	/// The damage.
	/// </returns>
	public int getDamage()
	{
		return healthDamage;
	}
	
	public enum EnemyState {
		DYING,
		DEAD,
		IDLE
	};
	
	private void disableMovement(){
		ForwardMovement fm = GetComponent<ForwardMovement>();
		if(fm!=null) fm.enabled = false;
		//PlaneMovement pm = GetComponent<PlaneMovement>();
		//if(pm!=null) {
		//	pm.enabled = false;
			//Destroy (pm.gameObject);
		//}
	}
	
	private void disableCollider() {
		//CapsuleCollider cc = GetComponent<CapsuleCollider>();
		Collider bx = GetComponent<Collider>();
		
		if (bx != null) bx.enabled = false;
	}
	
	//Plays a random sound from the monsters death audio list
	//if they don't have one, simply returns
	private void playDeathSound()
	{
		//won't make it compulsary to have sound effects
		if (deathSounds == null || deathSounds.Length == 0) return;
		
		AudioClip c = deathSounds[Random.Range (0, deathSounds.Length)];
		AudioSource.PlayClipAtPoint(c, transform.position);
	}
}
