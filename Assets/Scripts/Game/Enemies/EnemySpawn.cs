using UnityEngine;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
	//public GameObject enemy;
	public GameObject friendly;
	public GameObject craftCarrier;
	
	//public GameObject snail;
	public GameObject[] plebSnail;
	public GameObject[] copterSnail;
	public GameObject[] planeSnail;
	public GameObject[] tetsudoSnail;
	public GameObject[] lineSpawnPoint;
	
	//formations contains exact wave formations up to wave 10
	private List<List<System.Action>> formations;
	private List<System.Action> currentFormation;
	
	private Dictionary<int, System.Action> informationDisplays;
	
	private EnemyInfoPanels enemyInfoPanels;
	
	private GameObject currentBoss;
	
	private int currentWave;
	private float between = 2.0f;
	private float lastSpawn = 0;
	private bool boss = false;
	
	private int spawnCount = 20;
	private Quaternion spawnAngle = Quaternion.Euler (0, 180, 0);
	
	void Start ()
	{
		//cache enemy info panel
		GameObject gui = GameObject.FindGameObjectWithTag("Gui");
		enemyInfoPanels = gui.GetComponent<EnemyInfoPanels>();
		
		// Create the waves
		
		currentFormation = new List<System.Action> ();
		
		formations = new List<List<System.Action>> ();
		
		for (int i = 0; i < 10; i++) {
			formations.Add (new List<System.Action> ());
		}
		
		//wave 1 – 10 plebs
		addObject (formations [0], 10, spawnPleb);
		
		//wave 2 - 8 plebs, 5 testudo
		addObject (formations [1], 8, spawnPleb);
		addObject (formations [1], 5, spawnTetsudo);
		
		//wave 3 - 10 plebs, 4 toads
		addObject (formations [2], 10, spawnPleb);
		addObject (formations [2], 4, spawnGood);
		
		//wave 4 - 8 plebs, 5 helicopter, 3 toads
		addObject (formations [3], 8, spawnPleb);
		addObject (formations [3], 5, spawnCopter);
		addObject (formations [3], 3, spawnGood);
		
		//wave 5 - 8 plebs, 2 testudo, 2 helicopter, 3 toads
		addObject (formations [4], 8, spawnPleb);
		addObject (formations [4], 2, spawnTetsudo);
		addObject (formations [4], 2, spawnCopter);
		addObject (formations [4], 3, spawnGood);
		
		//wave 6 - 10 plebs, 5 planes, 3 toads
		addObject (formations [5], 10, spawnPleb);
		addObject (formations [5], 5, spawnPlane);
		addObject (formations [5], 3, spawnGood);
		
		//wave 7 - 10 plebs, 2 testudo, 3 helicopter, 2 planes, 4 toads
		addObject (formations [6], 10, spawnPleb);
		addObject (formations [6], 2, spawnTetsudo);
		addObject (formations [6], 3, spawnCopter);
		addObject (formations [6], 2, spawnPlane);
		addObject (formations [6], 4, spawnGood);
		
		//wave 8 - 8 plebs, 10 testudo, 5 toads
		addObject (formations [7], 8, spawnPleb);
		addObject (formations [7], 10, spawnTetsudo);
		addObject (formations [7], 5, spawnGood);
		
		//wave 9 - 10 plebs, 4 helicopter, 4 planes, 4 testudo, 4 toads
		addObject (formations [8], 10, spawnPleb);
		addObject (formations [8], 4, spawnCopter);
		addObject (formations [8], 4, spawnPlane);
		addObject (formations [8], 4, spawnTetsudo);
		addObject (formations [8], 4, spawnGood);
		
		//wave 10 - SnailCraftCarrier (15 planes)
		addObject (formations [9], 1, spawnCraftCarrier);
		
		//set our current formation to the first formation
		currentFormation = formations [0];
		
		initEnemyInfoPanels();
	}
	
	private void initEnemyInfoPanels() {
		Dictionary<System.Action, System.Action> displayMethods = new Dictionary<System.Action, System.Action>();
		
		//add methods for each monster type
		displayMethods.Add (spawnPleb, enemyInfoPanels.displayPleb);
		displayMethods.Add (spawnTetsudo, enemyInfoPanels.displayTestudo);
		displayMethods.Add (spawnCopter, enemyInfoPanels.displayHelicopter);
		displayMethods.Add (spawnPlane, enemyInfoPanels.displayPlane);
		displayMethods.Add (spawnCraftCarrier, enemyInfoPanels.displayAircraftCarrier);
		displayMethods.Add (spawnGood, enemyInfoPanels.displayToad);
		
		//now go over all the different monsters and set up when our info panels display
		List<System.Action> used = new List<System.Action>();
		informationDisplays = new Dictionary<int, System.Action>();
		
		for (int i = 0; i < formations.Count; i++) {
			foreach (System.Action a in formations[i]) {
				//already added this, so skip
				if (used.Contains(a)) continue;
				
				//otherwise add it
				informationDisplays.Add (i, displayMethods[a]);
				used.Add (a);
				
				//only 1 new type is every introduced per round
				break;
			}
		}
	}
	
	private void addObject (List<System.Action> addTo, int toAdd, System.Action method)
	{
		for (int i = 0; i < toAdd; i++) {
			addTo.Add (method);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		
		if (true && !boss) {
			//not enough time has elapsed to spawn a new monster. Return
			if (Time.time - lastSpawn <= between)
				return;
			
			//hack cause no other easy way to do it
		if ( currentWave == 0 && informationDisplays.ContainsKey(0)) {
			System.Action a = informationDisplays[0];
			a.Invoke();
			
			informationDisplays.Remove(0);
		}
			
			//get a random spawn method from list...
			int i = Random.Range (0, currentFormation.Count);
			System.Action action = currentFormation [i];
			
			//spawn them, then delete it from list
			action.Invoke ();
			
			currentFormation.RemoveAt (i);
			
			//if we've exhausted that list, call to populate new wave
			if (currentFormation.Count == 0)
				populateNextSpawnWave ();
			
			//set last spawn time
			lastSpawn = Time.time;
			
			return;
		}
	}
	
	public void resumeSpawning ()
	{
		boss = false;
		Destroy (currentBoss);
		spawnCount = 20;
	}
	
	private void populateNextSpawnWave ()
	{
		currentWave++;
		
		if (currentWave < formations.Count) {
			//spawning from predetermined lists...
				
			currentFormation = formations [currentWave];
			
			//check if we need to display an info panel
			if (informationDisplays.ContainsKey(currentWave)) {
				Debug.Log ("should be displaying...");
				informationDisplays[currentWave].Invoke();
			}
				
		} else {
			if (currentWave + 1 % 5 == 0) {
				//spawn boss
				addObject (currentFormation, 1, spawnCraftCarrier);
			} else {
				//spawning based on pattern
				//y = x - 9	(zero based index)
				//Wave x – (6 + y) plebs, (1 + y) helicopter, (2 + y) testudo, (2 + y) planes
				int y = currentWave - 9;
			
				addObject (currentFormation, 6 + y, spawnPleb);
				addObject (currentFormation, 1 + y, spawnCopter);
				addObject (currentFormation, 2 + y, spawnTetsudo);
				addObject (currentFormation, 2 + y, spawnPlane);
			}
		}
		
		Debug.Log ("wave:" + currentWave);
	}
	
	private void spawnCraftCarrier ()
	{
		boss = true;
		Vector3 pos = new Vector3 (1f, -1f, 41f);
		currentBoss = (GameObject)Instantiate (craftCarrier, pos, Quaternion.Euler (0, 180, 0));
	}
	
	private void spawnGood ()
	{ 
		Vector3 middle = getLinePosition ();
		middle.y = -0.35f;
		
		GameObject o = (GameObject)Instantiate (friendly, middle, Quaternion.Euler (0, -180, 0));
		//quick hack to make it loop animation
		o.GetComponent<Animation> () ["friendly toad animation"].wrapMode = WrapMode.Loop;
	}
	
	private void spawnPleb ()
	{
		GameObject snail = getRandomPleb ();
		Vector3 point = getLinePosition ();
		
		Instantiate (snail, point, Quaternion.Euler (0, -180, 0));
	}
	
	private void spawnCopter ()
	{
		GameObject copter = getRandomCopter ();
		Vector3 point = getLinePosition ();
		
		Instantiate (copter, point, Quaternion.Euler (0, -180, 0));
		
	}

	private void spawnPlane ()
	{
		GameObject plane = getRandomPlane ();
		Vector3 point = getLinePosition ();
		point.y = 0.6f;

		Instantiate (plane, point, Quaternion.Euler (0, -180, 0));

	}

	private void spawnTetsudo ()
	{
		GameObject tetsudo = getRandomTetsudo ();
		Vector3 point = getLinePosition ();
		point.y = 0.03f;

		Instantiate (tetsudo, point, Quaternion.Euler (0, -180, 0));

	}
	
	private GameObject getRandomPleb ()
	{
		return plebSnail [Random.Range (0, plebSnail.Length)];
	}
	
	private GameObject getRandomCopter ()
	{
		return copterSnail [Random.Range (0, copterSnail.Length)];
	}

	private GameObject getRandomPlane ()
	{
		return planeSnail [Random.Range (0, planeSnail.Length)];
	}

	private GameObject getRandomTetsudo ()
	{
		return tetsudoSnail [Random.Range (0, tetsudoSnail.Length)];
	}
	
	//returns a random vector of one of the positions of a line formation spawn point 
	private Vector3 getLinePosition ()
	{
		return lineSpawnPoint [Random.Range (0, lineSpawnPoint.Length)].transform.position;
	}
}
