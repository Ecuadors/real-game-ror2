using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpBullets : MonoBehaviour
{
    public string description = ("Improved Wand\nProjectiles fly through more enemies.");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Improved Wand\nProjectiles fly through more enemies.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().SharpBullets += 1;
            GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletPierce += 1;
            GameObject.FindWithTag("Player").GetComponent<ShootManager>().bulletVelocity += 1.5f;

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
