using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[SerializeField]
public class Sound : MonoBehaviour
{
    public AudioSource source;
    public string clipName;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    public bool loop;
    
}
