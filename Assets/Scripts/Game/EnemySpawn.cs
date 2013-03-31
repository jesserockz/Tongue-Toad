using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public GameObject Enemy;
	
	private float between = 2.0f;
	private float lastSpawn = 0;
	
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawn > between) {
			//Instantiate(enemyPrefab);
			Instantiate(Enemy, new Vector3(0,0,30f), Quaternion.identity);
			lastSpawn = Time.time;
		}
	}
}
