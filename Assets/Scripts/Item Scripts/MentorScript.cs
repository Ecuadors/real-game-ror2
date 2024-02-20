using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorScript : MonoBehaviour
{
    public string description = ("Mentor\nIncreases damage on your first hit on an enemy.");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Mentor\nIncreases damage on your first hit on an enemy.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Mentors += 1;

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

