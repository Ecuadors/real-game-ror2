using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalBossScript : MonoBehaviour
{
    public GameObject enemyExplosion;

    public GameObject BulletPrefab;
    private float rotation = 0.0f;

    public AudioSource BossAudio;
    public AudioClip bossSpawn;
    private float nextFire = 3f;
    public float attackSpeed = 1f;
    public float bulletShootSpeed = 3f;

    private float damage = 15.0f;
    public Vector3 playerPos;
    public Animator anim;
    public Slider healthbar;
    public TMP_Text HealthText;
    private float nextAttack;
    public float speedModifier;
    public float ogSpeedModifier;
    public bool slowedDown = false;
    public GameObject DamageIndicator;

    private int difficultyScalingMins;
    private int difficultyScalingSecs;
    
    private float difficultyScaling;

    public bool enemyDead = false;
    
    // item checks
    public float blockChance;
    public int isAcid;
    public int ItemSpawnChance = 0;
    public int numOfBombs;



    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().isBossFight = true;
        GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().bossSpawned = true;
        if (StatsManager.doSFX == true)
        {
            BossAudio.PlayOneShot(bossSpawn, (StatsManager.Volume/500f));
        }
        speedModifier = 9f;
        ogSpeedModifier = speedModifier;
        damage = 500;
        healthbar.maxValue = 20000;
        Debug.Log("Boss Damage: " + damage  + " Boss Health: " + healthbar.maxValue);
        gameObject.transform.localScale += new Vector3(16, 16, 16);
        healthbar.value = healthbar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        numOfBombs = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Bombs;
        ItemSpawnChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().MagicRings;
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        // boss healthbar
        HealthText.text = (healthbar.value + "/" + healthbar.maxValue);
        if ((playerPos-transform.position).magnitude < 100 && !GameObject.FindWithTag("Player").GetComponent<Animator>().GetBool("dead")) {
            if (!enemyDead)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos, speedModifier * Time.deltaTime);
                anim.SetBool("running", true);
                if (playerPos.x - transform.position.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, -180.0f, 0.0f); 

                }
                if (playerPos.x - transform.position.x >= 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f); 
                }
                if (Time.time > nextFire)
                {
                    // THE BOSS SHOOTS AT THE PLAYER

                    Vector2 direction = (Vector2)((playerPos - transform.position));
                    direction.Normalize();

                    rotation = (Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x)*180 / Mathf.PI);
                    // Creates the bullet locally
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

                    // Adds velocity to the bullet
                    nextFire = Time.time + bulletShootSpeed;

                }
                
            }
        } else {
            anim.SetBool("running", false);
        }
        healthbar.value -= (isAcid/140f);
        // stun check
        if (speedModifier < ogSpeedModifier)
        {
            speedModifier += 0.009f;
        }  

        if (healthbar.value <= 0 && enemyDead != true)
            {
                enemyDead = true;
                OnEnemyDeath();
            }
    }
    // runs when an enemy is killed
    void OnEnemyDeath()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().isBossFight = false;
        GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().bossSpawned = false;
        // ADDS TO THE STATS
        EndgameManager.kills += 1;
        
        int Sacrafices = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Sacrafices * 2;
        float maxHp = GameObject.FindWithTag("Player").GetComponent<PlayerController>().maxHp;
        float HP = GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP;

        GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().itemSpawnLoc = transform.position;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().coins += (3000);
        // bombs check
        if (numOfBombs > 0)
        {
            Destroy(gameObject);
            Instantiate(enemyExplosion, new Vector3(transform.position.x, (transform.position.y + (numOfBombs/4f) + 1f), 0), Quaternion.identity);
        }
        if (numOfBombs == 0)
        {
            Destroy(gameObject);
        }
        if ((HP + (maxHp * Sacrafices/100)) <= maxHp)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP += (maxHp * Sacrafices/100);
        } else if ((HP + (maxHp * Sacrafices/100)) > maxHp)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP = maxHp;
        }
        for (int i = 0; i < 12; i++) 
        {
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().itemSpawnLoc = new Vector3(Random.Range(transform.position.x-3f, transform.position.x+3f), transform.position.y, transform.position.z);
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnItems();
        }       
        // adds xp to the player
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().neededXp -= (50 * (1 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().KnowledgeCrystals)/2f));


        
    }

    // void OnTriggerEnter2D(Collider2D other)
    //     {
    //         if (other.gameObject.tag == "Enemy")
    //         {
    //             other.GetComponent<EnemyController>().healthbar.value -= ((numOfBombs * 5) + 5);
    //         }
    //     }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player" && Time.time > nextAttack && gameObject.tag == "Boss" && !GameObject.FindWithTag("Player").GetComponent<Animator>().GetBool("dead")) 
        {
            blockChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Forcefields;
            if (blockChance * 8 > 88)
            {
                blockChance = 11;
            }
            if (Random.Range(1, 101) < blockChance * 8)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP -= 0;
                // damage Text
                DamageIndicator.GetComponent<FloatingMessage>().damage = 0;
            }
            else 
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP -= damage * other.gameObject.GetComponent<PlayerController>().damageReduction;
                // damage Text
                DamageIndicator.GetComponent<FloatingMessage>().damage = damage * other.gameObject.GetComponent<PlayerController>().damageReduction;
            }
            
            DamageIndicator.GetComponent<FloatingMessage>().color = Color.white;
            Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 

            if (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Wreaths > 0)
            {
                healthbar.value -= (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Wreaths * 10);
                DamageIndicator.GetComponent<FloatingMessage>().damage = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Wreaths * 10;
                DamageIndicator.GetComponent<FloatingMessage>().color = Color.blue;
                Instantiate(DamageIndicator, transform.position, Quaternion.identity); 

            }

            nextAttack = Time.time + attackSpeed;

        }
        
    }
    
}

