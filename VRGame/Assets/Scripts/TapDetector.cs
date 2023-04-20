
using FMOD.Studio;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class TapDetector : MonoBehaviour
{
    [SerializeField] private ParticleSystem stream;
    [SerializeField] private bool grabbable = true;
    private bool inHand = false;
    private bool beerTapOff = true;
    private EventInstance sound;
    private XRLever lever;

    public void Grab()
    {
        inHand = true;
    }

    public void LetGo()
    {
        inHand = false;
        stream.Stop();
        sound.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void activate()
    {
        if ((grabbable && inHand) || !grabbable)
        {
            stream.Play();
            sound.getPlaybackState(out PLAYBACK_STATE playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                sound.start();
        }
    }

    public void deactivate()
    {
        stream.Stop();
        sound.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void BeerTap()
    {
        beerTapOff = !beerTapOff;
    }

    public void UpdateLever()
    {
        lever.value = beerTapOff;
        if (lever.value)
            deactivate();
        else
            activate();
    }

    private void Start()
    {
        sound = AudioManager.Instance.CreateInstance(FMODEvents.Instance.Pouring);
        if (!grabbable)
            lever = GetComponent<XRLever>();
    }

    private void Update()
    {
        if (grabbable && !inHand)
            deactivate();
    }

    private void OnDestroy()
    {
        sound.stop(STOP_MODE.IMMEDIATE);
    }
}
