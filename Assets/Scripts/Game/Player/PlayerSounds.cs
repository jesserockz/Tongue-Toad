using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour
{

	public AudioClip movement;
	public AudioClip tongueStart;
	public AudioClip tongueStretch;
	public AudioClip tongueComplete;
	
	public AudioSource movementtSource;
	public AudioSource tongueStartSource;
	public AudioSource tongueStretchSource;
	public AudioSource tongueCompleteSource;
	
	private GameObject player;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		movementtSource = AddAudio (movement);
		tongueStartSource = AddAudio (tongueStart);
		tongueStretchSource = AddAudio (tongueStretch);
		tongueCompleteSource = AddAudio (tongueComplete);
	}
	
	private AudioSource AddAudio (AudioClip clip)
	{
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip;
		newAudio.loop = false;
		newAudio.volume = 0.7f;
		return newAudio;
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool moving = Input.GetAxis("Horizontal") != 0;
		
		if (moving && !movementtSource.isPlaying)
		{
			movementtSource.Play();
		}
	}
}
