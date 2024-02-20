using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowledgeCrystalScript : MonoBehaviour
{
    public string description = ("Knowledge Crystal\nIncreases XP rates.");

    // Start is called before the first frame update
    void Start()
    {
        description = ("Knowledge Crystal\nIncreases XP rates.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().KnowledgeCrystals += 1;
            
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.white;
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription(description);
            
            Destroy(gameObject);
        }
    }

    void OnMouseDown() 
    {
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.white;
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription(description);
    }
}

