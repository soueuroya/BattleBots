using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillScript : MonoBehaviour
{
    [SerializeField] private Vector3 origin;
    [SerializeField] private float limit;
    [SerializeField] private float speed;
    [SerializeField] private float segmentSize;
    [SerializeField] private List<Rigidbody> segments;

    [SerializeField] private float speedStep = 0.0001f;
    [SerializeField] private float targetSpeed;
    [SerializeField] private int maxSpeed = 0;
    [SerializeField] private int minSpeed = 10;
    [SerializeField] private float swapSpeedTime = 10;

    [SerializeField] private float scale = 0;
    [SerializeField] private float frameReduction = 3;
    [SerializeField] private float frame = 0;
    [SerializeField] public bool isBeingControlled;
    [SerializeField] public TreadmillScript treadmillToControl;
    private Transform tr;

    void Start()
    {
        if (!isBeingControlled)
        {
            Invoke("SwapSpeed", swapSpeedTime);
        }
    }

    private void OnValidate()
    {
        segments = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        if (treadmillToControl != null)
        {
            treadmillToControl.isBeingControlled = true;
            treadmillToControl.enabled = false;
            segments.AddRange(new List<Rigidbody>(treadmillToControl.GetComponentsInChildren<Rigidbody>()));
        }
        tr = transform;
        if (segments.Count > 2) // SETTING THE MINIMUM TO 3 segments, which should be totally fine.
        {
            origin = segments[0].transform.localPosition;
            segmentSize = segments[1].transform.localPosition.z - segments[0].transform.localPosition.z;
            limit = segments[segments.Count - 1].transform.localPosition.z + segmentSize;
        }
    }

    void Update()
    {
        if (!isBeingControlled)
        {
            if (frame < frameReduction)
            {
                frame++;
            }
            else
            {
                frame = 0;
                if (targetSpeed < speed)
                {
                    if (targetSpeed < speed - speedStep)
                    {
                        speed -= speedStep;
                    }
                    else
                    {
                        speed = targetSpeed;
                    }
                }
                else if (targetSpeed > speed)
                {
                    if (targetSpeed > speed + speedStep)
                    {
                        speed += speedStep;
                    }
                    else
                    {
                        speed = targetSpeed;
                    }
                }

                foreach (Rigidbody segment in segments)
                {
                    segment.MovePosition(segment.transform.position + (tr.forward * speed));
                    if (segment.transform.localPosition.z > limit)
                    {
                        segment.transform.localPosition = origin + (tr.forward * (segment.transform.localPosition.z - limit));
                    }
                }
            }
        }
    }

    private void SwapSpeed()
    {
        targetSpeed = Random.Range(minSpeed, maxSpeed);
        targetSpeed /= 100;
        Debug.Log(targetSpeed);
        Invoke("SwapSpeed", swapSpeedTime);
    }
}
