using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealCooldownScript : MonoBehaviour
{
    [SerializeField] private Slider healCooldown;
    public float healIncrement = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        healCooldown.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (healCooldown.value > 0)
        {
            healCooldown.value -= (healIncrement);
        }
        else 
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().canHeal = true;
        }
    }

    public void ResetHealCooldown()
    {
        healCooldown.value = 100;
    }

    void OnMouseDown() 
    {
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.cyan;
        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerLevel >= 5)
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription("Heal yourself for some HP!");
        }
        else 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription("Reach level 5 to unlock this ability.");
        }
    }
}
