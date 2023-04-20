using FMOD.Studio;
using UnityEngine;

public class WaterFaucet : MonoBehaviour
{
    [SerializeField] private ParticleSystem water;
    private EventInstance sound;

    private void Start()
    {
        sound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Pouring);
    }

    private void OnTriggerEnter(Collider other)
    {
        water.Play();
        sound.getPlaybackState(out PLAYBACK_STATE playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            sound.start();
    }

    private void OnTriggerExit(Collider other)
    {
        water.Stop();
        sound.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
