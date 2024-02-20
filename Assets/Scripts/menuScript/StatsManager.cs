using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public TMP_Text volumeText;
    public Slider volumeSlider;
    public Toggle backgroundToggle;
    public Toggle SFXToggle;
    public static float Volume = 50f;
    public static bool doBackgroundMusic = true;
    public static bool doSFX = true;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = StatsManager.Volume;
        backgroundToggle.isOn = StatsManager.doBackgroundMusic;
        SFXToggle.isOn = StatsManager.doSFX;
    }

    // Update is called once per frame
    void Update()
    {
        volumeText.text = "Volume: " + (volumeSlider.value).ToString();
        Volume = volumeSlider.value;
        doBackgroundMusic = backgroundToggle.isOn;
        doSFX = SFXToggle.isOn;
    }
}
