using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    private int staffs, vortexes, mentors, StunChance, bossDamage, energyDrinks, focusCrystals;
    public Color redColor = Color.red;
    public Color whiteColor = Color.white;
    public GameObject DamageIndicator;
    public Vector3 playerPos;
    public GameObject enemyExplosion;
    public GameObject lightningPrefab;
    private float slashDamage = 20f;
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
        
        slashDamage *= (1 + 1.2f * GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().BigBullets);
        transform.localScale += new Vector3(0.5f * GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().BigBullets,0.2f * GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().BigBullets ,0);
        SFX = GetComponent<AudioSource>();

        


    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
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
            slashDamage = other.gameObject.GetComponent<EnemyController>().healthbar.maxValue * 10;
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
            slashDamage *= (1f + focusCrystals/5f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<EnemyController>().healthbar.value == other.GetComponent<EnemyController>().healthbar.maxValue)
        {
            slashDamage *= (1 + mentors/2f);
        }
        other.GetComponent<EnemyController>().healthbar.value -= slashDamage;
        EndgameManager.damage += slashDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = slashDamage;
        
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
        
    
    }
    void flyingEnemyHit(GameObject other)
    {
        // VORTEX CHECK FIRST CUZ IT IS INSTA KILL
        if (Random.Range(0, 150) < vortexes)
        {
            slashDamage = other.gameObject.GetComponent<FlyingEnemyScript>().healthbar.maxValue;
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
            slashDamage *= (1f + focusCrystals/5f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<FlyingEnemyScript>().healthbar.value == other.GetComponent<FlyingEnemyScript>().healthbar.maxValue)
        {
            slashDamage *= (1 + mentors/2f);
        }
        other.GetComponent<FlyingEnemyScript>().healthbar.value -= slashDamage;
        EndgameManager.damage += slashDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = slashDamage;
        
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
        
        
    }
    void golemHit(GameObject other)
    {
        // VORTEX CHECK FIRST CUZ IT IS INSTA KILL
        if (Random.Range(0, 120) < vortexes)
        {
            slashDamage = other.gameObject.GetComponent<GolemController>().healthbar.maxValue * 10;
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
            slashDamage *= (1f + focusCrystals/5f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<GolemController>().healthbar.value == other.GetComponent<GolemController>().healthbar.maxValue)
        {
            slashDamage *= (1 + mentors/2f);
        }
        other.GetComponent<GolemController>().healthbar.value -= slashDamage;
        EndgameManager.damage += slashDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = slashDamage;
        
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
        
        
    }
    // worm
    void wormHit(GameObject other)
    {
        // VORTEX CHECK FIRST CUZ IT IS INSTA KILL
        if (Random.Range(0, 120) < vortexes)
        {
            slashDamage = other.gameObject.GetComponent<WormController>().healthbar.maxValue * 10;
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
            slashDamage *= (1f + focusCrystals/5f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<WormController>().healthbar.value == other.GetComponent<WormController>().healthbar.maxValue)
        {
            slashDamage *= (1 + mentors/2f);
        }
        other.GetComponent<WormController>().healthbar.value -= slashDamage;
        EndgameManager.damage += slashDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = slashDamage;
        
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
            slashDamage *= (1f + focusCrystals/5f);
            // other.GetComponent<BossController>().healthbar.value -= slashDamage * (1f + focusCrystals/5f);
            // DamageIndicator.GetComponent<FloatingMessage>().damage = slashDamage * (1f + focusCrystals/5f);
        }
        if (bossDamage > 0)
        {
            slashDamage *= (1f + bossDamage/2f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<BossController>().healthbar.value == other.GetComponent<BossController>().healthbar.maxValue)
        {
            slashDamage *= (1 + mentors/5f);
        }
        
        other.GetComponent<BossController>().healthbar.value -= slashDamage;
        EndgameManager.damage += slashDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = slashDamage;
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
            slashDamage *= (1f + focusCrystals/5f);
            // other.GetComponent<BossController>().healthbar.value -= slashDamage * (1f + focusCrystals/5f);
            // DamageIndicator.GetComponent<FloatingMessage>().damage = slashDamage * (1f + focusCrystals/5f);
        }
        if (bossDamage > 0)
        {
            slashDamage *= (1f + bossDamage/4f);
        }
        // CHECKS IF THEY HAVE ANY MENTORS AND IF ITS THE FIRST HIT
        if (mentors > 0 && other.GetComponent<FinalBossScript>().healthbar.value == other.GetComponent<FinalBossScript>().healthbar.maxValue)
        {
            slashDamage *= (1 + mentors/5f);
        }
        
        other.GetComponent<FinalBossScript>().healthbar.value -= slashDamage;
        EndgameManager.damage += slashDamage;
        // damage Text
        DamageIndicator.GetComponent<FloatingMessage>().damage = slashDamage;
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
        
    }
}