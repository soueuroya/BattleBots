using UnityEngine;
using static StaticHelper;

public class FirePieceScript : CombatPieceScript
{
    [SerializeField] private bool fireInitialized;
    [SerializeField] private bool FIRELOCKUPDATE;

    private const PieceType typeToUse = PieceType.FIRE;
    private const float healthToUse = FIRE_PIECE_HEALTH;
    private const float massToUse = FIRE_PIECE_WEIGHT;
    protected void InitializeFire(bool forceUpdate = false)
    {
        if ((!fireInitialized || forceUpdate) && !FIRELOCKUPDATE)
        {
            base.InitializeCombatPiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    private void Awake()
    {
        InitializeFire();
    }

    private void Start()
    {
        InitializeFire();
    }

    private void Reset()
    {
        InitializeFire();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeFire();
    }
}
