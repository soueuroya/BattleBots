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

    public HingeJoint HJoint { get { return hJoint; } private set { hJoint = value; } }
    [SerializeField] protected HingeJoint hJoint;

    public HingeJoint AxisJoint { get { return axisJoint; } private set { axisJoint = value; } }
    [SerializeField] protected HingeJoint axisJoint;

    [SerializeField] private const PieceType typeToUse = PieceType.TIRE;
    [SerializeField] private const float healthToUse = TIRE_PIECE_HEALTH;
    [SerializeField] private const float massToUse = TIRE_PIECE_WEIGHT;

    private Vector3 initialRot;

    protected void InitializeTire()
    {
        base.InitializePiece();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;

        //TODO SET JOINTS
        hJoint = GetComponent<HingeJoint>();
        axisJoint = hJoint.connectedBody.GetComponent<HingeJoint>();
        JointLimits otherJointLimits = new JointLimits();
        otherJointLimits.min = -TIRE_PIVOT_ANGLE;
        otherJointLimits.max = TIRE_PIVOT_ANGLE;
        axisJoint.limits = otherJointLimits;
        axisJoint.useLimits = true;
        //axisJoint.transform.parent.GetComponent<FramePieceScript>()
        FramePieceScript frameConnectedTo = axisJoint.transform.parent.GetComponent<FramePieceScript>();
        if (frameConnectedTo != null)
        {
            if (frameConnectedTo.rightAxis == axisJoint)
            {
                frameConnectedTo.rightTire = this;
            }
            else if (frameConnectedTo.leftAxis == axisJoint)
            {
                frameConnectedTo.leftTire = this; // change to list and then just add?
            }
            else
            {
                Debug.LogError("FOUND NO AXIS");
            }
        }
        else
        {
            Debug.LogError("FOUND NO FRAME");
        }
        initialRot = hJoint.connectedBody.transform.localRotation.eulerAngles;
    }

    public void RotateForward(float speed, bool pivot = false) // TODO - CREATE CO ROUTINE TO LERP ROTATION INSTEAD OF SNAPPING
    {
        //TIRE MOTOR
        JointMotor motor = hJoint.motor;
        motor.force = StaticHelper.TIRE_FORCE;
        motor.targetVelocity = speed;
        motor.freeSpin = false;

        if (speed == 0) // IF STOP, TURN OFF MOTOR
        {
            hJoint.useMotor = false;
            axisJoint.useMotor = false;
            axisJoint.transform.localRotation = Quaternion.Euler(initialRot); // RESET ROTATION
        }
        else // IF MOVING, CREATE COPY OF JOINT MOTOR, APPLY CHANGES AND OVERWRITE ORIGINAL
        {
            if (pivot)
            {
                JointMotor otherMotor = axisJoint.motor;
                otherMotor.force = StaticHelper.TIRE_AXIS_FORCE;
                otherMotor.freeSpin = false;
                if (tireHSide == TireHSide.RIGHT)
                {
                    if (tireVSide == TireVSide.UP)
                    {
                        otherMotor.targetVelocity = Mathf.Abs(speed);
                    }
                    else
                    {
                        otherMotor.targetVelocity = -Mathf.Abs(speed);
                    }
                }
                else
                {
                    if (tireVSide == TireVSide.UP)
                    {
                        otherMotor.targetVelocity = -Mathf.Abs(speed);
                    }
                    else
                    {
                        otherMotor.targetVelocity = Mathf.Abs(speed);
                    }
                }
                axisJoint.motor = otherMotor;
                axisJoint.useMotor = true;
            }
            else
            {
                hJoint.useMotor = false;
                axisJoint.useMotor = false;
                axisJoint.transform.localRotation = Quaternion.Euler(initialRot); // RESET ROTATION
            }
            hJoint.motor = motor; // overwritting here
            hJoint.useMotor = true; // turning on the new original
        }
    }

    protected override void DestroyPiece()
    {
        if (hJoint != null)
        {
            hJoint.breakForce = 0;
            Destroy(hJoint);
            robot.LosePiece(this);
        }
        /*if (joints != null)
        {
            foreach (HingeJoint hj in joints)
            {
                Debug.Log("Joint not null");
                hj.breakForce = 0;
                //Destroy(fj);
            }
            joints.Clear();
        }*/
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
