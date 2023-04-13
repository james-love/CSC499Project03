using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.Hands;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private HandsAndControllersManager handManager;
    [SerializeField] private InputActionReference menu;
    private Vector3 origPos;
    private Quaternion origRot;
    private RectTransform canvasTransform;
    private bool openMenu = false;
    public void ExitClicked()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void MusicMuteClicked()
    {
        
    }
    public void SFXMuteClicked()
    {

    }

    public void VolumeSliderChanged(System.Single newValue)
    {

    }

    private void Awake()
    {
        menu.action.started += MenuPressed;
    }

    private void MenuPressed(CallbackContext context)
    {
        if (context.started)
            openMenu = !openMenu;
    }

    private void Start()
    {
        canvasTransform = canvas.GetComponent<RectTransform>();
        origPos = canvasTransform.anchoredPosition3D;
        origRot = canvasTransform.localRotation;
    }

    private void Update()
    {
        if (handManager.TrackingLeftHand)
        {
            canvasTransform.anchoredPosition3D = origPos;
            canvasTransform.localRotation = origRot;
            openMenu = false;
        }
        else
        {
            canvasTransform.anchoredPosition3D = new Vector3(origPos.x, origPos.y + 0.3f, origPos.z - 0.18f);
            canvasTransform.localRotation = Quaternion.Euler(origRot.x, origRot.y, 0);
        }

        if ((handManager.TrackingLeftHand && transform.rotation.eulerAngles.z < 200 && transform.rotation.eulerAngles.z > 160) || (!handManager.TrackingLeftHand && openMenu))
            canvas.alpha = 1;
        else
            canvas.alpha = 0;
    }
}
