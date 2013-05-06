using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public GameObject enemy;
    public GameObject friendly;
    public GameObject snail;
	
	private float between = 2.0f;
	private float lastSpawn = 0;
	
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawn > between) {
			//Instantiate(enemyPrefab);
			if(Random.Range(0,100)<=20)
                Instantiate(friendly, new Vector3(25f,0.3f,60f), Quaternion.identity);
			else
                //Instantiate(enemy, new Vector3(25f, 0.3f, 55f), Quaternion.identity);
                Instantiate(snail, new Vector3(25f, 0.2f, 60f), Quaternion.identity);
            lastSpawn = Time.time;
		}
	}
}
