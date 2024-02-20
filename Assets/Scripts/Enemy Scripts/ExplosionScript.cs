using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public int numOfBombs;
    public Animator anim;

    private bool isStaff = false;

    // Start is called before the first frame update
    void Start()
    {
        isStaff = GameObject.FindWithTag("Player").GetComponent<PlayerController>().isStaff;
        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().AngelPopped == true)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().AngelPopped = false;
            numOfBombs = 1000;
            anim.transform.localScale += new Vector3(18, 18, 18);

        }
        else if (isStaff == true)
        {
            numOfBombs = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Staffs/2;
            anim.transform.localScale += new Vector3(numOfBombs+1, numOfBombs+1, numOfBombs+1);
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().isStaff = false;
        }
        else
        {
            numOfBombs = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Bombs;
            anim.transform.localScale += new Vector3(numOfBombs+1, numOfBombs+1, numOfBombs+1);
        }
        GetComponent<Animator>().Play("Explosion");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (other.GetComponent<EnemyController>() != null)
                {
                    other.GetComponent<EnemyController>().healthbar.value -= ((numOfBombs * 5) + 5);
                }
                if (other.GetComponent<FlyingEnemyScript>() != null)
                {
                    other.GetComponent<FlyingEnemyScript>().healthbar.value -= ((numOfBombs * 5) + 5);
                }
                if (other.GetComponent<GolemController>() != null)
                {
                    other.GetComponent<GolemController>().healthbar.value -= ((numOfBombs * 5) + 5);
                }
                if (other.GetComponent<WormController>() != null)
                {
                    other.GetComponent<WormController>().healthbar.value -= ((numOfBombs * 5) + 5);
                }
            }
            if (other.gameObject.tag == "Boss")
            {
                if (numOfBombs == 1000)
                {
                    numOfBombs = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Bombs;
    
                }
                anim.transform.localScale += new Vector3(numOfBombs+1, numOfBombs+1, numOfBombs+1);
                if (other.GetComponent<BossController>() != null)
                {
                    other.GetComponent<BossController>().healthbar.value -= ((numOfBombs * 5) + 5);
                }
                else 
                {
                    other.GetComponent<FinalBossScript>().healthbar.value -= ((numOfBombs * 5) + 5);
                }
            } 
        }
}
