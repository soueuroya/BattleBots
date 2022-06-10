using UnityEngine;
using static StaticHelper;

public class ShockPieceScript : CombatPieceScript
{
    private const PieceType typeToUse = PieceType.SHOCK;
    private const float healthToUse = SHOCK_PIECE_HEALTH;
    private const float massToUse = SHOCK_PIECE_WEIGHT;

    protected void InitializeShock()
    {
            base.InitializeCombatPiece();
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
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
        InitializeShock();
    }
}
