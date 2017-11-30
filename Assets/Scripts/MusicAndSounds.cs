using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAndSounds : MonoBehaviour {

    public AudioSource soundEffectSource;
    public AudioSource musicSource;
    public static MusicAndSounds instance = null;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
	}

    public void playSound(AudioClip theSound)
    {
        soundEffectSource.clip = theSound;
        soundEffectSource.pitch = 1;
        soundEffectSource.timeSamples = 0;
        soundEffectSource.Play();
    }

    public void playSoundFast(AudioClip theSound, float thePitch)
    {
        soundEffectSource.clip = theSound;
        soundEffectSource.pitch = thePitch;
        soundEffectSource.timeSamples = theSound.samples / 2;
        soundEffectSource.Play();
    }

    public void playSoundReverse(AudioClip theSound)
    {
        soundEffectSource.clip = theSound;
        soundEffectSource.pitch = -1;
        soundEffectSource.timeSamples = soundEffectSource.clip.samples - 1;
        soundEffectSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
