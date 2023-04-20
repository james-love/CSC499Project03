using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int totalRequests = 5;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int requestsComplete = 0;
    private int successfulRequests = 0;

    public void RequestComplete(bool result)
    {
        requestsComplete += 1;
        if (result)
            successfulRequests += 1;

        CheckResults();
    }

    private void CheckResults()
    {
        if (requestsComplete == totalRequests)
        {
            AudioManager.Instance.PlayOneShot(successfulRequests == totalRequests ? FMODEvents.Instance.Win : FMODEvents.Instance.Lose, GameObject.FindGameObjectWithTag("Player").transform.position);
            canvasGroup.alpha = 1;
            scoreText.text = $"{successfulRequests} / {totalRequests}";
        }
    }
}
