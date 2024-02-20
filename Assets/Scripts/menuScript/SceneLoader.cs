using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string scene;
    // Update is called once per frame
    // void Update()
    // {
    //     if (GameObject.FindWithTag("fade").GetComponent<Transform>().localScale.x < 1000f)
    //     {
    //         // GameObject.FindWithTag("fade").transform.localScale.x += 0.1f;
    //         GameObject.FindWithTag("fade").GetComponent<Transform>().localScale += new Vector3(0.1f, 0.1f, 0);
    //     }
    //     else 
    //     {
    //         LoadScene("sceneName");
    //     }
    // } 
    public void invokeScene(string sceneName)
    {
        scene = sceneName;
        InvokeRepeating("LoadScene", 0, 0.01f);
    }
    public void LoadScene()
    {
        // if (GameObject.FindWithTag("fade").GetComponent<Transform>().localScale.x < 1000f)
        // {
        if (GameObject.FindWithTag("fade").GetComponent<Transform>().localScale.x < 920)
        {
            // GameObject.FindWithTag("fade").transform.localScale.x += 0.1f;
            GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().volume -= 0.002f;
            GameObject.FindWithTag("fade").GetComponent<Transform>().localScale += new Vector3(12f, 12f, 0);
        }
        else
        {
            SceneManager.LoadScene(scene);

        }


    }
}