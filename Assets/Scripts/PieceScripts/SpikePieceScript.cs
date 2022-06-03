using UnityEngine;
using static StaticHelper;

public class SpikePieceScript : CombatPieceScript
{
    private const PieceType typeToUse = PieceType.SPIKE;
    private const float healthToUse = SPIKE_PIECE_HEALTH;
    private const float massToUse = SPIKE_PIECE_WEIGHT;

    protected void InitializeSpike()
    {
        base.InitializeCombatPiece();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;
    }

    private void Start()
    {
        InitializeSpike();
    }

    private void Reset()
    {
        InitializeSpike();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeSpike();
    }
}
