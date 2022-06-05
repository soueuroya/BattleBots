using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class CombatPieceScript : FramePieceScript
{
    [SerializeField] protected OutDirection direction;
    [SerializeField] public ParticleSystem partSyst;
    [SerializeField] protected float charge;
    [SerializeField] protected float maxCharge;
    protected void InitializeCombatPiece()
    {
        base.InitializePiece();
        //TODO SET PARTICLES
        partSyst = GetComponent<ParticleSystem>();
        if (partSyst == null)
        {
            //partSyst = Resources.Load<ParticleSystem>("ParticleComponentHolders/AcidPiece");
            //partSyst = gameObject.AddComponent(typeof (ParticleSystem), Resources.Load<ParticleSystem>("ParticleComponentHolders/AcidPiece"));
        }
    }

    public virtual void  Activate()
    {
        if (partSyst != null)
        {
            partSyst.Play(); // TODO, receive % of force, so when attacking we can increase to the maximum, and as the fuel goes down, the attack goes down. Also when the piece is destroyed
        }
    }

    public virtual void Deactivate()
    {
        if (partSyst != null)
        {
            partSyst.Stop();
        }
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
        InitializeCombatPiece();
    }
}
