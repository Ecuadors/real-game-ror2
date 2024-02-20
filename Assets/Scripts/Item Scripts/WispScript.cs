using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispScript : MonoBehaviour
{
    public string description = ("Worthy Wisp\nSummons a wisp to aid you in battle.");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Worthy Wisp\nSummons a wisp to aid you in battle.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Wisps += 1;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().activateWisp = true;

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
