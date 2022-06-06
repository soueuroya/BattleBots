using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomperScript : MonoBehaviour
{
    public Vector3 origin;
    public Transform tr;
    public Rigidbody rb;
    public float speed;
    public bool active;

    void Start()
    {
        tr = transform;
        origin = tr.position;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!active)
        {
            if (tr.position.y >= origin.y)
            {
                Release();
            }
        }
        if (!rb.useGravity && rb.velocity.y < speed)
        {
            rb.velocity = Vector3.up * speed;
        }
    }

    void Reset()
    {
        speed = 3;
        tr = transform;
        origin = tr.position;
        rb = GetComponent<Rigidbody>();
    }

    public void Release()
    {
        active = true;
        rb.useGravity = true;
        Invoke("GoUp", 2f);
    }

    public void GoUp()
    {
        active = false;
        rb.useGravity = false;
        rb.velocity = Vector3.up * speed;
    }
}
