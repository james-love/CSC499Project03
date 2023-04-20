using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnimator;
    private bool doorOpen;
    // Start is called before the first frame update

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) {
            doorAnimator.SetBool("Door Open", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            doorAnimator.SetBool("Door Open", false);
        }
    }
}
