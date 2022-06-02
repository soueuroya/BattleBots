using UnityEngine;
using static StaticHelper;

public class FirePieceScript : CombatPieceScript
{
    ///[SerializeField] private bool fireInitialized;
    ///[SerializeField] private bool FIRELOCKUPDATE;

    [SerializeField] private const PieceType typeToUse = PieceType.FIRE;
    [SerializeField] private const float healthToUse = FIRE_PIECE_HEALTH;
    [SerializeField] private const float massToUse = FIRE_PIECE_WEIGHT;
    protected void InitializeFire(bool forceUpdate = false)
    {
        ///if ((!fireInitialized || forceUpdate) && !FIRELOCKUPDATE)
        {
            ///fireInitialized = true;
            base.InitializeCombatPiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    private void Start()
    {
        ///fireInitialized = false;
        InitializeFire(true);
    }

    private void Reset()
    {
        InitializeFire();
    }

    private void OnDrawGizmosSelected()
    {
        ///fireInitialized = false;
        InitializeFire(true);
    }
}
