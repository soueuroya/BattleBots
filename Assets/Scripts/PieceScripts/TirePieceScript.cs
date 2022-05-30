using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class TirePieceScript : PieceScript
{
    public TireSide TireSide { get { return tireSide; } private set { tireSide = value; } }
    [SerializeField] protected TireSide tireSide;
    [SerializeField] private bool tireInitialized;
    [SerializeField] private bool TIRELOCKUPDATE;

    private const PieceType typeToUse = PieceType.TIRE;
    private const float healthToUse = TIRE_PIECE_HEALTH;
    private const float massToUse = TIRE_PIECE_WEIGHT;

    protected void InitializeTire(bool forceUpdate = false)
    {
        if ((!tireInitialized || forceUpdate) && !TIRELOCKUPDATE)
        {
            base.InitializePiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    public void RotateForward(float speed)
    {
        if (speed == 0) // IF STOP, TURN OFF MOTOR
        {
            joint.useMotor = false;
        }
        else // IF MOVING, CREATE COPY OF JOINT MOTOR, APPLY CHANGES AND OVERWRITE ORIGINAL
        {
            JointMotor motor = joint.motor;
            motor.force = 100;
            motor.targetVelocity = speed;
            motor.freeSpin = false;

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
