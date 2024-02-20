using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
     private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
        // Optionally, show pause menu/UI here
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
        // Optionally, hide pause menu/UI here
    }
}
