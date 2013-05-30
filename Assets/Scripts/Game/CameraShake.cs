using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	
	float shake_decay = 0f;
	float shake_intensity = 0f;
	public bool ending = false;
	
	Vector3 originPosition;
	Quaternion originRotation;
	
	// Use this for initialization
	void Start () {
		originPosition = transform.position;
		originRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.rotation = new Quaternion(
			originRotation.x + Random.Range(-shake_intensity,shake_intensity)*0.02f,
			originRotation.y + Random.Range(-shake_intensity,shake_intensity)*0.02f,
			originRotation.z + Random.Range(-shake_intensity,shake_intensity)*0.02f,
			originRotation.w + Random.Range(-shake_intensity,shake_intensity)*0.02f);
			shake_intensity -= shake_decay;
			
		}
		
	}
	
	public void Shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;
		shake_intensity = 0.01f;
		shake_decay = 0.002f;
	}
}
