using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource myAudio;
    public AudioClip[] music;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusic(int trackNumber)
    {
        myAudio.clip = music[trackNumber];
        myAudio.Play();
    }
}
