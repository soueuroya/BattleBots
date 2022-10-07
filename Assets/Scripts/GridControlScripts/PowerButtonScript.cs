using UnityEngine;
using static StaticHelper;

public class PowerButtonScript : MonoBehaviour
{
    public PowerType powerType;
    public bool isHeld;

    private void OnMouseDown()
    {
        isHeld = true;
        ControlScript.Instance.controllables[ControlScript.Instance.currentControllable].StartPower(powerType);
    }
    private void OnMouseUp()
    {
        isHeld = false;
        ControlScript.Instance.controllables[ControlScript.Instance.currentControllable].StopPower(powerType);
    }
    private void Update()
    {
    }
}
