using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    public List<string> request = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        request.Add("Vodka");
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
            string letterGrade = CalculateGrade(grade);
            print("You got a " + letterGrade);
        }
    }

    private string CalculateGrade(float grade)
    {
        string res;
        if (grade >= 90)
        {
            res = "A";
        }
        else if (grade >= 80)
        {
            res = "B";
        }
        else if (grade >= 70)
        {
            res = "C";
        }
        else if (grade >= 60)
        {
            res = "D";
        }
        else
        {
            res = "F";
        }
        return res;
    }

    private float CalculateScore(GameObject container)
    {
        int score = 0;
        float maxScore = 5f * request.Count;
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
            
        return (float)score/maxScore;
    }

}
