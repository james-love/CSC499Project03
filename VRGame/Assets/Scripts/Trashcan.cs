using UnityEngine;

public class Trashcan : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().freezeRotation = true;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
