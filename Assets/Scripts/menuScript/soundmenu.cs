using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundmenu : MonoBehaviour
{
    public AudioSource BackgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundMusic.volume = (StatsManager.Volume/500f);
        if (StatsManager.doBackgroundMusic == false)
        {
            BackgroundMusic.volume = (0);

        }
    }
}
