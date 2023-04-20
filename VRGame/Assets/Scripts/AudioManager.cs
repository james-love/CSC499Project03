using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float MasterVolume = 1;
    public bool MusicMute = false;
    public bool SFXMute = false;

    private List<EventInstance> eventInstances;
    private EventInstance musicEventInstance;
    public static AudioManager Instance { get; private set; }

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            eventInstances = new();

            masterBus = RuntimeManager.GetBus("bus:/");
            musicBus = RuntimeManager.GetBus("bus:/Music");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");

            if (PlayerPrefs.HasKey("volume"))
                MasterVolume = PlayerPrefs.GetFloat("volume");
            if (PlayerPrefs.HasKey("musicmute"))
                MusicMute = PlayerPrefs.GetInt("musicmute") == 1;
            if (PlayerPrefs.HasKey("sfxmute"))
                SFXMute = PlayerPrefs.GetInt("sfxmute ") == 1;

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeMusic(FMODEvents.Instance.GameMusic);
    }

    private void Update()
    {
        masterBus.setVolume(MasterVolume);
        musicBus.setMute(MusicMute);
        sfxBus.setMute(SFXMute);
    }

    private void InitializeMusic(EventReference musicEvent)
    {
        musicEventInstance = CreateInstance(musicEvent);
        musicEventInstance.start();
    }

    public EventInstance CreateInstance(EventReference eventRef)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventRef);
        //eventInstances.Add(eventInstance);
        return eventInstance;
    }
}
