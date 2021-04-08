using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
 
    public Sound[] sounds;
    public static AudioManager InstanceAM;

    private void Awake()
    {
        if(InstanceAM == null)
        {
            InstanceAM = this;
        }else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.clip = s.clip;
            s.source.name = s.name;
        }
    }
    private void Start()
    {
        PlayAudio("n","Music");
        CEnemyController.audioManager = this;
        CPlayerController.audioManager = this;
        
    }
    public void PlayAudio(string _name,string characterName)
    {
        switch(characterName)
        {
            case "Enemy01":
                foreach (Sound s in sounds)
                {
                    if (s.name == _name + characterName)
                    {
                        s.source.Play();
                    }
                }
                print("StartSoundEnmy01");
                break;
            case "Enemy02":
                foreach (Sound s in sounds)
                {
                    if (s.name == _name + characterName)
                    {
                        s.source.Play();
                    }
                }
                break;
            case "Enemy03":
                foreach (Sound s in sounds)
                {
                    if (s.name == _name + characterName)
                    {
                        s.source.Play();
                    }
                }
                break;
            case "Enemy04":
                foreach (Sound s in sounds)
                {
                    if (s.name == _name + characterName)
                    {
                        s.source.Play();
                    }
                }
                break;
            case "Boss":
                foreach (Sound s in sounds)
                {
                    if (s.name == _name + characterName)
                    {
                        s.source.Play();
                    }
                }
                break;
            case "Music":
                foreach (Sound s in sounds)
                {
                    if (s.source.name == _name + characterName)
                    {
                        s.source.Play();
                        print("StartSound");
                    }
                    
                }print("StartSound");
                break;

        }
    }
}
