using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class TirePieceScript : PieceScript
{
    public TireHSide TireHSide { get { return tireHSide; } private set { tireHSide = value; } }
    [SerializeField] protected TireHSide tireHSide;
    public TireVSide TireVSide { get { return tireVSide; } private set { tireVSide = value; } }
    [SerializeField] protected TireVSide tireVSide;
    public TireOrientation TireOrientation { get { return tireOrientation; } private set { tireOrientation = value; } }
    [SerializeField] protected TireOrientation tireOrientation;

    [SerializeField] private bool tireInitialized;
    [SerializeField] private bool TIRELOCKUPDATE;

    [SerializeField] protected HingeJoint[] joints;
    [SerializeField] protected HingeJoint joint;

    private const PieceType typeToUse = PieceType.TIRE;
    private const float healthToUse = TIRE_PIECE_HEALTH;
    private const float massToUse = TIRE_PIECE_WEIGHT;

    private Vector3 initialRot;

    protected void InitializeTire(bool forceUpdate = false)
    {
        if ((!tireInitialized || forceUpdate) && !TIRELOCKUPDATE)
        {
            base.InitializePiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;

            //TODO SET JOINTS
            joint = GetComponent<HingeJoint>();
            joints = GetComponents<HingeJoint>();

            initialRot = joint.connectedBody.transform.localRotation.eulerAngles;
        }
    }

    public void RotateForward(float speed, bool pivot = false)
    {
        if (speed == 0) // IF STOP, TURN OFF MOTOR
        {
            joint.useMotor = false;
            joint.connectedBody.transform.localRotation = Quaternion.Euler(initialRot);
        }
        else // IF MOVING, CREATE COPY OF JOINT MOTOR, APPLY CHANGES AND OVERWRITE ORIGINAL
        {
            JointMotor motor = joint.motor;
            motor.force = 100;
            motor.targetVelocity = speed;
            motor.freeSpin = false;

            if (pivot)
            {
                if (tireHSide == TireHSide.RIGHT)
                {
                    if (tireVSide == TireVSide.UP)
                    {
                        joint.connectedBody.transform.localRotation = Quaternion.Euler(initialRot + (Vector3.up * -6));
                    }
                    else
                    {
                        joint.connectedBody.transform.localRotation = Quaternion.Euler(initialRot + (Vector3.up * 6));
                    }
                }
                else
                {
                    if (tireVSide == TireVSide.UP)
                    {
                        joint.connectedBody.transform.localRotation = Quaternion.Euler(initialRot + (Vector3.up * 6));
                    }
                    else
                    {
                        joint.connectedBody.transform.localRotation = Quaternion.Euler(initialRot + (Vector3.up * -6));
                    }
                }
            }
            else
            {
                joint.connectedBody.transform.localRotation = Quaternion.Euler(initialRot);
            }

            joint.motor = motor; // overwritting here
            joint.useMotor = true; // turning on the new original
        }
    }
    private void Awake()
    {
        InitializeTire();
    }

    private void Start()
    {
        InitializeTire();
    }

    private void Reset()
    {
        InitializeTire();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeTire();
    }
}
