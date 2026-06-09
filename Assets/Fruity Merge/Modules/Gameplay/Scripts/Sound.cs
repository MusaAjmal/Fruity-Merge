using UnityEngine.Audio;
using UnityEngine;

public enum SoundType { Music, SFX }

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    public SoundType soundType = SoundType.SFX;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}

