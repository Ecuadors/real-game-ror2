using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsScreenSound : MonoBehaviour
{
    public AudioSource backgroundMusic;

    public TMP_Text kills;
    public TMP_Text items;
    public TMP_Text damage;
    public TMP_Text shots;
    public TMP_Text time;
    public TMP_Text level;
    // Start is called before the first frame update
    void Start()
    {
        kills.text = "Kills: " + EndgameManager.kills;
        items.text = "Items: " + EndgameManager.items;
        damage.text = "Damage: " + Mathf.RoundToInt(EndgameManager.damage);
        shots.text = "Shots Fired: " + EndgameManager.shots;
        time.text = "Time: " + EndgameManager.time;
        level.text = "Level: " + EndgameManager.level;
    }

    // Update is called once per frame
    void Update()
    {
        if (StatsManager.doBackgroundMusic)
        {
            backgroundMusic.volume = (StatsManager.Volume/500f);

        } 
        else 
        {
            backgroundMusic.volume = 0;
        }
    }
}
