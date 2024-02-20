using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelScript : MonoBehaviour
{
    public string description = ("Angel\nCalls a blessing from above to grant an extra life.");
    // Start is called before the first frame update
    void Start()
    {
        description = ("Angel\nCalls a blessing from above to grant an extra life.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Angels += 1;
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().AngelCount += 1;

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
