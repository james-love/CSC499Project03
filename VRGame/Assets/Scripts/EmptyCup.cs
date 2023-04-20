using System.Collections.Generic;
using UnityEngine;

public class EmptyCup : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Cup"))
        {
            other.GetComponent<CupContainer>().contents = new List<string>();
        }
    }
}
