using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorScript : MonoBehaviour
{
    public Vector3 origin; // where the door starts
    public Vector3 target; // where the door should go
    public Vector3 difference; // where the door should go
    public bool open; // is open or not
    public bool moving; // if the door is moving or not
    public Transform tr; // cached transform
    public float timer; // timer for opening and closing door
    public float delay; // time to wait between opening and closing
    public int loops; // how many moviments should we segregate the animation
    public int index; // which position the door should be
    public bool active; // if trapdoor is already in action

    void Start()
    {
        tr = transform;
        origin = tr.position;
        target = origin + (tr.right * 3 * tr.lossyScale.x); // set the target to the end of the trapdoor ( the size of the trapdoor is 3 units, so we multiply by it's real size in the world to get the exat position it should go.)
    }

    private void Reset()
    {
        delay = 0.02f;
        loops = 40;
        tr = transform;
        origin = tr.position;
        target = origin + (tr.right * 3 * tr.lossyScale.x);
    }

    private void Update()
    {
        if (active)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                if (!moving)
                {
                    moving = true;
                }
                Move();
            }
        }
    }

    public void Activate()
    { 
        if (!active && !moving)
        {
            active = true;
        }
    }

    public void Move()
    {
        if (index < loops)
        {
            index++;
            if (open)
            {
                difference = target - origin;
                tr.position = origin + (difference / loops) * (loops - index);
            }
            else
            {
                difference = target - origin;
                tr.position = origin + (difference / loops) * index;
            }
        }
        else
        {
            index = 0;
            GoToTarget();
        }
    }

    public void GoToTarget()
    {
        if (open)
        {
            tr.position = origin;
        }
        else
        {
            tr.position = target;
        }
        open = !open;
        moving = false;
        active = false;
    }
}
