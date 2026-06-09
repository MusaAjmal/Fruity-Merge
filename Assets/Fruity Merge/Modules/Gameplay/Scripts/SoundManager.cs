using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public Sound[] sounds;
    private bool musicMuted = false;
    private bool sfxMuted = false;
    public static SoundManager instance { get; private set; }

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Load saved volume preferences
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        musicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        sfxMuted = PlayerPrefs.GetInt("SFXMuted", 0) == 1;

        if (musicMuted)
            musicVolume = 0f;

        if (sfxMuted)
            sfxVolume = 0f;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * GetTypeVolume(s.soundType);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
       
    }

    public bool IsMusicMuted()
    {
        return musicMuted;
    }

    public bool IsSFXMuted()
    {
        return sfxMuted;
    }

    public void ToggleMusicMute()
    {
        musicMuted = !musicMuted;

        SetVolume(SoundType.Music, musicMuted ? 0f : 1f);

        PlayerPrefs.SetInt("MusicMuted", musicMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSFXMute()
    {
        sfxMuted = !sfxMuted;

        SetVolume(SoundType.SFX, sfxMuted ? 0f : 1f);

        PlayerPrefs.SetInt("SFXMuted", sfxMuted ? 1 : 0);
        PlayerPrefs.Save();
    }




    public void Start()
    {
       Play("Background_Music");
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = s.volume * GetTypeVolume(s.soundType);
        s.source.Play();
    }
    public void PlayOneShotSound(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = s.volume * GetTypeVolume(s.soundType);
        s.source.PlayOneShot(s.clip);
        Debug.Log("SoundPlayed!");
    }
    public void StopSound(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
        
    }

    /// <summary>
    /// Set volume for a sound type (0-1). Updates all playing sources of that type.
    /// </summary>
    public void SetVolume(SoundType type, float volume)
    {
        volume = Mathf.Clamp01(volume);

        if (type == SoundType.Music)
            musicVolume = volume;
        else
            sfxVolume = volume;

        // Update all active sources of this type
        foreach (Sound s in sounds)
        {
            if (s.soundType == type && s.source != null)
                s.source.volume = s.volume * volume;
        }
    }

    public float GetTypeVolume(SoundType type)
    {
        return type == SoundType.Music ? musicVolume : sfxVolume;
    }
}
