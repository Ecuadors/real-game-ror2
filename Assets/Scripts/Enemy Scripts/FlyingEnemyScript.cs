using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemyScript : MonoBehaviour
{
    public GameObject enemyExplosion;

    private AudioSource EnemyAudio;
    private AudioClip enemyChomp;

    public GameObject BulletPrefab;
    public Animator anim;

    private int secondswhendied;
    private int counter;
    private float nextFire = 3f;
    public float bulletShootSpeed = 3f;
    private float rotation = 0.0f;


    private float damage = 12.0f;
    public Vector3 playerPos;
    private float scaleModifier;
    public float speedModifier;
    public float ogSpeedModifier;
    public Slider healthbar;
    public bool slowedDown = false;
    public GameObject DamageIndicator;

    private int difficultyScalingMins;
    private int difficultyScalingSecs;
    
    public float difficultyScaling;

    public bool enemyDead = false;

    public Color newColor;
    
    // item checks
    public float blockChance;
    public int isAcid;
    public int ItemSpawnChance = 0;
    public int numOfBombs;
    public int numOfTrees;
    private int numOfStuns; 



    // Start is called before the first frame update
    void Start()
    {
        // RANDOM COLOOORORR!!!
        newColor = new Color(Random.Range(0.1f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), 1f);
        GetComponent<SpriteRenderer>().color = newColor;
        //DIFIFUCLT SCCKKAZKLING!!! 
        difficultyScaling = GameObject.FindWithTag("Player").GetComponent<PlayerController>().difficultyScaling;

        scaleModifier = Random.Range(-0.3f, 0.3f);
        speedModifier = Random.Range(1.5f, 3f);
        ogSpeedModifier = speedModifier;

        if (difficultyScaling >= 6f)
        {
            difficultyScaling *= 2;
        }
        // scales with time
        damage = (difficultyScaling*5f + 12f);
        healthbar.maxValue = Random.Range((difficultyScaling*10f + 15f), (difficultyScaling*10f + 75f));
        // Debug.Log("Damage: " + damage + " health: " + healthbar.maxValue);
        gameObject.transform.localScale += new Vector3(scaleModifier, scaleModifier, scaleModifier);
        healthbar.value = healthbar.maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        numOfBombs = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Bombs;
        ItemSpawnChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().MagicRings;
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        playerPos.y += 3f;
        if ((playerPos-transform.position).magnitude < 40 && GameObject.FindWithTag("Player").GetComponent<Animator>().GetBool("dead") == false) {
            if (!enemyDead)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos, speedModifier * Time.deltaTime);
                if (playerPos.x - transform.position.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, -180.0f, 0.0f); 

                }
                if (playerPos.x - transform.position.x >= 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f); 
                }
            }
        } 
        if ((playerPos-transform.position).magnitude < 25 && GameObject.FindWithTag("Player").GetComponent<Animator>().GetBool("dead") == false) {
            if (!enemyDead)
            {
                if (Time.time > nextFire)
                {
                    StartCoroutine(ShootBullet());
                    nextFire = Time.time + bulletShootSpeed;

                }
            }
        }

        healthbar.value -= (isAcid/140f);
        
        // stun check
        if (speedModifier < ogSpeedModifier)
        {
            speedModifier += 0.006f;
        }

        if (healthbar.value <= 0 && enemyDead != true)
            {
                enemyDead = true;
                OnEnemyDeath();
            }
    }
    IEnumerator ShootBullet()
    {
        anim.SetTrigger("Attacking");
        yield return new WaitForSeconds(0.4f);
        // THE [enemy] SHOOTS AT THE PLAYER

        Vector2 direction = (Vector2)((new Vector3(playerPos.x, playerPos.y-3f, playerPos.z) - transform.position));
        direction.Normalize();

        rotation = (Mathf.Atan2((playerPos.y-3f) - transform.position.y, playerPos.x - transform.position.x)*180 / Mathf.PI);
        
        // Creates the bullet 
        if (rotation < 90 && rotation > -90) 
        {
            GameObject bullet = (GameObject)Instantiate(BulletPrefab, transform.position + (Vector3)(direction * 0.5f), Quaternion.Euler(0, 0, rotation));
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;

        }
        else 
        {
            GameObject bullet = (GameObject)Instantiate(BulletPrefab, transform.position + (Vector3)(direction * 0.5f), Quaternion.Euler(0, 180, (180-rotation)));
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 20;

        }
    }

    // runs when an enemy is killed
    void OnEnemyDeath()
    {
        // ADDS TO THE STATS
        EndgameManager.kills += 1;

        int Sacrafices = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Sacrafices * 2;
        int numOfTrees = GameObject.FindWithTag("Player").GetComponent<PlayerController>().magicTrees;
        float maxHp = GameObject.FindWithTag("Player").GetComponent<PlayerController>().maxHp;
        float HP = GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP;

        GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().itemSpawnLoc = transform.position;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().coins += 10;
        // bombs check
        if (numOfBombs > 0)
        {
            Destroy(gameObject);
            Instantiate(enemyExplosion, new Vector3(transform.position.x, (transform.position.y + (numOfBombs/4f) + 1f), 0), Quaternion.identity);
        }

        // CHECKS IF THEY HAVE OVERHEALING!
        if (numOfTrees > 0)
        {
            maxHp *= (1 + numOfTrees/2f);
        }
        
        if ((HP + (maxHp * Sacrafices/100f)) <= maxHp)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP += (maxHp/(1f + numOfTrees/2f) * Sacrafices/100f);
        } else if ((HP + (maxHp * Sacrafices/100f)) > maxHp)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP = maxHp;
        }
        if (Random.Range(0, 16) <= ((ItemSpawnChance * 2f) + 5))
        {
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnItems();
        }
        // adds xp to the player
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().neededXp -= (8 * (1 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().KnowledgeCrystals)/2f));
        // destroys the thingy
        Destroy(gameObject);
        
        
    }
    
}
