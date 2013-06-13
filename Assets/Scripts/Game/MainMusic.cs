using UnityEngine;
using System.Collections;

public class MainMusic : MonoBehaviour
{
	
	public AudioClip introNormal, chorusNormal;
	public AudioClip[] versesNormal;
	public AudioClip introTrip, chorusTrip;
	public AudioClip[] versesTrip;
	private AudioSource normalSource, tripSource;
	private SongPart songPart;
	private Player player;
	
	// Use this for initialization
	void Start ()
	{
		this.transform.position = Camera.mainCamera.transform.position;
		
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		
		AudioSource[] sources = GetComponents<AudioSource> ();
		
		normalSource = sources [0];
		tripSource = sources [1];
		
		normalSource.clip = introNormal;
		normalSource.volume = 1;
		
		
		tripSource.clip = introTrip;
		tripSource.volume = 0;
		
		songPart = SongPart.intro;
		Debug.Log (normalSource.clip.length);
		normalSource.Play ();
		//normalSource.time = 30;
		tripSource.Play ();
	}
	
	void Update ()
	{
		if (normalSource.time > getTime (songPart)) {
			AudioClip normal = null, trip = null;
			//finished that part, go to next
			switch (songPart) {
			case SongPart.intro:
			case SongPart.verse:
				songPart = SongPart.chorus;
				normal = chorusNormal;
				trip = chorusTrip;
				break;
			case SongPart.chorus:
				songPart = SongPart.verse;
				normal = versesNormal [Random.Range (0, versesNormal.Length)];
				trip = versesTrip [Random.Range (0, versesTrip.Length)];
				break;
			}
			
			normalSource.clip = normal;
			tripSource.clip = trip;
			
			normalSource.Play ();
			tripSource.Play ();
		}

		
		bool tripping = player.isTripping ();
		
		float mult = tripping ? -1 : 1;
		float val = 0.01f;
		
		normalSource.volume = Mathf.Clamp (normalSource.volume + mult * val, 0.0f, 1.0f);
		tripSource.volume = Mathf.Clamp (tripSource.volume + -mult * val, 0.0f, 1.0f);
	}
	
	private SongPart next (SongPart song)
	{
		switch (song) {
		case SongPart.intro:
			return SongPart.chorus;
		case SongPart.chorus:
			return SongPart.verse;
		case SongPart.verse:
		default:
			return SongPart.chorus;
		}
		
		//shouldn't reach here
	}
	
	private float getTime (SongPart part)
	{
		//all hard coded in. These are the actual times of songs. 
		//Unity doesn't read them properly, giving a longer time, resulting in noticeable silence between tracks
		switch (part) {
		case SongPart.intro:
			return 35.95f;
		case SongPart.chorus:
			return 11.95f;
		case SongPart.verse:
		default:
			return 23.95f;
		}
	}
	
	enum SongPart
	{
		intro,
		chorus,
		verse,
	}
}
