using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupContainer : MonoBehaviour
{
    [SerializeField] public List<string> contents = new List<string>();
    [SerializeField] GameObject fill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contents.Count > 0)
        {
            fill.SetActive(true);
        }
        else
        {
            fill.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //determines if liquid is already in cup, and added if it is not in the cup.
        if (collision.transform.tag == "Liquid")
        {
            if (contents.Contains(collision.gameObject.GetComponent<PourDetector>().content))
            {
                print("its already in the cup");
            }
            else
            {
                contents.Add(collision.gameObject.GetComponent<PourDetector>().content);
                print("Added to cup");
            }
        }
    }
    
}
