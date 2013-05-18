using UnityEngine;
using System.Collections;

//Script is added to an enemy to make it drop shells when the enemy dies
public class ShellDrop : MonoBehaviour {
	
	public GameObject[] shells;

	
	//Spawns the shells
	public void spawnShells(Vector3 position, int numShells) {
		
		for (int i = 0; i < numShells; i++) {
			//iterate over number, and fire some new shells around
			int val = Random.Range (0, shells.Length);
			GameObject shell = (GameObject) Instantiate(shells[val], position, Quaternion.identity);
		}
	}
}
