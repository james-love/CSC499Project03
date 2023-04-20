using UnityEngine;
using UnityEngine.Events;

public class Drunk : MonoBehaviour
{
    public UnityEvent<bool> RequestFinished;
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Rigidbody rb;
    private bool inHand = false;
    private bool finished = false;
    private bool screaming = false;

    public void HoverStart()
    {
        canvas.alpha = 1;
    }

    public void HoverEnd()
    {
        canvas.alpha = 0;
    }

    public void GrabStart()
    {
        inHand = true;
    }

    public void GrabEnd()
    {
        inHand = false;
    }

    private void Update()
    {
        if (!screaming && !inHand && rb.velocity.magnitude > 20f)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Scream, transform.position);
            screaming = true;
        }

        if (!finished && transform.position.y < -10f)
        {
            finished = true;
            RequestFinished.Invoke(true);
        }
    }
}
