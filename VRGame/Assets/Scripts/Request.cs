using System;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    public List<string> request = new List<string>();
    [SerializeField] GameObject happy;
    [SerializeField] GameObject angry;
    private Animator leave;
    // Start is called before the first frame update

    private void Awake()
    {
        leave = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Cup"))
        {
            other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            other.transform.rotation = Quaternion.identity;
            other.GetComponent<Rigidbody>().freezeRotation = true;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            bool match = matching(other.gameObject);
            CustomerApproval(match);
            leave.SetBool("Leave",true);
            other.gameObject.SetActive(false);
        }
    }

    private void CustomerApproval(bool match)
    {
        if (match)
        {
            angry.SetActive(false);
            happy.SetActive(true);
        }
        else
        {
            happy.SetActive(false);
            angry.SetActive(true);
        }
    }

    private bool matching(GameObject container)
    {
        var check = container.GetComponent<CupContainer>().contents;
        if (request.Count == check.Count)
        {
            foreach (string content in request)
            {
                if (check.Contains(content))
                {
                    check.Remove(content);
                }
                if (check.Count == 0)
                {
                    break;
                }
            }
            bool match = check.Count == 0;
            return match;
        }
        else
        {
            return false;
        }

    }
}
