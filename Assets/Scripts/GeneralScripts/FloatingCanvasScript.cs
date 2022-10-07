using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCanvasScript : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Transform tr;
    private Transform target;
    void Awake()
    {
        tr = transform;
        if (offset == null)
        { 
            offset = tr.position - tr.parent.position; 
        }
        target = tr.parent.transform;
        tr.SetParent(null);
    }

    private void Update()
    {
        if (target != null)
        {
            tr.position = target.position + offset;
        }
    }

}
