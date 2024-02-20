using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCont : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 1, -10f);
    private float smoothTime = 0.08f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().orthographicSize = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("=") && GetComponent<Camera>().orthographicSize >= 5.0f) {
            GetComponent<Camera>().orthographicSize -= 0.02f;
        }
        if (Input.GetKey("-") && GetComponent<Camera>().orthographicSize <= 15.0f) {
            GetComponent<Camera>().orthographicSize += 0.02f;
        }
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        // transform.position = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        // transform.position = new Vector3(transform.position.x, transform.position.y + 1, -10);
    }
}