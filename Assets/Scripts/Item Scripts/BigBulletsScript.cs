using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBulletsScript : MonoBehaviour
{
    public string description = ("Magical Mushrooms\nEnhances your magic.");

    // Start is called before the first frame update
    void Start()
    {
        description = ("Magical Mushrooms\nEnhances your magic.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().BigBullets += 1;
            GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletDamage *= 1.2f;
            GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletSize += 0.5f;

            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.green;
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription(description);

            Destroy(gameObject);
        }
    }

    void OnMouseDown() 
    {
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.green;
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription(description);
    }
}
