using UnityEngine;


public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 20;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    public string content;
    private bool isPouring = false;
    private Stream currentStream = null;
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        bool pourCheck = CalculatePourAngle() > pourThreshold;

        if(isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                
                StartPour();
            }
            else
            {
                
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream = null;

    }

    private float CalculatePourAngle()
    {
        float currentRotation = Mathf.Sqrt(Mathf.Pow(transform.rotation.x, 2) + Mathf.Pow(transform.rotation.z, 2)) * Mathf.Rad2Deg;
        return currentRotation;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        streamObject.AddComponent<CapsuleCollider>();
        streamObject.GetComponent<CapsuleCollider>().isTrigger = false;
        streamObject.transform.tag = content;
        return streamObject.GetComponent<Stream>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Cup")
        {
            gameObject.SetActive(false);
            transform.position = startPos;
            gameObject.SetActive(true);
        }
    }
}
