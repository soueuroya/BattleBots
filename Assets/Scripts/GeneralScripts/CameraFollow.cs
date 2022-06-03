using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform tr;
    public Vector3 offset;
    void Start()
    {
        tr = transform;
        offset = tr.position - target.position;
    }
    void Update()
    {
        tr.position = offset + RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].transform.GetChild(0).transform.position;
    }
}
