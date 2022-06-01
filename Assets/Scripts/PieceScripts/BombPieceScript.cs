using UnityEngine;
using static StaticHelper;

public class BombPieceScript : CombatPieceScript
{
    [SerializeField] private bool bombInitialized;
    [SerializeField] private bool BOMBLOCKUPDATE;

    private const PieceType typeToUse = PieceType.BOMB;
    private const float healthToUse = BOMB_PIECE_HEALTH;
    private const float massToUse = BOMB_PIECE_WEIGHT;

    protected void InitializeBomb(bool forceUpdate = false)
    {
        if ((!bombInitialized || forceUpdate) && !BOMBLOCKUPDATE)
        {
            base.InitializeCombatPiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    private void Start()
    {
        bombInitialized = false;
        InitializeBomb(true);
    }

    private void Reset()
    {
        InitializeBomb();
    }

    private void OnDrawGizmosSelected()
    {
        bombInitialized = false;
        InitializeBomb(true);
    }
}
