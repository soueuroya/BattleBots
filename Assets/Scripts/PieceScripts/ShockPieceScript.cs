using UnityEngine;
using static StaticHelper;

public class ShockPieceScript : CombatPieceScript
{
    [SerializeField] private bool shockInitialized;
    [SerializeField] private bool SHOCKLOCKUPDATE;

    private const PieceType typeToUse = PieceType.SHOCK;
    private const float healthToUse = SHOCK_PIECE_HEALTH;
    private const float massToUse = SHOCK_PIECE_WEIGHT;

    protected void InitializeShock(bool forceUpdate = false)
    {
        if ((!shockInitialized || forceUpdate) && !SHOCKLOCKUPDATE)
        {
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            tr = GetComponent<Transform>();

            //SET RIGIDBODY
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
            rb.mass = massToUse;

            //SET MESHCOLLIDER
            MeshCollider mc = GetComponent<MeshCollider>();
            if (mc == null)
            {
                mc = gameObject.AddComponent<MeshCollider>();
            }
            mc.convex = true;

            //TODO SET JOINTS
            joint = GetComponent<HingeJoint>();
            joints = GetComponents<HingeJoint>();

            //TODO SET PARTICLES
            partSyst = GetComponent<ParticleSystem>();
            if (partSyst == null)
            {
                //partSyst = Resources.Load<ParticleSystem>("ParticleComponentHolders/AcidPiece");
                //partSyst = gameObject.AddComponent(typeof (ParticleSystem), Resources.Load<ParticleSystem>("ParticleComponentHolders/AcidPiece"));
            }
        }
    }

    private void Awake()
    {
        InitializeShock();
    }

    private void Start()
    {
        InitializeShock();
    }

    private void Reset()
    {
        InitializeShock();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeShock(true);
    }
}
