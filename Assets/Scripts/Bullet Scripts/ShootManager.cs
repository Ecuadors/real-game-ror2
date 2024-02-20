using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootManager : MonoBehaviour
{
    public GameObject BulletPrefab;
    public AudioSource SFX;

    private Vector3 spawnLoc;
    public Vector3 mousePos = new Vector3(0, 0, 0);
    public float bulletVelocity = 32.0f;
    public float attackSpeed = 0.5f;
    private float nextFire;
    private float bulletAnimOver;
    public float rotation = 0.0f;
    public int bulletPierce = 1;
    public float bulletDamage = 10.0f;
    public float bulletSize = 0.5f;

    private float bulletAnimLength = 0.36f;

    public float timeSinceLastShot = 5f;
    public bool isShooting = false;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            anim.SetFloat("attackSpeedMultiplier", (0.5f/attackSpeed));
            if (isShooting && Time.time > bulletAnimOver)
            {
                isShooting = false; 
                bulletAnimOver = Time.time + bulletAnimLength;

            }

            timeSinceLastShot += Time.deltaTime;
            if (Input.GetMouseButton(0) && Time.time > nextFire && !anim.GetBool("dead") && !GetComponent<PlayerController>().isClimbing) 
            {
                if (StatsManager.doSFX == true)
                {
                    SFX.pitch = Random.Range(0.7f, 1.66f);  
                    SFX.volume = (StatsManager.Volume/500f);
                    SFX.PlayOneShot(SFX.clip);
                }
                shootBullet();
                if (GetComponent<PlayerController>().isSpedUp)
                {
                    GetComponent<PlayerController>().isSpedUp = false;
                    GetComponent<PlayerController>().speed /= (1f + GetComponent<PlayerController>().tempHats * 0.4f);
                }

                timeSinceLastShot = 0;
                nextFire = Time.time + attackSpeed;
            } 
        }
        
    }
    void shootBullet() 
    {
        isShooting = true;
        bulletAnimOver = Time.time + bulletAnimLength;

        EndgameManager.shots += 1;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();

        rotation = (Mathf.Atan2(worldMousePos.y - transform.position.y, worldMousePos.x - transform.position.x)*180 / Mathf.PI);
        
        // Creates the bullet locally
        // also spawns it up a bit so it spawns from the player's hand
        GameObject bullet = (GameObject)Instantiate(BulletPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z) + (Vector3)(direction * 0.5f), Quaternion.identity);

        // Adds velocity to the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletVelocity;
        // SHOOT ANIMATION
        anim.SetTrigger("Attack");
        if (transform.localScale.x == 6.5f && Input.mousePosition.x <= 956)
        {
            transform.localScale = new Vector3(-6.5f, transform.localScale.y, 6.5f);
        }
        if (transform.localScale.x == -6.5f && Input.mousePosition.x > 956)
        {
            transform.localScale = new Vector3(6.5f, transform.localScale.y, 6.5f);
        }
    }
    
}
