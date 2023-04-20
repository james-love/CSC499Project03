using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private Animator fadeTransition;
    public bool Loading { get; private set; }

    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(LoadAsync(levelIndex));
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator LoadAsync(int levelIndex)
    {
        Loading = true;
        Time.timeScale = 0;
        fadeTransition.SetTrigger("Start");
        yield return new WaitUntil(() => Utility.AnimationFinished(fadeTransition, "TransitionStart"));

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        while (!operation.isDone)
            yield return null;

        switch (levelIndex)
        {
            case 0:
                Player.Instance.gameObject.transform.position = new Vector3(0f, 0f, 0f);
                Player.Instance.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 1:
                Player.Instance.gameObject.transform.position = new Vector3(6.92f, 0.62f, -19.73f);
                Player.Instance.gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            default:
                break;
        }

        fadeTransition.SetTrigger("Loaded");
        Loading = false;
        Time.timeScale = 1;
    }
}
