using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    [SerializeField] private GameObject PausePanel;
    // public Image pauseImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if (!isPaused)
        {
            // pauseImg.enabled = (true);
            Time.timeScale = 0;
            isPaused = true;
            AudioListener.pause = true;
            PausePanel.SetActive(true);
        }
        else 
        {
            // pauseImg.enabled = (false);
            Time.timeScale = 1;
            isPaused = false;
            AudioListener.pause = false;
            PausePanel.SetActive(false);
        }
    }
}
