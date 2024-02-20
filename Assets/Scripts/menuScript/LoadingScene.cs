using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    
    public void LoadScene(int sceneId)
    {
        LoadingScreen.SetActive(true);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PauseMenu.isPaused = false;
            AudioListener.pause = false;
        }
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    
    IEnumerator LoadSceneAsync(int sceneId)
    {
        
        // Artificially increase the fill value over a short duration
        float elapsedTime = 0f;
        float duration = 0.5f; // Adjust the duration as needed
        while (elapsedTime < duration * 0.75f)
        {
            float progressValue = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            LoadingBarFill.fillAmount = progressValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (elapsedTime < duration)
        {
            float progressValue = Mathf.Lerp(0.75f, 1f, elapsedTime / duration);
            LoadingBarFill.fillAmount = progressValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the progress bar is filled completely before loading the scene
        LoadingBarFill.fillAmount = 1f;

        // Start loading the scene asynchronously

        // Update the progress bar during scene loading
        // while (!operation.isDone)
        // {
        //     float progressValue = Mathf.Clamp01(operation.progress);
        //     LoadingBarFill.fillAmount = progressValue;
        //     yield return null;
        // }
        // yield return new WaitForSeconds(2);
    }
}