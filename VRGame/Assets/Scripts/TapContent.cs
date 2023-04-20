using System.Collections.Generic;
using UnityEngine;

public class TapContent : MonoBehaviour
{
    [SerializeField] private string content;

    [System.Obsolete]
    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Cup"))
        {
            List<string> otherContains = other.GetComponent<CupContainer>().contents;
            if (otherContains.Contains(content))
            {
            }
            else
            {
                otherContains.Add(content);
                other.GetComponent<CupContainer>().MixColor(gameObject.GetComponent<ParticleSystem>().startColor);
            }
        }
    }
}
