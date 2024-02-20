using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfoButton : MonoBehaviour
{
    [SerializeField] private AudioSource SFX;
    public float transitionSpeed = 0.8f; // Adjust this to control the speed of the transition

    private bool transitioning = false;

    public void invokeScene(float targetYPosition)
    {
        SFX.volume = (StatsManager.Volume/500f);
        if (StatsManager.doSFX == false)
        {
            SFX.volume = (0);
        }
        SFX.Play(0);
        if (!transitioning)
        {
            transitioning = true;
            StartCoroutine(MoveCameraSmoothly(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, targetYPosition, Camera.main.transform.position.z)));
        }
    }

    IEnumerator MoveCameraSmoothly(Vector3 initialPosition, Vector3 targetPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionSpeed)
        {
            Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / transitionSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = targetPosition;
        transitioning = false;
    }
}