using UnityEngine;
using static StaticHelper;

public class CombatPieceScript : PieceScript
{
    [SerializeField] protected OutDirection direction;
    [SerializeField] protected ParticleSystem partSyst;
    [SerializeField] protected float charge;
    [SerializeField] protected float maxCharge;

    [SerializeField] protected FixedJoint[] joints;
    [SerializeField] protected FixedJoint joint;

    [SerializeField] private bool combatPieceInitialized;
    [SerializeField] private bool COMBATPIECELOCKUPDATE;

    protected void InitializeCombatPiece(bool forceUpdate = false)
    {
        if ((!combatPieceInitialized || forceUpdate) && !COMBATPIECELOCKUPDATE)
        {
            base.InitializePiece(forceUpdate);
            //TODO SET PARTICLES
            partSyst = GetComponent<ParticleSystem>();
            if (partSyst == null)
            {
                //partSyst = Resources.Load<ParticleSystem>("ParticleComponentHolders/AcidPiece");
                //partSyst = gameObject.AddComponent(typeof (ParticleSystem), Resources.Load<ParticleSystem>("ParticleComponentHolders/AcidPiece"));
            }

            //TODO SET JOINTS
            joint = GetComponent<FixedJoint>();
            joints = GetComponents<FixedJoint>();
        }
    }

    public void Activate()
    {
        if (partSyst != null)
        {
            partSyst.Play();
        }
    }

    public void Deactivate()
    {
        if (partSyst != null)
        {
            partSyst.Stop();
        }
    }

    private void Awake()
    {
        InitializeCombatPiece();
    }

    private void Start()
    {
        InitializeCombatPiece();
    }

    private void Reset()
    {
        InitializeCombatPiece();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeCombatPiece(true);
    }
}
