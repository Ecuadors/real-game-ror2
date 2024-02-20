using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlashCooldownManager : MonoBehaviour
{
    [SerializeField] private Slider slashCooldown;
    public float slashIncrement = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        slashCooldown.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (slashCooldown.value > 0)
        {
            slashCooldown.value -= (slashIncrement);
        }
        else 
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().canSlash = true;
        }
    }

    public void ResetSlashCooldown()
    {
        slashCooldown.value = 100;
    }
    // description when clicked on the ability
    void OnMouseDown() 
    {
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.cyan;
        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerLevel >= 2)
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription("Slash around you, doing high damage!");
        }
        else 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription("Reach level 2 to unlock this ability.");
        }
    }
}