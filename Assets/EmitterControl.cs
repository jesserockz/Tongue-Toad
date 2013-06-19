using UnityEngine;
using System.Collections;

public class EmitterControl : MonoBehaviour {
	
	ParticleEmitter emit;
	ParticleSystem sys;
	
	public int[][] colours;// = new int[][]{{123,123,123},{234,234,234}};
	
	// Use this for initialization
	void Start () {
		//emit = GetComponent<ParticleEmitter>();
		sys = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		float ran = Random.Range(-8f,3f);
		pos = new Vector3(pos.x+ran,pos.y,pos.z);
		int r = Random.Range(50,180);
		int g = Random.Range(80,140);
		int b = Random.Range(50,160);
		float ranY = Random.Range(-0.5f,1.5f);
		sys.Emit(pos,new Vector3(0f,ranY,-10f),0.3f,10f,new Color32((byte)r,(byte)g,(byte)b,(byte)255));
	}
}
