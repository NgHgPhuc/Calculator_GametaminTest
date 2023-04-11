using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public static SoundObject Instance { get; private set; }

    public AudioSource BGM;
    public AudioSource SoundFx;

    public AudioClip ButtonNumberClick;
    public AudioClip MathButtonClick;
    public AudioClip Error;
    public AudioClip DeleteButton;
    public AudioClip ACButton;
    public AudioClip EqualButton;
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetVolumnBGM(float value)
    {
        BGM.volume = value;
    }
    public void SetVolumnSoundFX(float value)
    {
        SoundFx.volume = value;
    }
    public void PlaySoundFx(AudioClip audioClip)
    {
        SoundFx.clip = audioClip;
        SoundFx.Play();
    }
}
