using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource myAudio;
    public AudioClip[] inGameEffects;
    public AudioClip[] CutSceneEffects;

    public AudioClip[] effectsRandom;
    public AudioClip[] swooshRandom;


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

    public void PlayPunch()
    {
        myAudio.clip = effectsRandom[Random.Range(0, effectsRandom.Length)];
        myAudio.Play();
    }

    public void PlaySwoosh()
    {
        myAudio.clip = swooshRandom[Random.Range(0, swooshRandom.Length)];
        myAudio.Play();
    }
}
