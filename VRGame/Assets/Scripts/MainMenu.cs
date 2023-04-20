using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartClicked()
    {
        LevelManager.Instance.LoadLevel(1);
    }

    public void ExitClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
