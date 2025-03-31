using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField][Range(0f, 1f)] private float BGM;
    [SerializeField][Range(0f, 1f)] private float SFX;

    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;

    private AudioSource BGMAudioSource;
    public AudioClip BGMClip;

    private void Awake()
    {
        BGMAudioSource = GetComponent<AudioSource>();
        BGMAudioSource.volume = BGM;
        BGMAudioSource.loop = true;
        //슬라이더 추가시 주석 해제
        //sliderBGM.value = BGM;
    }

    private void FixedUpdate()
    {
        //BGMAudioSource.volume = sliderBGM.value;
    }

    private void Start()
    {
        ChangeBackGroundMusic(BGMClip);
    }

    public void ChangeBackGroundMusic(AudioClip clip)
    {
        BGMAudioSource.Stop();
        BGMAudioSource.clip = clip;
        BGMAudioSource.Play();
    }

    //public static void PlayClip(AudioClip clip)
    //{
    //    SoundSource obj = Instantiate(instance.soundSourcePrefab);
    //    SoundSource soundSource = obj.GetComponent<SoundSource>();
    //    soundSource.Play(clip, instance.SFX, instance.soundEffectPitchVariance);
    //}
}
