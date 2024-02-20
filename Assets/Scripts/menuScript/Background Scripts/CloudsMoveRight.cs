using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsMoveRight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (0.08f * Time.deltaTime), transform.position.y, transform.position.z);
        if (transform.position.x >= 260)
        {
            transform.position = new Vector3(-212, transform.position.y, transform.position.z);
        }
    }
}
