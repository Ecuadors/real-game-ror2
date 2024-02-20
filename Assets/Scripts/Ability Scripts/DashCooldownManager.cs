using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownManager : MonoBehaviour
{
    [SerializeField] private Slider dashCooldown;
    public float dashIncrement = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        dashCooldown.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCooldown.value > 0)
        {
            dashCooldown.value -= (dashIncrement);
        }
        else 
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().canDash = true;
        }
    }

    public void ResetDashCooldown()
    {
        dashCooldown.value = 100;
    }
    // description when clicked on the ability
    void OnMouseDown() 
    {
        GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ItemInfoText.color = Color.cyan;
        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerLevel >= 3)
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription("Thrust yourself forward with a dash!");
        }
        else 
        {
            GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().ShowItemDescription("Reach level 3 to unlock this ability.");
        }
    }
}
