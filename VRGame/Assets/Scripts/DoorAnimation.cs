using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator door;
    // Start is called before the first frame update
    private void Awake()
    {
        door = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Opening");   
        door.SetBool("Open", true);
    }
    private void OnTriggerExit(Collider other)
    {
        door.SetBool("Open", false);
    }
}
