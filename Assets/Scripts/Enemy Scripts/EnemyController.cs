using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{
    public GameObject enemyExplosion;

    public AudioSource EnemyAudio;
    public AudioClip enemyChomp;

    private int secondswhendied;
    private int counter;
    private bool itemSpawned = false;

    private float damage = 12.0f;
    public Vector3 playerPos;
    public Animator anim;
    private float scaleModifier;
    public float speedModifier;
    public float ogSpeedModifier;
    public Slider healthbar;
    public float attackSpeed = 0.8f;
    private float nextAttack;
    public bool slowedDown = false;
    public GameObject DamageIndicator;

    private int difficultyScalingMins;
    private int difficultyScalingSecs;
    
    private float difficultyScaling;

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
        newColor = new Color(Random.Range(0.6f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        GetComponent<SpriteRenderer>().color = newColor;
        //DIFIFUCLT SCCKKAZKLING!!! 
        difficultyScaling = GameObject.FindWithTag("Player").GetComponent<PlayerController>().difficultyScaling;

        scaleModifier = Random.Range(-0.6f, 1.0f);
        speedModifier = Random.Range(3f, 5f);
        ogSpeedModifier = speedModifier;

        if (difficultyScaling >= 6f)
        {
            difficultyScaling *= 2;
        }
        // scales with time
        damage = (difficultyScaling*5f + 12f);
        healthbar.maxValue = Random.Range((difficultyScaling*10f + 22f), (difficultyScaling*10f + 150f));
        // Debug.Log("Damage: " + damage + " health: " + healthbar.maxValue);
        gameObject.transform.localScale += new Vector3(scaleModifier, scaleModifier, scaleModifier);
        healthbar.value = healthbar.maxValue;
    }
    IEnumerator Die()
    {
        anim.SetBool("enemyDead", true);
        gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        numOfBombs = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Bombs;
        ItemSpawnChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().MagicRings;
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        if ((playerPos-transform.position).magnitude < 35 && GameObject.FindWithTag("Player").GetComponent<Animator>().GetBool("dead") == false) {
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
                
            }
        } else {
            anim.SetBool("running", false);
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
            itemSpawned = true;
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnItems();
        }
        if (numOfBombs == 0 && itemSpawned == false)
        {
            StartCoroutine(Die());
        }
        if (numOfBombs == 0 && itemSpawned == true)
        {
            Destroy(gameObject);
        }
        // adds xp to the player
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().neededXp -= (5 * (1 + (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().KnowledgeCrystals)/2f));
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player" && Time.time > nextAttack && gameObject.tag == "Enemy" && other.gameObject.GetComponent<Animator>().GetBool("dead") == false) 
        {
            blockChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Forcefields;
            if (blockChance * 8 > 88)
            {
                blockChance = 11;
            }
            if (Random.Range(0, 100) < blockChance * 8)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP -= 0;
                // damage Text
                DamageIndicator.GetComponent<FloatingMessage>().damage = 0;
            }
            else 
            {
                if (StatsManager.doSFX == true)
                {
                    EnemyAudio.pitch = 1.2f;
                    EnemyAudio.PlayOneShot(enemyChomp, (StatsManager.Volume/166f));
                }
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
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "rope")
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "rope")
        {
            GetComponent<Rigidbody2D>().gravityScale = 2; //to asign

        }
    }

    
}
