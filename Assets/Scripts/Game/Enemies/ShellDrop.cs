using UnityEngine;
using System.Collections;

//Script is added to an enemy to make it drop shells when the enemy dies
public class ShellDrop : MonoBehaviour {
	
	public GameObject[] shells;
	public GameObject newShell;

	
	//Spawns the shells
	public void spawnShells(Vector3 position, int numShells) {
        numShells *= (int)TripMode.bonuses[TripMode.shellDropMultiplier];
		for (int i = 0; i < 1; i++) {
			//iterate over number, and fire some new shells around
			int val = Random.Range (0, shells.Length);
			position.y = -0.2f;
			GameObject shell = (GameObject) Instantiate(newShell, position, Quaternion.identity);
		}
	}
}
