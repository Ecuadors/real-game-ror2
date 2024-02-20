using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.006f, 0, 0);
        if (transform.position.x > 1300)
        {
            transform.position = new Vector3(-2445.635f, 0, 0);
        }

    }
}
