using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public string description = ("An Apple\nIncreases HP and regen.");

    // Start is called before the first frame update
    void Start()
    {
        description = ("An Apple\nIncreases HP and regen.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Apple += 1;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().HpRegen += 0.001f;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().maxHp += 10;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().HP += 10;
            
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
