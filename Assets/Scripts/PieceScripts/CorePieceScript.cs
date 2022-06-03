using UnityEngine;
using static StaticHelper;

public class CorePieceScript : FramePieceScript
{
    private const PieceType typeToUse = PieceType.CORE;
    private const float healthToUse = CORE_PIECE_HEALTH;
    private const float massToUse = CORE_PIECE_WEIGHT;
    protected void InitializeCore()
    {
        base.InitializePiece();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;
    }

    private void Start()
    {
        InitializeCore();
    }

    private void Reset()
    {
        InitializeCore();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeCore();
    }
}
