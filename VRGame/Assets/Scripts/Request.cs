using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    public readonly List<string> request = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        request.Add("Vodka");
        request.Add("Whiskey");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Cup")
        {
            other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            other.transform.rotation = Quaternion.identity;
            other.GetComponent<Rigidbody>().freezeRotation = true;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            float grade = CalculateScore(other.gameObject);
            print(grade);
        }
    }

    private float CalculateScore(GameObject container)
    {
        int score = 0;
        int maxScore = 5 * request.Count;
        // TODO get score calculator working. needs to accurately reflect if elements are in the cup.
        foreach (string content in container.GetComponent<CupContainer>().contents) {
            if (request.Contains(content))
            {
                score += 5;
                print(content);
            }
            else
            {
                score -= 1;
                print(content);
            }
        }
            
        return score/maxScore;
    }

}
