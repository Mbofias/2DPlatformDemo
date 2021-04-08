using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAudioNoLoop : MonoBehaviour
{
    public AudioClip[] audios;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().clip = audios[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
