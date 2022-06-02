using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    private Transform tr;
    private void Awake()
    {
        tr = transform;
    }
    public void UpdateHealth(float percentage)
    {
        tr.localScale = Vector3.up + Vector3.forward + Vector3.right * percentage;
    }
}
