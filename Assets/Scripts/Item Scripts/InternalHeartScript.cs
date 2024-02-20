using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalHeartScript : MonoBehaviour
{
    public string description = ("External Heart\nPops on low HP and heals you.");
    // Start is called before the first frame update
    void Start()
    {
        description = ("External Heart\nPops on low HP and heals you.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().InternalHearts += 1;
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().HeartsCount += 1;

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