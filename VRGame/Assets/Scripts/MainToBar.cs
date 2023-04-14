using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainToBar : MonoBehaviour
{
    public string nextScene;
    private void MoveToScene(Collision2D collision)
    {
        SceneManager.LoadScene(nextScene);
    }
}
