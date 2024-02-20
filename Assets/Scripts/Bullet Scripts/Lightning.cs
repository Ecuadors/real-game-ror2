using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lightning : MonoBehaviour
{
    public Vector3 playerPos;

    public GameObject enemyExplosion;

    public List<GameObject> enemies = new List<GameObject>();
    public Animator anim;
    public float bulletDamage;
    private float rotation;
    private bool checkRotation = true;
    public Color redColor = Color.red;
    public Color whiteColor = Color.white;
    public GameObject DamageIndicator;
    private int pierce;
    public GameObject nearestEnemy;
    public AudioSource SFX;
    public GameObject oldNearestEnemy;

    private void Awake() 
    {
        SFX = GetComponent<AudioSource>();
        transform.Translate(0,-0.35f,0);

        if (checkRotation == true) {
            rotation = GameObject.FindWithTag("Player").GetComponent<ShootManager>().rotation;
            checkRotation = false;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        pierce = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().LightningHammers + 2;
        nearestEnemy = FindNearestEnemy();
        oldNearestEnemy = nearestEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        bulletDamage = GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletDamage * GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().LightningHammers/2f;
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        if (nearestEnemy != null && (nearestEnemy.transform.position-transform.position).magnitude < 10)
        {
            Vector2 direction = (Vector2)((nearestEnemy.transform.position - transform.position));
            direction.Normalize();

            rotation = (Mathf.Atan2(nearestEnemy.transform.position.y - transform.position.y, nearestEnemy.transform.position.x - transform.position.x)*180 / Mathf.PI);
            // proper rotation is a must!
            if (rotation < 90 && rotation > -90) 
            {
                transform.eulerAngles = new Vector3(0, 0, rotation);

            }
            else {
                transform.eulerAngles = new Vector3(0, 180, (180-rotation));
            }
        
            // Adds velocity to the bullet
            GetComponent<Rigidbody2D>().velocity = direction * 20f;
        }
        else
        {
            Destroy(gameObject);
        }

    
    }
    GameObject FindNearestEnemy()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] allBosses = GameObject.FindGameObjectsWithTag("Boss");

        nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in allEnemies)
        {
            if (!enemies.Contains(enemy))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        foreach (GameObject boss in allBosses)
        {
            if (!enemies.Contains(boss))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, boss.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = boss;
                }
            }
        }
        if (nearestEnemy == oldNearestEnemy)
        {
            nearestEnemy = null;
        }
        return nearestEnemy;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
        // CHECKS IF ENEMY
        // REWORKED SYSTEM BTW (it works!!!!!!!)
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            if (!enemies.Contains(other.gameObject))
            {
                pierce -= 1;
                if (pierce <= 0)
                {
                    Destroy(gameObject);
                }
                // the actual damage part
                if (other.GetComponent<EnemyController>() != null)
                {
                    enemyHit(other.gameObject);
                }
                if (other.GetComponent<FlyingEnemyScript>() != null)
                {
                    flyingEnemyHit(other.gameObject);
                }
                if (other.GetComponent<GolemController>() != null)
                {
                    golemHit(other.gameObject);
                }
                if (other.GetComponent<WormController>() != null)
                {
                    wormHit(other.gameObject);
                }
                if (other.GetComponent<BossController>() != null)
                {
                    bossHit(other.gameObject);
                }
                if (other.GetComponent<FinalBossScript>() != null)
                {
                    finalBossHit(other.gameObject);
                }
                enemies.Add(nearestEnemy);
                nearestEnemy = FindNearestEnemy(); 
                oldNearestEnemy = nearestEnemy;
            }
              
        }
    }    
            
    
    void enemyHit(GameObject other)
    { 
        // very condensed code
        other.GetComponent<EnemyController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
    }
    void flyingEnemyHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<FlyingEnemyScript>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
    }
    void golemHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<GolemController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
    }
    // worm
    void wormHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<WormController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
    }
    // boss
    void bossHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<BossController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
    }
    void finalBossHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<FinalBossScript>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
    }
    
    
}
