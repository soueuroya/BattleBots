using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class CombatPieceScript : FramePieceScript
{
    [SerializeField] protected OutDirection direction;
    [SerializeField] protected ParticleSystem partSyst;
    [SerializeField] protected float charge;
    [SerializeField] protected float maxCharge;

    ///[SerializeField] private bool combatPieceInitialized;
    ///[SerializeField] private bool COMBATPIECELOCKUPDATE;

    protected void InitializeCombatPiece(bool forceUpdate = false)
    {
        ///if ((!combatPieceInitialized || forceUpdate) && !COMBATPIECELOCKUPDATE)
        {
            ///combatPieceInitialized = true;
            base.InitializePiece(forceUpdate);
            //TODO SET PARTICLES
            partSyst = GetComponent<ParticleSystem>();
            if (partSyst == null)
            {
                //partSyst = Resources.Load<ParticleSystem>("ParticleComponentHolders/AcidPiece");
                //partSyst = gameObject.AddComponent(typeof (ParticleSystem), Resources.Load<ParticleSystem>("ParticleComponentHolders/AcidPiece"));
            }
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

    private void Start()
    {
        ///combatPieceInitialized = false;
        InitializeCombatPiece(true);
    }

    private void Reset()
    {
        InitializeCombatPiece();
    }

    private void OnDrawGizmosSelected()
    {
        ///combatPieceInitialized = false
        InitializeCombatPiece(true);
    }
}
