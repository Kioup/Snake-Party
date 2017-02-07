using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoSingleton<SoundManager> {

    public AudioClip GameMusic;
    private AudioSource _audiosource;


    void Start() {
        Debug.Log("test");
        _audiosource = GetComponent<AudioSource>();
        if (!_audiosource.isPlaying) {
            _audiosource.clip = GameMusic;
            _audiosource.Play();
        }
    }



}
