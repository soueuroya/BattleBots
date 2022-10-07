using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class MoveButtonScript : MonoBehaviour
{
    public MovementDirection dir;
    public bool isHeld;
    public List<KeyCode> keys;
    public List<KeyCode> constraints;
    private bool isGoing;
    private void Update()
    {
        isGoing = true;
        foreach (KeyCode keyCode in keys)
        {
            if (!Input.GetKey(keyCode))
            {
                if (dir == MovementDirection.FORWARD)
                {
                    Debug.Log("Not going forward because not pressing: " + keyCode);
                }
                isGoing = false;
                break;
            }
            /*else if (Input.GetKeyUp(key))
            {
                StopMove();
            }*/
        }
        foreach (KeyCode constraint in constraints)
        {
            if (Input.GetKey(constraint))
            {
                if (dir == MovementDirection.FORWARD)
                {
                    Debug.Log("Not going forward because constraint: " + constraint);
                }
                isGoing = false;
                break;
            }
        }

        if (!isGoing)
        {
            StopMove();
        }
        else
        {
            Debug.Log("going forward");
            StartMove();
        }

        if (isHeld)
        {
            ControlScript.Instance.Move(dir);
        }
    }
    private void OnMouseDown()
    {
        StartMove();
    }
    private void OnMouseUp()
    {
        StopMove();
    }
    private void StartMove()
    {
        isHeld = true;
    }
    private void StopMove()
    {
        if (isHeld)
        {
            isHeld = false;
            ControlScript.Instance.Stop(dir);
        }
    }
}
