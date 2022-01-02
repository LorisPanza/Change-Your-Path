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
        if (kvothe != null){
            velocity = kvothe.GetMoveDelta();
            AudioSource walkSrc = GetSound("Walk").source;
            if ((velocity.x != 0 || velocity.y != 0) && kvothe.velocity != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            if (isMoving)
            {
                if (!walkSrc.isPlaying)
                    walkSrc.Play();
            }
            else
                walkSrc.Stop();
        }
        
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
    {   Sound[] s = new Sound[4];
        s[0] = GetSound("Background");
        s[1] = GetSound("DialogueBackground");
        s[2] = GetSound("Labyrinth");
        s[3] = GetSound("RobotGame");
        //Sound s = GetSound("Background");
        for (var i=0; i<s.Length; i++) {
            if (s[i] == null) {
                Debug.LogWarning("Sound: Music #" + i +" not found!");
                return;
            }
            s[i].source.volume = vol;
        }
    }

    public void SetControlVolume (float vol)
    {   
        Sound[] s = new Sound[12];
        s[0] = GetSound("Click");
        s[1] = GetSound("openMap");
        s[2] = GetSound("closeMap");
        s[3] = GetSound("mapFlip");
        s[4] = GetSound("mapMovement");
        s[5] = GetSound("mapSelection");
        s[6] = GetSound("mapChoice");
        s[7] = GetSound("mapDenied");
        s[8] = GetSound("QuestCompleted");
        s[9] = GetSound("Victory");
        s[10] = GetSound("Countdown");
        s[11] = GetSound("Lose");

        for (var i=0; i<s.Length; i++) {
            if (s[i] == null) {
                Debug.LogWarning("Sound: Control #" + i +" not found!");
                return;
            }
            s[i].source.volume = vol;
        }
    }

    public void SetAmbientVolume (float vol)
    {
        Sound[] s = new Sound[2];
        s[0] = GetSound("Walk");
        s[1] = GetSound("Voice");
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == null)
            {
                Debug.LogWarning("Sound: Ambient #" + i + " not found!");
                return;
            }
            s[i].source.volume = vol;
        }
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
