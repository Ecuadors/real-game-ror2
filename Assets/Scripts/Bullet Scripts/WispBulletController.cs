using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WispBulletController : MonoBehaviour
{
    public Vector3 playerPos;

    public GameObject enemyExplosion;

    public Animator anim;
    public float bulletDamage;
    private float rotation;
    private bool checkRotation = true;
    public Color redColor = Color.red;
    public Color whiteColor = Color.white;
    public GameObject DamageIndicator;
    
    public AudioSource SFX;

    private void Awake() 
    {
        bulletDamage = GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletDamage * GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Wisps + 16f;

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
        // proper rotation is a must!
        if (rotation < 90 && rotation > -90) 
        {
            transform.eulerAngles = new Vector3(0, 0, rotation);

        }
        else {
            transform.eulerAngles = new Vector3(0, 180, (180-rotation));
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
        // plays sound and animation when bullet hits the ground
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Walls") {
            GetComponent<Animator>().Play("Explosion");
            Destroy(GetComponent<BoxCollider2D>());
            transform.position += new Vector3(0, 1, 0);
            anim.transform.localScale += new Vector3(4, 4, 4);
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
        // CHECKS IF ENEMY
        // REWORKED SYSTEM BTW (it works!!!!!!!)
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
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
        }
    }    
            
    
    void enemyHit(GameObject other)
    { 
        // very condensed code
        other.GetComponent<EnemyController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // sets damage text to white
        DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
    void flyingEnemyHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<FlyingEnemyScript>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // sets damage text to white
        DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
    void golemHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<GolemController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // sets damage text to white
        DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
    // worm
    void wormHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<WormController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // sets damage text to white
        DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
    // boss
    void bossHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<BossController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // sets damage text to white
        DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
    void finalBossHit(GameObject other)
    {
        // very condensed code
        other.GetComponent<FinalBossScript>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // sets damage text to white
        DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
    
    
}
