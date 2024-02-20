using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public Animator anim;
    
    public float damage = 50f;
    // private bool checkRotation = true;
    // public float rotation;
    public float difficultyScaling;

    public GameObject DamageIndicator;
    
    private void Awake() 
    {
        difficultyScaling = GameObject.FindWithTag("Player").GetComponent<PlayerController>().difficultyScaling;
        damage = (difficultyScaling*2) + 10;
        if (transform.localScale.x > 2)
        {
            damage += 50;
        }
        transform.Translate(0,-0.35f,0);
        // if (checkRotation == true) {
        //     if (finalBoss == false)
        //     {
        //         rotation = GameObject.FindWithTag("Boss").GetComponent<BossController>().rotation;

        //     }
        //     if (finalBoss == true)
        //     {
        //         rotation = GameObject.FindWithTag("Boss").GetComponent<FinalBossScript>().rotation;

        //     }
        //     checkRotation = false;
        // }
        
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        DestroyWhenOffScreen();
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
        // plays sound and animation when bullet hits the ground
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Walls") {
            GetComponent<Animator>().Play("Explosion");
            // SFX.pitch = Random.Range(0.8f, 1.5f);
            // SFX.PlayOneShot(SFX.clip);
            Destroy(GetComponent<BoxCollider2D>());
            transform.position += new Vector3(0, 1, 0);
            anim.transform.localScale += new Vector3(4, 4, 4);
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        

        }
        // CHECKS IF ENEMY
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().HP -= damage * GameObject.FindWithTag("Player").GetComponent<PlayerController>().damageReduction;

            // damage Text
            DamageIndicator.GetComponent<FloatingMessage>().damage = damage * GameObject.FindWithTag("Player").GetComponent<PlayerController>().damageReduction;
            


            // spawns the damage text
            Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
            
            
            Destroy(gameObject);
        }
    }
    
    
    
    // destroys bullets when off screen
    private void DestroyWhenOffScreen() 
    {
  
        if (GameObject.FindWithTag("Boss") != null)
        {
            if (GameObject.FindWithTag("Boss").transform.position.x - transform.position.x > 60f || GameObject.FindWithTag("Boss").transform.position.x - transform.position.x < -60f)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
