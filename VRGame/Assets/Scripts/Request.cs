using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Request : MonoBehaviour
{
    public List<string> request = new List<string>();
    [SerializeField] GameObject happy;
    [SerializeField] GameObject angry;
    [SerializeField] CanvasGroup requestCanvas;
    [SerializeField] TextMeshProUGUI requestText;
    public UnityEvent<bool> requestFinished;
    private Animator leave;
    // Start is called before the first frame update

    public void HoverStart()
    {
        requestCanvas.alpha = 1;
    }

    public void HoverEnd()
    {
        requestCanvas.alpha = 0;
    }

    private void Awake()
    {
        leave = GetComponentInParent<Animator>();
        requestText.text = request.Aggregate(string.Empty, (acc, cur) => $"{acc}{(acc == string.Empty ? string.Empty : "\n")}{cur}");
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
            StartCoroutine(RequestFinished(match));
        }
    }

    private IEnumerator RequestFinished(bool match)
    {
        yield return new WaitUntil(() => Utility.AnimationFinished(leave, "Leave"));
        requestFinished.Invoke(match);
    }

    private void CustomerApproval(bool match)
    {
        if (match)
        {
            angry.SetActive(false);
            happy.SetActive(true);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Happy, transform.position);
        }
        else
        {
            happy.SetActive(false);
            angry.SetActive(true);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.Angry, transform.position);
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
