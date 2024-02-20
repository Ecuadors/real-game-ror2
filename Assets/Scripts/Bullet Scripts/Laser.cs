using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float rotation = 0.0f;
    public LayerMask layersToHit;
    SpriteRenderer SpriteRenderer;

    private bool isReal = false;
    private bool rotate = true;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.color = transform.parent.gameObject.GetComponent<GolemController>().newColor;
        StartCoroutine(flashingLaser());

    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            rotation = (Mathf.Atan2(GameObject.FindWithTag("Player").transform.position.y - transform.position.y, GameObject.FindWithTag("Player").transform.position.x - transform.position.x)*180 / Mathf.PI);
            // rotates it properly
    
            transform.eulerAngles = new Vector3(0, 0, rotation);
        }
        // makes the laser beeeeehind the golem
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 50f, layersToHit);
        if (hit.collider == null)
        {
            transform.localScale = new Vector3(5f, transform.localScale.y, 1);
            return;
        }
        transform.localScale = new Vector3(hit.distance/(4.9f * (GetComponentInParent<GolemController>().scaleModifier + 4)), transform.localScale.y, 1);
        if (hit.collider.tag == "Player" && isReal)
        {
            isReal = false;
            hit.collider.GetComponent<PlayerController>().HP -= 10f;
        }
    }
    
    IEnumerator doDamage()
    {
        SpriteRenderer.color = Color.red;

        isReal = true;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ToNoneAlpha(0.2f));
        Destroy(gameObject);
    }
    IEnumerator flashingLaser()
    {
        // fadeIn = StartCoroutine(ToFullAlpha(0.2f));
        // fadeOut = StartCoroutine(ToNoneAlpha(0.2f)); 

        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(ToFullAlpha(0.2f));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(ToNoneAlpha(0.2f)); 
        }
        rotate = false;
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(ToFullAlpha(0.2f));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(ToNoneAlpha(0.2f)); 
        }
        StartCoroutine(doDamage());
        
    }
    IEnumerator ToFullAlpha(float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / duration);
            SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 1f);
    }
    IEnumerator ToNoneAlpha(float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / duration);
            SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 1f);
    }
}
