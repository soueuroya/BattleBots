using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform tr;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        offset = tr.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        tr.position = offset + target.position;
    }
}
