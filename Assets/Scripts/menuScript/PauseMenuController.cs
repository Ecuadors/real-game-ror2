using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    
    [SerializeField] private Toggle SFXToggle;
    [SerializeField] private Toggle MusicToggle;
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private TMP_Text VolumeText;
    public Sprite checkedSprite;
    public Sprite uncheckedSprite;
    void Awake()
    {
        // Set the initial sprite based on the initial toggle state
        // SFXToggle.image.sprite = StatsManager.doSFX ? checkedSprite : uncheckedSprite;
        // MusicToggle.image.sprite = StatsManager.doBackgroundMusic ? checkedSprite : uncheckedSprite;
    }
    // Start is called before the first frame update
    void Start()
    {

        VolumeSlider.value = StatsManager.Volume;
        VolumeText.text = "Volume: " + StatsManager.Volume;
        SFXToggle.isOn = StatsManager.doSFX;
        MusicToggle.isOn = StatsManager.doBackgroundMusic;
        
        //Adds a listener to the main slider and invokes a method when the value changes.
		VolumeSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});

    }
    void ValueChangeCheck()
	{
		StatsManager.Volume = VolumeSlider.value;
        VolumeText.text = "Volume: " + StatsManager.Volume;
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
