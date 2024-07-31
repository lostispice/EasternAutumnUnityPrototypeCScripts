using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {        
    }

    // Resets the flag stamp on the mail item
    public void ResetStamp()
    {
        for (int n = 0; n < transform.childCount; n++)
        {
            transform.GetChild(n).gameObject.SetActive(false);
        }
    }
}
