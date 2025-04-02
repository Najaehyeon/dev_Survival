using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField][Range(0f, 1f)] private float BGM;
    [SerializeField][Range(0f, 1f)] private float SFX;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;


    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;

    private AudioSource BGMAudioSource;
    public AudioClip BGMClip;

    public SoundSource soundSourcePrefab;

    ObjectPoolManager objectPoolManager;
    private void Awake()
    {
        instance = this;
        BGMAudioSource = GetComponent<AudioSource>();
        BGMAudioSource.volume = BGM;
        BGMAudioSource.loop = true;
        sliderBGM.value = BGM;
        sliderSFX.value = SFX;
    }
    private void FixedUpdate()
    {
        ChangeVolume(); 
    }

    private void Start()
    {
        objectPoolManager = ObjectPoolManager.Instance;
        ChangeBackGroundMusic(BGMClip);
    }

    public void ChangeVolume()
    {
        BGM = sliderBGM.value;
        BGMAudioSource.volume = BGM;
        SFX = sliderSFX.value;
    }

    public void ChangeBackGroundMusic(AudioClip clip)
    {
        BGMAudioSource.Stop();
        BGMAudioSource.clip = clip;
        BGMAudioSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        GameObject obj = objectPoolManager.GetObject(0, Vector3.zero, Quaternion.identity);

        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.SFX, instance.soundEffectPitchVariance);
    }

}
