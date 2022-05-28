using UnityEngine;
using static TypeHelper;

public class MoveButtonScript : MonoBehaviour
{
    public MovementDirection dir;
    public bool isHeld;
    private void OnMouseDown()
    {
        isHeld = true;
    }
    private void OnMouseUp()
    {
        isHeld = false;
        RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].Stop();
    }
    private void Update()
    {
        if (isHeld)
        {
            RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].Move(dir);
        }
    }
}
