using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioClip Project_2, Project_4, Project_6, bossMusic;
    public bool bossSpawned;
    private bool changeMusic = true;
    private int StartingMusic;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        StartingMusic = Random.Range(0, 3);
        if (StartingMusic == 0)
        {
            music.clip = Project_2;
        }
        else if (StartingMusic == 1)
        {
            music.clip = Project_4;
        }
        else if (StartingMusic == 2)
        {
            music.clip = Project_6;
        }
        music.Play(0);
        StartCoroutine(ExampleCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        music.volume = (StatsManager.Volume/500f);

        if (StatsManager.doBackgroundMusic == false)
        {
            music.volume = 0;
        }
        if (bossSpawned == true && changeMusic == true)
        {
            StopCoroutine("ExampleCoroutine");
            music.clip = bossMusic;
            music.Play(0);
            changeMusic = false;
        }
        if (bossSpawned == false && changeMusic == false)
        {
            StartCoroutine(ExampleCoroutine());
            changeMusic = true;

        }
    }
    
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(music.clip.length);
        if (music.clip == Project_2)
        {
            music.clip = Project_4;
        }
        if (music.clip == Project_4)
        {
            music.clip = Project_6;
        }
        if (music.clip == Project_6)
        {
            music.clip = Project_2;
        }
        music.Play(0);
        StartCoroutine(ExampleCoroutine());
    }
}
