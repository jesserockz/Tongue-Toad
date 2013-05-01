using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEngine : MonoBehaviour {

    public Dictionary<string,AudioClip> effects = new Dictionary<string,AudioClip>();
    public Dictionary<string, AudioClip> music = new Dictionary<string, AudioClip>();
    List<AudioSource> playingAudio = new List<AudioSource>();

    static SoundEngine soundEngine;
    AudioSource bgm;

	void Awake () {
        soundEngine = this;
        gameObject.AddComponent<AudioSource>();
        Object[] ec = Resources.LoadAll("Sounds/Effects",typeof(AudioClip));
        foreach (Object c in ec)
        {
            AudioClip ac = (AudioClip)c;
            effects.Add(ac.name, ac);
        }
        Object[] mc = Resources.LoadAll("Sounds/Music", typeof(AudioClip));
        foreach (Object c in mc)
        {
            AudioClip ac = (AudioClip)c;
            music.Add(ac.name, ac);
        }

        bgm = gameObject.GetComponent<AudioSource>();
        bgm.clip = music["tongue toad"];
        bgm.loop = true;
        //bgm.PlayOneShot(music["tongue toad"]);
        
        bgm.Play();
	}

    void Start() { 
    }

    void Update()
    {
        for (int i = 0; i < playingAudio.Count; i++)
        {
            if(playingAudio[i].time>=playingAudio[i].clip.length)
                Destroy(playingAudio[i].gameObject);
            if (Pause.isPaused && bgm.isPlaying)
            {
                bgm.Pause();
                //Debug.Log("Pausing bgm");
                playingAudio[i].Pause();
            }
            else if (!Pause.isPaused && !bgm.isPlaying)
            {
                bgm.Play();
                //Debug.Log("Resuming bgm");

                playingAudio[i].Play();
            }

        }
    }

    public static SoundEngine Get()
    {
        return soundEngine;
    }


    public bool PlayEffect(string name, Transform location)
    {
        
        AudioClip clip = effects[name];
        AudioSource.PlayClipAtPoint(clip, location.position);
        return true;
    }
    public bool PlayEffect(string name)
    {
        AudioClip clip = effects[name];
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        return true;
    }

    public void PlayMusic(string name, bool background, Transform location)
    {
        AudioClip clip = music[name];
        if (background)
        {
            bgm.Stop();
            bgm.clip = music[name];
            bgm.Play();
        }
        else
        {
            playingAudio.Add(PlayTempClipAt(clip, location.position));
            
        }
    }
    public void PlayMusic(string name, bool background)
    {
        AudioClip clip = music[name];
        if (background)
        {
            bgm.Stop();
            bgm.clip = music[name];
            bgm.Play();
        }
        else
        {
            playingAudio.Add(PlayTempClipAt(clip, Vector3.zero));
        }
    }


    public AudioSource PlayTempClipAt(AudioClip clip, Vector3 position)
    {
        GameObject goTemp = new GameObject("TempAudio");
        goTemp.transform.position = position;
        AudioSource aSource = goTemp.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.Play();
        Destroy(goTemp, clip.length); 
        return aSource; 
    }

}
