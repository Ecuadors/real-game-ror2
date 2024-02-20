using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public GameObject[] CommonItemPrefabs;
    public GameObject[] RareItemPrefabs;
    public GameObject[] LegendaryItemPrefabs;
    private float PlayerXPos;
    private float PlayerYPos;
    private float enemyYspawn;
    public Vector3 itemSpawnLoc;
    private float timer;
    public float SpawnTime = 7.0f;
    private float randomSpawn = 7.0f;
    private int itemSpawn = 20;
    private int whichEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > randomSpawn) 
        {
            timer = 0f;
            randomSpawn = Random.Range(SpawnTime-1.8f, SpawnTime+1.8f);
            if (Random.Range(0, 4) > 0)
            {
                SpawnGroundEnemies();
            }
            else 
            {
                SpawnAirEnemies();
            }
            if (SpawnTime >= 2.6f)
            {
                SpawnTime -= 0.08f;
            }
        }
    }

    void SpawnGroundEnemies() 
    {
        int randomNum = Random.Range(0, 10);
        if (randomNum < 3)
        {
            whichEnemy = 2;
        }
        else if (randomNum > 7)
        {
            whichEnemy = 1;
        }
        else 
        {
            whichEnemy = 0;
        }
        int randomLocation = Random.Range(0, 2);
        if (randomLocation == 0)
        {
            enemyYspawn = 0;
        }
        else 
        {
            enemyYspawn = 14;
        }
        PlayerXPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position.x;
        PlayerYPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position.y;

        if (PlayerXPos - 60 < -230)
        {
            Instantiate(EnemyPrefabs[whichEnemy], new Vector3((PlayerXPos + 40), enemyYspawn, 0), Quaternion.identity);
        }
        if (PlayerXPos + 60 > 230)
        {
            Instantiate(EnemyPrefabs[whichEnemy], new Vector3((PlayerXPos - 40), enemyYspawn, 0), Quaternion.identity);
        }
        if (PlayerXPos < 170 && PlayerXPos > -170)
        {
            if (Random.Range(0, 2) == 0) 
            {
                Instantiate(EnemyPrefabs[whichEnemy], new Vector3(Random.Range(PlayerXPos-60, PlayerXPos-20), enemyYspawn, 0), Quaternion.identity);
            } else {
                Instantiate(EnemyPrefabs[whichEnemy], new Vector3(Random.Range(PlayerXPos+20, PlayerXPos+60), enemyYspawn, 0), Quaternion.identity);
            }
        }
    }
    void SpawnAirEnemies() 
    {
        PlayerXPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position.x;
        if (PlayerXPos - 60 < -230)
        {
            Instantiate(EnemyPrefabs[3], new Vector3((PlayerXPos + 40), PlayerYPos+Random.Range(4, 8), 0), Quaternion.identity);
        }
        if (PlayerXPos + 60 > 230)
        {
            Instantiate(EnemyPrefabs[3], new Vector3((PlayerXPos - 40), PlayerYPos+Random.Range(4, 8), 0), Quaternion.identity);
        }
        if (PlayerXPos < 170 && PlayerXPos > -170)
        {
            if (Random.Range(0, 2) == 0) 
            {
                Instantiate(EnemyPrefabs[3], new Vector3(Random.Range(PlayerXPos-60, PlayerXPos-20), PlayerYPos+Random.Range(4, 8), 0), Quaternion.identity);
            } else {
                Instantiate(EnemyPrefabs[3], new Vector3(Random.Range(PlayerXPos+20, PlayerXPos+60), PlayerYPos+Random.Range(4, 8), 0), Quaternion.identity);
            }
        }

    }
    public void SpawnBoss()
    {
        PlayerXPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position.x;
        if (PlayerXPos - 60 < -230)
        {
            Instantiate(EnemyPrefabs[4], new Vector3(PlayerXPos+40, PlayerYPos+1, 0), Quaternion.identity);   
        }
        if (PlayerXPos + 60 > 230)
        {
            Instantiate(EnemyPrefabs[4], new Vector3(PlayerXPos-40, PlayerYPos+1, 0), Quaternion.identity);   
        }
        if (PlayerXPos < 170 && PlayerXPos > -170)
        {
            Instantiate(EnemyPrefabs[4], new Vector3(PlayerXPos+60, PlayerYPos+1, 0), Quaternion.identity);
        }
    }

    public void SpawnItems() 
    {
        // chooses which item to spawn
        itemSpawn = (Random.Range(1, 31));
        if (itemSpawn == 1)
        {
            Instantiate(LegendaryItemPrefabs[Random.Range(0, LegendaryItemPrefabs.Length)], itemSpawnLoc, Quaternion.identity);

        }
        else if (itemSpawn <= 8) {
            Instantiate(RareItemPrefabs[Random.Range(0, RareItemPrefabs.Length)], itemSpawnLoc, Quaternion.identity);

        } else
        {
            Instantiate(CommonItemPrefabs[Random.Range(0, CommonItemPrefabs.Length)], itemSpawnLoc, Quaternion.identity);
        }
    }
}
