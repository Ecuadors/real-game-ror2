using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperScript : MonoBehaviour
{
    public string description = ("Hot Chili Pepper\nLowers ability cooldowns!");

    // Start is called before the first frame update
    void Start()
    {
        description = ("Hot Chili Pepper\nLowers ability cooldowns!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().HotPeppers += 1;
            GameObject.FindWithTag("CooldownManager").GetComponent<SlashCooldownManager>().slashIncrement += 0.015f;
            GameObject.FindWithTag("CooldownManager").GetComponent<DashCooldownManager>().dashIncrement += 0.01f;
            GameObject.FindWithTag("CooldownManager").GetComponent<HealCooldownScript>().healIncrement += 0.005f;

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
