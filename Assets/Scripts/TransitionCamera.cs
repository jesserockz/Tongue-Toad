using UnityEngine;
using System.Collections;

public class TransitionCamera : MonoBehaviour
{
	public static bool isTransitioning = false;
	public static Vector3 originalCameraLocation;
	public static Quaternion originalCameraRotation;
	private float transitionDuration = 2.0f;
	private Vector3 mainLocation;
	private Quaternion mainRotation;
	
	// Use this for initialization
	void Start ()
	{
		mainLocation = Camera.mainCamera.transform.position;
		mainRotation = Camera.mainCamera.transform.rotation;

		StartCoroutine (Transition ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	IEnumerator Transition ()
	{
		//this will only be true if we have gone through the main menu
		if (isTransitioning) {
			//disable spawner
			EnemySpawn spawner = GameObject.Find ("EnemySpawn").GetComponent<EnemySpawn>();
			
			spawner.enabled = false;
			
			float t = 0.0f;
		
			while (t < 1.0f) {
				t += Time.deltaTime * (Time.timeScale / transitionDuration);
				//t += 0.01f;
 
				Camera.mainCamera.transform.position = Vector3.Lerp (originalCameraLocation, mainLocation, t);
				Camera.mainCamera.transform.rotation = Quaternion.Lerp (originalCameraRotation, mainRotation, t);

				yield return 0;
			}
			
			//reenable spawner after moving camera
			spawner.enabled = true;
		}
	}
}
