using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopwatchScript : MonoBehaviour
{
    public string description = ("Stopwatch\nIncreases attack speed");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Stopwatch\nIncreases attack speed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Stopwatches += 1;
            GameObject.FindWithTag("Player").GetComponent<ShootManager>().attackSpeed *= 0.92f;

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

