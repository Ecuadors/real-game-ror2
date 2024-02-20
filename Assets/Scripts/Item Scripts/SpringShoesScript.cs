using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringShoesScript : MonoBehaviour
{
    public string description = ("Springers\nIncreases jump height!");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Springers\nIncreases jump height!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Springers += 1;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().jumpHeight += 4;

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
