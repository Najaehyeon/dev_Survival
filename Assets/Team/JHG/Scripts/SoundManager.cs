using System.Collections;
using System.Collections.Generic;
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
    private void Awake()
    {
        instance = this;
        BGMAudioSource = GetComponent<AudioSource>();
        BGMAudioSource.volume = BGM;
        BGMAudioSource.loop = true;
        //슬라이더 추가시 주석 해제
        //sliderBGM.value = BGM;
    }

    private void FixedUpdate()
    {
        //ChangeVolume(); 
    }

    private void Start()
    {
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

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.SFX, instance.soundEffectPitchVariance);
    }
}
