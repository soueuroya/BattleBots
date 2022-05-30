using UnityEngine;
using static StaticHelper;

public class PowerButtonScript : MonoBehaviour
{
    public PowerType powerType;
    public bool isHeld;

    private void OnMouseDown()
    {
        isHeld = true;
        Debug.Log("POWER activated: " + powerType.ToString());
        RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].StartPower(powerType);
    }
    private void OnMouseUp()
    {
        isHeld = false;
        Debug.Log("POWER deactivated: " + powerType.ToString());
        RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].StopPower(powerType);
    }
    private void Update()
    {
    }
}
