using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawHolderScript : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private Transform limit;
    [SerializeField] private Transform origin;
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

    void FixedUpdate()
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
            rb.MovePosition(tr.position + tr.forward * speed * Time.deltaTime);
            if (isForward)
            {
                if (tr.localPosition.z > limit.localPosition.z)
                {
                    isActive = false;
                    isForward = false;
                    speed *= -1;
                }
            }
            else
            {
                if (tr.localPosition.z <= origin.localPosition.z)
                {
                    isActive = false;
                    isForward = true;
                    speed *= -1;
                }
            }
        }
    }
}
