using UnityEngine;
using static StaticHelper;

public class BombPieceScript : CombatPieceScript
{
    private const PieceType typeToUse = PieceType.BOMB;
    private const float healthToUse = BOMB_PIECE_HEALTH;
    private const float massToUse = BOMB_PIECE_WEIGHT;

    protected void InitializeBomb()
    {
        base.InitializeCombatPiece();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;
    }

    private void Start()
    {
        InitializeBomb();
    }

    private void Reset()
    {
        InitializeBomb();
    }
}
