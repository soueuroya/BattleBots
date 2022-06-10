using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggleScript : MonoBehaviour
{
    public bool isActive = false;
    public Vector3 target;
    public Vector3 origing;
    private Transform tr;
    private void Start()
    {
        tr = transform;
        origing = transform.localPosition;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isActive)
            {
                isActive = false;
                tr.localPosition = origing;
            }
            else
            {
                isActive=true;
                tr.localPosition = target;
            }
        }
    }
}
