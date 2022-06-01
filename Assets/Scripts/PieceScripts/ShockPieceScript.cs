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
            shockInitialized = true;
            base.InitializeCombatPiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    private void Start()
    {
        shockInitialized = false;
        InitializeShock(true);
    }

    private void Reset()
    {
        InitializeShock();
    }

    private void OnDrawGizmosSelected()
    {
        shockInitialized = false;
        InitializeShock(true);
    }
}
