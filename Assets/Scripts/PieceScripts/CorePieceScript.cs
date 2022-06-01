using UnityEngine;
using static StaticHelper;

public class CorePieceScript : FramePieceScript
{
    [SerializeField] private bool coreInitialized;
    [SerializeField] private bool CORELOCKUPDATE;

    private const PieceType typeToUse = PieceType.CORE;
    private const float healthToUse = CORE_PIECE_HEALTH;
    private const float massToUse = CORE_PIECE_WEIGHT;
    protected void InitializeCore(bool forceUpdate = false)
    {
        if ((!coreInitialized || forceUpdate) && !CORELOCKUPDATE)
        {
            coreInitialized = true;
            base.InitializePiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    private void Start()
    {
        coreInitialized = false;
        InitializeCore(true);
    }

    private void Reset()
    {
        InitializeCore();
    }

    private void OnDrawGizmosSelected()
    {
        coreInitialized = false;
        InitializeCore(true);
    }
}
