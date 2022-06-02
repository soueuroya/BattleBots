using UnityEngine;
using static StaticHelper;

public class PowerButtonScript : MonoBehaviour
{
    public PowerType powerType;
    public bool isHeld;

    private void OnMouseDown()
    {
        isHeld = true;
        RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].StartPower(powerType);
    }
    private void OnMouseUp()
    {
        isHeld = false;
        RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].StopPower(powerType);
    }
    private void Update()
    {
    }
}
