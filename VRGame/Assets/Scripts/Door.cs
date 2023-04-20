using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    private bool doorOpen;
    // Start is called before the first frame update
    public void doorAnimation()
    {
        if (doorOpen)
        {
            doorAnimator.SetBool("Door Open", false);
            doorOpen = false;
        }
        else
        {
            doorAnimator.SetBool("Door Open", true);
            doorOpen = true;
        }
    }
}
