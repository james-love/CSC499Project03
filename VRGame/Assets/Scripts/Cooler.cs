using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : MonoBehaviour
{
    private Animator coolerAnimator;
    private bool doorOpen;
    // Start is called before the first frame update

    private void Awake()
    {
        coolerAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) {
            coolerAnimator.SetBool("Door Open", true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            coolerAnimator.SetBool("Door Open", false);
        }
    }
}
