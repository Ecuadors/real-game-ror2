using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BulletController : MonoBehaviour
{
    public Vector3 playerPos;

    public GameObject enemyExplosion;
    public GameObject lightningPrefab;

    public Animator anim;
    public int bulletPierce;
    public float bulletDamage;
    public float bulletSize;
    public float bossDamage;
    private float rotation;
    private int mentors;
    private int vortexes;
    private int staffs;
    private bool checkRotation = true;
    public int fireballChance = 0;
    public int StunChance = 0;
    private int focusCrystals;
    private int energyDrinks;
    public Color redColor = Color.red;
    public Color whiteColor = Color.white;
    public GameObject DamageIndicator;
    
    public AudioSource SFX;

    private void Awake() 
    {
        
        staffs = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Staffs;
        vortexes = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Vortexes;
        mentors = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Mentors;
        StunChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().StunGuns;
        bossDamage = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Cauldrons;
        energyDrinks = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().EnergyDrinks;
        focusCrystals = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FocusCrystals;
        bulletPierce = GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletPierce;
        bulletDamage = GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletDamage;
        bulletSize = GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletSize;
        fireballChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Spellbooks;


        SFX = GetComponent<AudioSource>();
        transform.Translate(0,-0.35f,0);
        bulletSize = GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletSize;

        if (checkRotation == true) {
            rotation = GameObject.FindWithTag("Player").GetComponent<ShootManager>().rotation;
            checkRotation = false;
        }
        

        transform.localScale += new Vector3(bulletSize, bulletSize, bulletSize);

    }
    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP <= (GameObject.FindWithTag("Player").GetComponent<PlayerController>().maxHp * 0.2f) && energyDrinks > 0)
        {
            bulletDamage *= (1.25f + energyDrinks/4f);
        }
        // CHECKS IF ITS A CRIT fireball
        if (Random.Range(1, 101) < fireballChance * 8)
        {
            GetComponent<SpriteRenderer>().color = redColor; 
            bulletDamage *= 2f;
            bulletSize += 1.0f;
        }
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
        DestroyWhenOffScreen();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
        // plays sound and animation when bullet hits the ground
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Walls") {
            GetComponent<Animator>().Play("Explosion");
            // SFX.pitch = Random.Range(0.8f, 1.5f);
            // SFX.PlayOneShot(SFX.clip);
            Destroy(GetComponent<BoxCollider2D>());
            transform.position += new Vector3(0, 1, -5);
            anim.transform.localScale += new Vector3(4, 4, 4);
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        

        }
        // CHECKS IF ENEMY
        // REWORKED SYSTEM BTW (it works!!!!!!!) (edit 2/11/24 it sucks)
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
            if (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().LightningHammers > 0)
            {
                if (Random.Range(0, 9) < GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().LightningHammers)
                {
                    GameObject Lightnin = (GameObject)Instantiate(lightningPrefab, other.transform.position, Quaternion.identity);
                    Lightnin.GetComponent<Lightning>().enemies.Add(other.gameObject);

                }
            }
        }
    }    
            
    
    void enemyHit(GameObject other)
    {
        // VORTEX CHECK FIRST CUZ IT IS INSTA KILL
        if (Random.Range(0, 120) < vortexes)
        {
            bulletDamage = other.gameObject.GetComponent<EnemyController>().healthbar.maxValue * 10;
        }
        // STAFF CHECK FOR EXPLOSIONS BOOM BOOM HEHE
        if (staffs > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().isStaff = true;
            Instantiate(enemyExplosion, new Vector3(other.transform.position.x, (other.transform.position.y + (staffs/4f) + 1f), 0), Quaternion.identity);

        }
        // FOCUS CRYSTAL CHECK
        other.GetComponent<EnemyController>().isAcid = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Acid;
        // STUN GUN CHECK
        StunChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().StunGuns;
        // CHECKS IF THE ENEMY ITSELF IS CLOSE AND DOESNT CHANGE DAMAGE IN CASE OTHER ENEMEIS ARE NOT NEARBY
        if ((playerPos-transform.position).magnitude < 7 && focusCrystals > 0)
        {
            bulletDamage *= (1f + focusCrystals/5f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<EnemyController>().healthbar.value == other.GetComponent<EnemyController>().healthbar.maxValue)
        {
            bulletDamage *= (1 + mentors/2f);
        }
        other.GetComponent<EnemyController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        
        // checks if enemy gets stunned
        if (Random.Range(0, 90) < StunChance)
        {
            other.GetComponent<EnemyController>().speedModifier = 0;
        }
        


        // colors depending if crit or not Fireball
        if (GetComponent<SpriteRenderer>().color == redColor)
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = redColor;
        }
        else 
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        }
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        
        // checks if you have a ice spell
        if (other.GetComponent<EnemyController>().slowedDown == false && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells > 0)
        {   
            other.GetComponent<EnemyController>().speedModifier *= (8f/(GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells+9f));
            
            other.GetComponent<EnemyController>().slowedDown = true;
        }
        
        bulletPierce -= 1;
        if (bulletPierce == 0) {
            Destroy(gameObject);
            // GetComponent<Animator>().Play("Explosion");
            // Destroy(GetComponent<BoxCollider2D>());
            // transform.position += new Vector3(0, 1, 0);
            // anim.transform.localScale += new Vector3(2, 2, 2);
            // GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }
    void flyingEnemyHit(GameObject other)
    {
        // VORTEX CHECK FIRST CUZ IT IS INSTA KILL
        if (Random.Range(0, 150) < vortexes)
        {
            bulletDamage = other.gameObject.GetComponent<FlyingEnemyScript>().healthbar.maxValue;
        }
        // STAFF CHECK FOR EXPLOSIONS BOOM BOOM HEHE
        if (staffs > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().isStaff = true;
            Instantiate(enemyExplosion, new Vector3(other.transform.position.x, (other.transform.position.y + (staffs/4f) + 1f), 0), Quaternion.identity);

        }
        // FOCUS CRYSTAL CHECK
        other.GetComponent<FlyingEnemyScript>().isAcid = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Acid;
        // STUN GUN CHECK
        StunChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().StunGuns;
        // CHECKS IF THE ENEMY ITSELF IS CLOSE AND DOESNT CHANGE DAMAGE IN CASE OTHER ENEMEIS ARE NOT NEARBY
        if ((playerPos-transform.position).magnitude < 7 && focusCrystals > 0)
        {
            bulletDamage *= (1f + focusCrystals/5f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<FlyingEnemyScript>().healthbar.value == other.GetComponent<FlyingEnemyScript>().healthbar.maxValue)
        {
            bulletDamage *= (1 + mentors/2f);
        }
        other.GetComponent<FlyingEnemyScript>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        
        // checks if enemy gets stunned
        if (Random.Range(0, 90) < StunChance)
        {
            other.GetComponent<FlyingEnemyScript>().speedModifier = 0;
        }
        


        // colors depending if crit or not Fireball
        if (GetComponent<SpriteRenderer>().color == redColor)
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = redColor;
        }
        else 
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        }
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        
        // checks if you have a ice spell
        if (other.GetComponent<FlyingEnemyScript>().slowedDown == false && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells > 0)
        {   
            other.GetComponent<FlyingEnemyScript>().speedModifier *= (8f/(GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells+9f));
            
            other.GetComponent<FlyingEnemyScript>().slowedDown = true;
        }
        
        bulletPierce -= 1;
        if (bulletPierce == 0) {
            Destroy(gameObject);
            // GetComponent<Animator>().Play("Explosion");
            // Destroy(GetComponent<BoxCollider2D>());
            // transform.position += new Vector3(0, 1, 0);
            // anim.transform.localScale += new Vector3(2, 2, 2);
            // GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }
    void golemHit(GameObject other)
    {
        // VORTEX CHECK FIRST CUZ IT IS INSTA KILL
        if (Random.Range(0, 120) < vortexes)
        {
            bulletDamage = other.gameObject.GetComponent<GolemController>().healthbar.maxValue * 10;
        }
        // STAFF CHECK FOR EXPLOSIONS BOOM BOOM HEHE
        if (staffs > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().isStaff = true;
            Instantiate(enemyExplosion, new Vector3(other.transform.position.x, (other.transform.position.y + (staffs/4f) + 1f), 0), Quaternion.identity);

        }
        // FOCUS CRYSTAL CHECK
        other.GetComponent<GolemController>().isAcid = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Acid;
        // STUN GUN CHECK
        StunChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().StunGuns;
        // CHECKS IF THE ENEMY ITSELF IS CLOSE AND DOESNT CHANGE DAMAGE IN CASE OTHER ENEMEIS ARE NOT NEARBY
        if ((playerPos-transform.position).magnitude < 7 && focusCrystals > 0)
        {
            bulletDamage *= (1f + focusCrystals/5f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<GolemController>().healthbar.value == other.GetComponent<GolemController>().healthbar.maxValue)
        {
            bulletDamage *= (1 + mentors/2f);
        }
        other.GetComponent<GolemController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        
        // checks if enemy gets stunned
        if (Random.Range(0, 90) < StunChance)
        {
            other.GetComponent<GolemController>().speedModifier = 0;
        }
        


        // colors depending if crit or not Fireball
        if (GetComponent<SpriteRenderer>().color == redColor)
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = redColor;
        }
        else 
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        }
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        
        // checks if you have a ice spell
        if (other.GetComponent<GolemController>().slowedDown == false && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells > 0)
        {   
            other.GetComponent<GolemController>().speedModifier *= (8f/(GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells+9f));
            
            other.GetComponent<GolemController>().slowedDown = true;
        }
        
        bulletPierce -= 1;
        if (bulletPierce == 0) {
            Destroy(gameObject);
            // GetComponent<Animator>().Play("Explosion");
            // Destroy(GetComponent<BoxCollider2D>());
            // transform.position += new Vector3(0, 1, 0);
            // anim.transform.localScale += new Vector3(2, 2, 2);
            // GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }
    // worm
    void wormHit(GameObject other)
    {
        // VORTEX CHECK FIRST CUZ IT IS INSTA KILL
        if (Random.Range(0, 120) < vortexes)
        {
            bulletDamage = other.gameObject.GetComponent<WormController>().healthbar.maxValue * 10;
        }
        // STAFF CHECK FOR EXPLOSIONS BOOM BOOM HEHE
        if (staffs > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().isStaff = true;
            Instantiate(enemyExplosion, new Vector3(other.transform.position.x, (other.transform.position.y + (staffs/4f) + 1f), 0), Quaternion.identity);

        }
        // FOCUS CRYSTAL CHECK
        other.GetComponent<WormController>().isAcid = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Acid;
        // STUN GUN CHECK
        StunChance = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().StunGuns;
        // CHECKS IF THE ENEMY ITSELF IS CLOSE AND DOESNT CHANGE DAMAGE IN CASE OTHER ENEMEIS ARE NOT NEARBY
        if ((playerPos-transform.position).magnitude < 7 && focusCrystals > 0)
        {
            bulletDamage *= (1f + focusCrystals/5f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<WormController>().healthbar.value == other.GetComponent<WormController>().healthbar.maxValue)
        {
            bulletDamage *= (1 + mentors/2f);
        }
        other.GetComponent<WormController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        
        // checks if enemy gets stunned
        if (Random.Range(0, 90) < StunChance)
        {
            other.GetComponent<WormController>().speedModifier = 0;
        }
        


        // colors depending if crit or not Fireball
        if (GetComponent<SpriteRenderer>().color == redColor)
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = redColor;
        }
        else 
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        }
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        
        // checks if you have a ice spell
        if (other.GetComponent<WormController>().slowedDown == false && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells > 0)
        {   
            other.GetComponent<WormController>().speedModifier *= (8f/(GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells+9f));
            
            other.GetComponent<WormController>().slowedDown = true;
        }
        
        bulletPierce -= 1;
        if (bulletPierce == 0) {
            Destroy(gameObject);
            // GetComponent<Animator>().Play("Explosion");
            // Destroy(GetComponent<BoxCollider2D>());
            // transform.position += new Vector3(0, 1, 0);
            // anim.transform.localScale += new Vector3(2, 2, 2);
            // GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }
    // boss
    void bossHit(GameObject other)
    {
        
        // STAFF CHECK FOR EXPLOSIONS BOOM BOOM HEHE
        if (staffs > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().isStaff = true;
            Instantiate(enemyExplosion, new Vector3(other.transform.position.x, (other.transform.position.y + (staffs/4f) + 1f), 0), Quaternion.identity);

        }
        // FOCUS CRYSTAL CHECK
        other.GetComponent<BossController>().isAcid = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Acid;
        // CHECKS IF THE ENEMY ITSELF IS CLOSE AND DOESNT CHANGE DAMAGE IN CASE OTHER ENEMEIS ARE NOT NEARBY
        if ((playerPos-transform.position).magnitude < 5 && focusCrystals > 0)
        {
            bulletDamage *= (1f + focusCrystals/5f);
            // other.GetComponent<BossController>().healthbar.value -= bulletDamage * (1f + focusCrystals/5f);
            // DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage * (1f + focusCrystals/5f);
        }
        if (bossDamage > 0)
        {
            bulletDamage *= (1f + bossDamage/2f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<BossController>().healthbar.value == other.GetComponent<BossController>().healthbar.maxValue)
        {
            bulletDamage *= (1 + mentors/5f);
        }
        
        other.GetComponent<BossController>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // checks if enemy gets stunned
        if (Random.Range(0, 100) < StunChance)
        {
            other.GetComponent<BossController>().speedModifier = 0;
        }


        // colors depending if crit or not
        if (GetComponent<SpriteRenderer>().color == redColor)
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = redColor;
        }
        else 
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        }
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        
        // checks if you have a ice spell
        if (other.GetComponent<BossController>().slowedDown == false && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells > 0)
        {                   
            other.GetComponent<BossController>().slowedDown = true;
        }
        
        bulletPierce -= 1;
        if (bulletPierce == 0) {
            Destroy(gameObject);
            // GetComponent<Animator>().Play("Explosion");
            // Destroy(GetComponent<BoxCollider2D>());
            // transform.position += new Vector3(0, 1, 0);
            // anim.transform.localScale += new Vector3(2, 2, 2);
            // GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }
    void finalBossHit(GameObject other)
    {
        // STAFF CHECK FOR EXPLOSIONS BOOM BOOM HEHE
        if (staffs > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().isStaff = true;
            Instantiate(enemyExplosion, new Vector3(other.transform.position.x, (other.transform.position.y + (staffs/4f) + 1f), 0), Quaternion.identity);

        }
        // FOCUS CRYSTAL CHECK
        other.GetComponent<FinalBossScript>().isAcid = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Acid;
        // CHECKS IF THE ENEMY ITSELF IS CLOSE AND DOESNT CHANGE DAMAGE IN CASE OTHER ENEMEIS ARE NOT NEARBY
        if ((playerPos-transform.position).magnitude < 5 && focusCrystals > 0)
        {
            bulletDamage *= (1f + focusCrystals/5f);
            // other.GetComponent<BossController>().healthbar.value -= bulletDamage * (1f + focusCrystals/5f);
            // DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage * (1f + focusCrystals/5f);
        }
        if (bossDamage > 0)
        {
            bulletDamage *= (1f + bossDamage/4f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<FinalBossScript>().healthbar.value == other.GetComponent<FinalBossScript>().healthbar.maxValue)
        {
            bulletDamage *= (1 + mentors/5f);
        }
        
        other.GetComponent<FinalBossScript>().healthbar.value -= bulletDamage;
        EndgameManager.damage += bulletDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = bulletDamage;
        // checks if enemy gets stunned
        if (Random.Range(0, 100) < StunChance)
        {
            other.GetComponent<FinalBossScript>().speedModifier = 0;
        }


        // colors depending if crit or not
        if (GetComponent<SpriteRenderer>().color == redColor)
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = redColor;
        }
        else 
        {
            DamageIndicator.GetComponent<FloatingMessage>().color = whiteColor;
        }
        // spawns the damage text
        Instantiate(DamageIndicator, other.transform.position, Quaternion.identity); 
        
        // checks if you have a ice spell
        if (other.GetComponent<FinalBossScript>().slowedDown == false && GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FrostSpells > 0)
        {                   
            other.GetComponent<FinalBossScript>().slowedDown = true;
        }
        
        bulletPierce -= 1;
        if (bulletPierce == 0) {
            Destroy(gameObject);
            // GetComponent<Animator>().Play("Explosion");
            // Destroy(GetComponent<BoxCollider2D>());
            // transform.position += new Vector3(0, 1, 0);
            // anim.transform.localScale += new Vector3(2, 2, 2);
            // GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }
    
    
    // destroys bullets when off screen
    private void DestroyWhenOffScreen() 
    {
        if (GameObject.FindWithTag("Player").transform.position.x - transform.position.x > 31f || GameObject.FindWithTag("Player").transform.position.x - transform.position.x < -31f)
        {
            Destroy(gameObject);
        }
    }
}
