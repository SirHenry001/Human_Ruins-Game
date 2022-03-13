using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource myAudio;
    public AudioClip[] inGameEffects;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        PlayFX(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFX(int trackNumber)
    {
        myAudio.clip = inGameEffects[trackNumber];
        myAudio.Play();
    }

}
