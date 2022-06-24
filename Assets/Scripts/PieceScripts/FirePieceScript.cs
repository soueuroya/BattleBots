using UnityEngine;
using static StaticHelper;

public class FirePieceScript : CombatPieceScript
{
    [SerializeField] private const PieceType typeToUse = PieceType.FIRE;
    [SerializeField] private const float healthToUse = FIRE_PIECE_HEALTH;
    [SerializeField] private const float massToUse = FIRE_PIECE_WEIGHT;
    protected void InitializeFire()
    {
        base.InitializeCombatPiece();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;
    }

    private void Start()
    {
        InitializeFire();
    }

    private void Reset()
    {
        InitializeFire();
    }
}
