using UnityEngine;
using static StaticHelper;

public class AcidPieceScript : CombatPieceScript
{
    [SerializeField] private bool acidInitialized;
    [SerializeField] private bool ACIDLOCKUPDATE;

    private const PieceType typeToUse = PieceType.ACID;
    private const float healthToUse = ACID_PIECE_HEALTH;
    private const float massToUse = ACID_PIECE_WEIGHT;


    protected void InitializeAcid(bool forceUpdate = false)
    {
        if ((!acidInitialized || forceUpdate) && !ACIDLOCKUPDATE)
        {
            acidInitialized = true;
            base.InitializeCombatPiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    /*
    protected void InitializeAcid(bool forceUpdate = false)
    {
        if ((!acidInitialized || forceUpdate) && !ACIDLOCKUPDATE)
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
    */

    private void Start()
    {
        acidInitialized = false;
        InitializeAcid(true);
    }

    private void Reset()
    {
        InitializeAcid();
    }

    private void OnDrawGizmosSelected()
    {
        acidInitialized = false;
        InitializeAcid(true);
    }
}
