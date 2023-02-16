using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class AudioElement
{
    [field: SerializeField] public AudioSource AudioSource { get; private set; }

    public void PlayOneShot(AudioClip _clip) => AudioSource.PlayOneShot(_clip);

    [SerializeField] private AudioClip[] m_clips;

    public AudioClip GetRandomClip()
    {
        if (m_clips.Length == 1) { return m_clips[0]; }
        return m_clips[UnityEngine.Random.Range(0, m_clips.Length)];
    }

    public AudioClip GetClip(int index)
    {
        if (index > m_clips.Length) { return null; }
        return m_clips[index];
    }

    public AudioClip GetClipBy(string name)
    {
        return m_clips.First(x => x.name.ToLower().Contains(name.ToLower()));
    }
}
