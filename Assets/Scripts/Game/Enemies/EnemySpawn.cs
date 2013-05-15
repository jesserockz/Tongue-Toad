using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	//public GameObject enemy;
    public GameObject friendly;
    //public GameObject snail;
	public GameObject[] leafSnail;
    public GameObject[] flyingSnail;
	
	public GameObject[] lineSpawnPoint;
	
	private float between = 4.0f;
	private float lastSpawn = 0;
	
	private Quaternion spawnAngle = Quaternion.Euler(0, 180, 0);
	
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawn > between) {
			//Instantiate(enemyPrefab);
			
			int val = Random.Range (0, 100);
			
			
			//get a new between value
			between  = Random.Range (0.1f, 3.0f);
			
			if(val <= 90) {
				spawnGood();
			}
			else {
				int whatBad = Random.Range (0, 100);
				
				if (whatBad <= 75) spawnPleb();
				else spawnCopter();
			}
			
			
            lastSpawn = Time.time;
		}
	}
	
	private void spawnGood()
	{ 
		Vector3 middle = getLinePosition();
		middle.y = 0;
		
		GameObject o = (GameObject) Instantiate(friendly, middle, Quaternion.Euler(0, -180, 0));
		//quick hack to make it loop animation
		o.GetComponent<Animation>()["friendly toad animation"].wrapMode = WrapMode.Loop;
	}
	
	
	private void spawnPleb()
	{
		GameObject snail = getRandomSnail();
		Vector3 point = getLinePosition();
		
		Instantiate(snail, point, Quaternion.Euler(0, -180, 0));
	}
	
	private void spawnCopter()
	{
		GameObject copter = getRandomCopter();
		Vector3 point = getLinePosition();
		
		Instantiate(copter, point, Quaternion.Euler(0, -180, 0));
		
	}
	
	private void spawnRandom()
	{
		GameObject snail = getRandomSnail();
		Vector3 middle = getLinePosition();
		
		
		Instantiate(snail, middle, Quaternion.Euler(0, -180, 0));
	}
	
	//spawns a few monsters in an abreast formation
	private void spawnAbreast() 
	{
		Vector3 middle = this.transform.position;
		Vector3 offset = new Vector3(1.5f, 0, 0);
		GameObject snail = getRandomSnail();
		
		Instantiate(snail, middle, Quaternion.Euler(0, -180, 0));
		Instantiate(snail, middle - offset, Quaternion.Euler(0, -180, 0));
		Instantiate(snail, middle + offset, Quaternion.Euler(0, -180, 0));
	}
	
	//spawns a few monsters in a line formation
	private void spawnLine() 
	{
		Vector3 middle = getLinePosition();
		Vector3 offset = new Vector3(0, 0, 5);
		
		GameObject snail = getRandomSnail();
		
		Instantiate(snail, middle - offset, Quaternion.Euler(0, -180, 0));
		Instantiate(snail, middle, Quaternion.Euler(0, -180, 0));
		Instantiate(snail, middle + offset, Quaternion.Euler(0, -180, 0));
	}
	
	//spawns a few monsters in a star formation
	private void spawnStar() 
	{
		
	}
	
	//spawns many little monsters who scatter around
	private void spawnScatter()
	{
		int range = Random.Range (5, 15);
		for (int i = 0; i < range; i++)
		{
            GameObject snail = getRandomSnail();
			//float xOffset = Random.Range(0.0f, 5.0f);
			Instantiate(snail, transform.position, Quaternion.Euler(0, -180, 0));
		}
	}
	
	private void spawnDiagonal()
	{
		GameObject snail = getRandomSnail();
		
		//multipler will make the diagonal go from left to right, or right to left
		//with either be -1, 1, 
		int ran = Random.Range(0, 2);	//0 inclusive to 2 exclusive
		int orientation = (ran == 0) ? 1 : -1;
		
		for (int i = 0; i < lineSpawnPoint.Length; i++)
		{
			Instantiate(snail, lineSpawnPoint[i].transform.position + new Vector3(0, 0, orientation * (3 + i * -3)), Quaternion.Euler(0, -180, 0));
		}
		
	}
	
	
	private GameObject getRandomSnail()
	{
         return leafSnail[Random.Range(0, leafSnail.Length)];
	}
	
	private GameObject getRandomCopter()
	{
		return flyingSnail[Random.Range(0, flyingSnail.Length)];
	}
	
	//returns a random vector of one of the positions of a line formation spawn point 
	private Vector3 getLinePosition()
	{
		return lineSpawnPoint[Random.Range (0, lineSpawnPoint.Length)].transform.position;
	}
}
