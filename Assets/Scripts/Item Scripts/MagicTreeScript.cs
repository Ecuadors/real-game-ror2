using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTreeScript : MonoBehaviour
{
    public string description = ("Magic Tree\nAllows overhealing.");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Magic Tree\nAllows overhealing.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().MagicTrees += 1;

            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.yellow;
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription(description);
            
            Destroy(gameObject);
        }
    }

    void OnMouseDown() 
    {
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.yellow;
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription(description);
    }
}