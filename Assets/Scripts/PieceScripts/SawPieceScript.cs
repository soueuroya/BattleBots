using UnityEngine;
using static StaticHelper;

public class SawPieceScript : CombatPieceScript
{
    private const PieceType typeToUse = PieceType.SAW;
    private const float healthToUse = SAW_PIECE_HEALTH;
    private const float massToUse = SAW_PIECE_WEIGHT;
    [SerializeField] private HingeJoint joint;
    [SerializeField] private MeshCollider collider;

    protected void InitializeShock()
    {
        base.InitializeCombatPiece();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;
    }

    public override void Activate()
    {
        if (joint != null)
        {
            JointMotor motor = joint.motor;
            motor.force = StaticHelper.SAW_FORCE;
            motor.targetVelocity = SAW_SPEED;
            motor.freeSpin = false;
            joint.motor = motor;
            joint.useMotor = true;
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

    public override void Deactivate()
    {
        if (joint != null)
        {
            JointMotor motor = joint.motor;
            motor.force = StaticHelper.SAW_FORCE;
            motor.targetVelocity = 0;
            motor.freeSpin = false;
            joint.motor = motor;
            //joint.useMotor = false;
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    private void Start()
    {
        InitializeShock();
    }

    private void Reset()
    {
        InitializeShock();
    }
}
