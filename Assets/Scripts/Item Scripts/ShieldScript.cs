using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public string description = ("Shield\nReduces damage taken.");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Shield\nReduces damage taken.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Shields += 1;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().damageReduction *= 0.8f;

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

