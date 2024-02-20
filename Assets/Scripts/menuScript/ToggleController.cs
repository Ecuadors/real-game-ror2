using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Toggle toggle;
    public Sprite checkedSprite;
    public Sprite uncheckedSprite;
    public AudioSource SFX;

    private Image toggleImage;

    private void Start()
    {
        SFX.ignoreListenerPause = true;
        // Get the Image component of the toggle
        toggleImage = toggle.GetComponent<Image>();

        // Listen for changes in the toggle state
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        // checks if the toggle should be on or not
        
       if (gameObject.name == ("SFX toggle"))
        {
            toggleImage.sprite = StatsManager.doSFX ? checkedSprite : uncheckedSprite;

        }
        if (gameObject.name == ("Background music Toggle"))
        {
            toggleImage.sprite = StatsManager.doBackgroundMusic ? checkedSprite : uncheckedSprite;

        }

    }

    private void OnDestroy()
    {
        // Remove the listener to avoid memory leaks
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (gameObject.name == "Background music Toggle") 
        {
            StatsManager.doBackgroundMusic = !StatsManager.doBackgroundMusic;
            SFX.volume = (StatsManager.Volume/500f);
            if (!StatsManager.doSFX)
            {
                SFX.volume = 0;
            }

            SFX.Play();
        }
        if (gameObject.name == "SFX toggle") 
        {
            StatsManager.doSFX = !StatsManager.doSFX;
            SFX.volume = (StatsManager.Volume/500f);
            if (!StatsManager.doSFX)
            {
                SFX.volume = 0;
            }

            SFX.Play();
        }


        // Swap the sprite based on the toggle state
        toggleImage.sprite = isOn ? checkedSprite : uncheckedSprite;
    }
}