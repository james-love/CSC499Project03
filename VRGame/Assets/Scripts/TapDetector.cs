using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDetector : MonoBehaviour
{
    [SerializeField] private GameObject stream;

    
    public void activate()
    {
        stream.GetComponent<ParticleSystem>().Play();
    }
    public void deactivate()
    {
        stream.GetComponent<ParticleSystem>().Stop();
    }
}
