using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSizeScript : MonoBehaviour
{
    private int numMagnets; 
    private Rigidbody2D _rigidbody;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(0, 3f);

    }

    // Update is called once per frame
    void Update()
    {    
        numMagnets = GameObject.FindWithTag("ItemManager").GetComponent<ItemsManager>().Magnets;
        // checks for magnets
        GetComponent<BoxCollider2D>().size = new Vector2((2.7f + numMagnets), (2.7f + numMagnets));
        
    }
}
