using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 20;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    private bool isPouring = false;
    private Stream currentStream = null;

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() > pourThreshold;

        if(isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                print("Start");
                StartPour();
            }
            else
            {
                print("end");
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        
    }

    private void EndPour()
    {


    }

    private float CalculatePourAngle()
    {
        float currentRotation = Mathf.Sqrt(Mathf.Pow(transform.rotation.x, 2) + Mathf.Pow(transform.rotation.z, 2)) * Mathf.Rad2Deg;
        print(currentRotation);
        return currentRotation;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
}