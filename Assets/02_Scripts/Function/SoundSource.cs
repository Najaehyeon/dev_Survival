using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundSource : MonoBehaviour, IPoolable
{
    private AudioSource _audioSource;

    private Action<GameObject> returnToPool;
    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        CancelInvoke();
        _audioSource.clip = clip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        Invoke("Disable", clip.length + 2);
    }

    public void Disable()
    {
        _audioSource.Stop();
        OnDespawn();
    }

    public void Initialize(Action<GameObject> returnAction)
    {
        returnToPool = returnAction;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        returnToPool?.Invoke(gameObject);
    }
}
