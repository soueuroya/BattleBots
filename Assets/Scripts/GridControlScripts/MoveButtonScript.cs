using UnityEngine;
using static StaticHelper;

public class MoveButtonScript : MonoBehaviour
{
    public MovementDirection dir;
    public bool isHeld;
    public KeyCode key;
    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            StartMove();
        }
        else if (Input.GetKeyUp(key))
        {
            StopMove();
        }

        if (isHeld)
        {
            RobotControlScript.Instance.Move(dir);
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
        isHeld = false;
        RobotControlScript.Instance.Stop();
    }
}
