using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartClicked()
    {
    
        SceneManager.LoadScene("VRGame");
    
}

    public void ExitClicked()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
