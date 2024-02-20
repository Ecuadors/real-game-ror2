using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpfulWispController : MonoBehaviour
{
    public GameObject wispBulletPrefab;
    private float attackSpeed;
    private float nextFire;
    private float rotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        attackSpeed = 20f/(GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Wisps + 4f);
        if (Time.time > nextFire)
        {
            shootBullet();
            nextFire = Time.time + attackSpeed;
        }
    }
    void shootBullet()
    {
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null && (nearestEnemy.transform.position-transform.position).magnitude < 35)
        {
            Vector2 direction = (Vector2)((nearestEnemy.transform.position - transform.position));
            direction.Normalize();

            rotation = (Mathf.Atan2(nearestEnemy.transform.position.y - transform.position.y, nearestEnemy.transform.position.x - transform.position.x)*180 / Mathf.PI);
            
            // Creates the bullet locally
            GameObject bullet = (GameObject)Instantiate(wispBulletPrefab, transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);

            // Adds velocity to the bullet
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 32;
        }
    
        
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        foreach (GameObject boss in bosses)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, boss.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = boss;
            }
        }

        return nearestEnemy;
    }
}
