using UnityEngine;
using static StaticHelper;

public class OilPieceScript : CombatPieceScript
{
    private const PieceType typeToUse = PieceType.OIL;
    private const float healthToUse = OIL_PIECE_HEALTH;
    private const float massToUse = OIL_PIECE_WEIGHT;

    protected void InitializeOil()
    {
        base.InitializeCombatPiece();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;
    }

    private void Start()
    {
        InitializeOil();
    }

    private void Reset()
    {
        InitializeOil();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeOil();
    }
}
