using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
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

    public void HoverStart()
    {
        print("debug 1");
    }

    public void HoverEnd()
    {
        print("debug 2");
    }
}
