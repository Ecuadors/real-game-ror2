using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShoesScript : MonoBehaviour
{
    public string description = ("Flying Shoes\nJump an extra time!");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Flying Shoes\nJump an extra time!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FlyingShoes += 1;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().numJumps = (GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().FlyingShoes + 1);

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
