using FMODUnity;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenuNoHandTracking : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private InputActionReference menu;
    [SerializeField] private Sprite muteMusicIcon;
    [SerializeField] private Sprite unMuteMusicIcon;

    [SerializeField] private Sprite muteSFXIcon;
    [SerializeField] private Sprite unMuteSFXIcon;

    [SerializeField] private Image musicIcon;
    [SerializeField] private Image sfxIcon;
    [SerializeField] private Slider volumeSlider;
    private Vector3 origPos;
    private Quaternion origRot;
    private RectTransform canvasTransform;
    private bool openMenu = false;
    public void ExitClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void MusicMuteClicked()
    {
        AudioManager.Instance.MusicMute = !AudioManager.Instance.MusicMute;
        PlayerPrefs.SetInt("musicmute", AudioManager.Instance.MusicMute ? 1 : 0);
        UpdateVolumeDisplay();
    }

    public void SFXMuteClicked()
    {
        AudioManager.Instance.SFXMute = !AudioManager.Instance.SFXMute;
        PlayerPrefs.SetInt("sfxmute", AudioManager.Instance.SFXMute ? 1 : 0);
        UpdateVolumeDisplay();
    }

    public void VolumeSliderChanged(System.Single newValue)
    {
        AudioManager.Instance.MasterVolume = newValue;
        PlayerPrefs.SetFloat("volume", newValue);
    }

    private void UpdateVolumeDisplay()
    {
        musicIcon.sprite = AudioManager.Instance.MusicMute ? muteMusicIcon : unMuteMusicIcon;
        sfxIcon.sprite = AudioManager.Instance.SFXMute ? muteSFXIcon : unMuteSFXIcon;
        volumeSlider.value = AudioManager.Instance.MasterVolume;
    }

    private void Awake()
    {
        menu.action.started += MenuPressed;
    }

    private void MenuPressed(CallbackContext context)
    {
        if (context.started)
        {
            openMenu = !openMenu;
            AudioManager.Instance.PlayOneShot(openMenu ? FMODEvents.Instance.OpenMenu : FMODEvents.Instance.CloseMenu, gameObject.transform.position);
        }
    }

    private void Start()
    {
        canvasTransform = canvas.GetComponent<RectTransform>();
        origPos = canvasTransform.anchoredPosition3D;
        origRot = canvasTransform.localRotation;
        UpdateVolumeDisplay();
    }

    private void Update()
    {

        canvasTransform.anchoredPosition3D = new Vector3(origPos.x, origPos.y + 0.3f, origPos.z - 0.18f);
        canvasTransform.localRotation = Quaternion.Euler(origRot.x, origRot.y, 0);

        if (openMenu)
            canvas.alpha = 1;
        else
            canvas.alpha = 0;
    }
}
