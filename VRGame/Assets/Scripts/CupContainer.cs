
using System.Collections.Generic;
using UnityEngine;

public class CupContainer : MonoBehaviour
{
    [SerializeField] public List<string> contents = new List<string>();
    [SerializeField] GameObject fill;
    [SerializeField] GameObject colorChange;

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

    public void MixColor(Color othercolor)
    {
        var rend = colorChange.GetComponent<Renderer>();
        if (contents.Count == 0)
        {
            rend.material.color = othercolor;
        }
        else {
            var newcolor = Color.Lerp(othercolor, rend.material.color, 0.5f);
            rend.material.color = newcolor;
        }
        
    }

    
    
}
