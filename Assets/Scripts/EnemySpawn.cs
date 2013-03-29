using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public GameObject enemy;
	
	private float between = 2.0f;
	private float lastSpawn = 0;
	
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawn > between) {
			//Instantiate(enemyPrefab);
			Instantiate(enemy, new Vector3(0,0,15f), Quaternion.identity);
			lastSpawn = Time.time;
		}
	}
}
