using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartClicked()
    {

    }

    public void ExitClicked()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
