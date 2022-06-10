using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomperScript : MonoBehaviour
{
    public Vector3 origin;
    public Transform tr;
    public Rigidbody rb;

    private void OnValidate()
    {
        tr = transform;
        origin = tr.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.isKinematic)
        {
            if (tr.position.y >= origin.y)
            {
                Release();
            }
            rb.MovePosition(tr.position + origin * Time.deltaTime/3);
        }
    }

    void Reset()
    {
        tr = transform;
        origin = tr.position;
        rb = GetComponent<Rigidbody>();
    }

    public void Release()
    {
        rb.isKinematic = false;
        CancelInvoke("GoUp");
        Invoke("GoUp", 3f);
    }

    public void GoUp()
    {
        rb.isKinematic = true;
    }
}
