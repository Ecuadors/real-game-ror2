using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingMessage : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private TMP_Text _damageValue;

    public float InitialYVelocity = 7f;
    public float InitialXVelocity = 1f;
    public float LifeTime = 0.6f;
    public float damage = 10f;
    public Color color;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damageValue = GetComponentInChildren<TMP_Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.velocity = new Vector2(Random.Range(-InitialXVelocity, InitialXVelocity), InitialYVelocity);
        Destroy(gameObject, LifeTime);
    }

    // private void SetMessage(string msg)
    // {
    //     _damageValue.SetText(msg);
    // }
    // Update is called once per frame
    void Update()
    {
        _damageValue.SetText((Mathf.RoundToInt(damage)).ToString());
        _damageValue.color = color;
    }
}
