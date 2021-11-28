using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider masterLevel, musicLevel, controlLevel, ambientLevel, brightnessLevel;
    [HideInInspector]
    private bool isMoving = false;
    [HideInInspector]
    private Vector3 velocity;
    public Player kvothe;
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {   
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start () {
        Play("Background");
    }


    void Update () {
        velocity = kvothe.GetMoveDelta();
        AudioSource walkSrc = GetSound("Walk").source;
        if ( (velocity.x != 0 || velocity.y != 0) && kvothe.velocity != 0 ) {
            isMoving = true;
        } else {
            isMoving = false;
        }
        if (isMoving) {
			if (!walkSrc.isPlaying)
			walkSrc.Play ();
		}
		else
			walkSrc.Stop ();
    }

    public Sound GetSound(string name) {
        return Array.Find(sounds, sound => sound.name == name);
    }

    public void Play (string name)
    {
        Sound s = GetSound(name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void SetMasterVolume (float vol)
    {   
        AudioListener.volume = vol;
    }
    
    public void SetMusicVolume (float vol)
    {   
        Sound s = GetSound("Background");
        if (s == null) {
            Debug.LogWarning("Sound: Background not found!");
            return;
        }
        s.source.volume = vol;
    }

    public void SetControlVolume (float vol)
    {   
        Sound[] s = new Sound[8];
        s[0] = GetSound("Click");
        s[1] = GetSound("openMap");
        s[2] = GetSound("closeMap");
        s[3] = GetSound("mapFlip");
        s[4] = GetSound("mapMovement");
        s[5] = GetSound("mapSelection");
        s[6] = GetSound("mapChoice");
        s[7] = GetSound("mapDenied");
        for (var i=0; i<6; i++) {
            if (s[i] == null) {
                Debug.LogWarning("Sound: Controls not found!");
                return;
            }
            s[i].source.volume = vol;
        }
    }

    public void SetAmbientVolume (float vol)
    {   
        Sound s = GetSound("Walk");
        if (s == null) {
            Debug.LogWarning("Sound: Walk not found!");
            return;
        }
        s.source.volume = vol;
    }

    public void SetDefault ()
    {
        SetMasterVolume(1f);
        SetMusicVolume(1f);
        SetControlVolume(1f);
        SetAmbientVolume(1f);
        masterLevel.value = 1;
        musicLevel.value = 1;
        controlLevel.value = 1;
        ambientLevel.value = 1;
        brightnessLevel.value = 0.5f;
    }
}
