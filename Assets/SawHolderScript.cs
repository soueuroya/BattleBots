using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawHolderScript : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private Transform limit;
    [SerializeField] private float speed;
    [SerializeField] private bool isForward;
    [SerializeField] private float delay;
    [SerializeField] private float timer;
    private Transform tr;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody>();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                isActive = true;
            }
        }
        else
        {
            rb.MovePosition(tr.position + tr.forward * speed);
        }
    }
}
