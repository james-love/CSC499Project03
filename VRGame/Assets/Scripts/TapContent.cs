using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapContent : MonoBehaviour
{
    [SerializeField] private string content;

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Cup"))
        {
            List<string> otherContains = other.GetComponent<CupContainer>().contents;
            if (otherContains.Contains(content))
            {
                print("Already in cup");
            }
            else
            {
                print("Added to cup");
                otherContains.Add(content);
            }
        }
    }
}
