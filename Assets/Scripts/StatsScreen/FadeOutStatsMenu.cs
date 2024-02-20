using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutStatsMenu : MonoBehaviour
{
    void Awake()
    {
        invokeScene();
    }
    public void invokeScene()
    {
        GameObject.FindWithTag("fade").GetComponent<Transform>().localScale = new Vector3(18f, 18f, 0);
        InvokeRepeating("LoadScene", 0, 0.01f);
    }
    public void LoadScene()
    { 
        if (GameObject.FindWithTag("fade").GetComponent<Transform>().localScale.x > 0)
        {
            // GameObject.FindWithTag("fade").transform.localScale.x += 0.1f;
            GameObject.FindWithTag("fade").GetComponent<Transform>().localScale -= new Vector3(0.3f, 0.3f, 0);
        }
        else 
        {
            GameObject.FindWithTag("fade").GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
            CancelInvoke();
        }


    }
}
