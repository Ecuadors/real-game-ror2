using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Transform>().localScale.x < 1)
        {
            GetComponent<Transform>().localScale += new Vector3(0.005f, 0.005f, 0);
        }
        
    }
}
