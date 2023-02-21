using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class AudioElement
{
    [field:Header("Audio Source")]
    [field: SerializeField] public AudioSource AudioSource { get; private set; }

    [Header("Pitch")]
    [Range(-3, 3)][SerializeField] private float _minPitch = 1f;
    [Range(-3, 3)][SerializeField] private float _maxPitch = 1f;

    [SerializeField] private AudioClip[] m_clips;

    public void PlayOneShotRandom() => AudioSource.PlayOneShot(GetRandomClip());
    public void PlayOneShot(AudioClip _clip) => AudioSource.PlayOneShot(_clip);

    public void PlayRandom()
    {
        AudioSource.clip = GetRandomClip();
        AudioSource.Play();
    }

    public void Play(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void Stop() => AudioSource.Stop();

    private bool HasAnyClip => m_clips.Length > 0;

    public AudioClip GetRandomClip()
    {
        if (!HasAnyClip)
        {
            Debug.LogWarning("WARNING: No AudioClip was found on this script");
            return null;
        }

        if (m_clips.Length == 1) { return m_clips[0]; }
        return m_clips[UnityEngine.Random.Range(0, m_clips.Length)];
    }

    public AudioClip GetClip(int index)
    {
        if (!HasAnyClip)
        {
            Debug.LogWarning("WARNING: No AudioClip was found on this script");
            return null;
        }

        if (index > m_clips.Length) { return null; }
        return m_clips[index];
    }

    public AudioClip GetClip(string name)
    {
        if (!HasAnyClip)
        {
            Debug.LogWarning("WARNING: No AudioClip was found on this script");
            return null;
        }

        return m_clips.First(x => x.name.ToLower().Contains(name.ToLower()));
    }

    public void SetRandomPitch()
    {
        if (_minPitch == _maxPitch) 
        {
            AudioSource.pitch = _minPitch;
            return;
        }

        AudioSource.pitch = UnityEngine.Random.Range(_minPitch, _maxPitch);
    }

    public void ResetPitch()
    {
        AudioSource.pitch = 1f;
    }
}
