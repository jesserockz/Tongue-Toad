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
			between  = Random.Range (4.0f, 8.0f);
			
			if(val <= 20) {
				spawnDiagonal();
                //Instantiate(friendly, new Vector3(25f,0.3f,60f), Quaternion.identity);
			}
			else if (val <= 50) {
                //Instantiate(enemy, new Vector3(25f, 0.3f, 55f), Quaternion.identity);
                //Instantiate(snail, new Vector3(25f, 0.2f, 60f), Quaternion.identity);
				spawnAbreast();
			} else if (val <= 70) {
				spawnLine ();
			} else {
				spawnDiagonal();
			}
            lastSpawn = Time.time;
			
		}
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
        int whichSnail = Random.Range(0, 3);
        if (whichSnail == 0)
        {
            return flyingSnail[Random.Range(0, flyingSnail.Length)];
        }
        else
        {
            return leafSnail[Random.Range(0, leafSnail.Length)];
        }
	}
	
	//returns a random vector of one of the positions of a line formation spawn point 
	private Vector3 getLinePosition()
	{
		return lineSpawnPoint[Random.Range (0, lineSpawnPoint.Length)].transform.position;
	}
}
