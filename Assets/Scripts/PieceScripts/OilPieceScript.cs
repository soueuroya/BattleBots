using UnityEngine;
using static StaticHelper;

public class OilPieceScript : CombatPieceScript
{
    [SerializeField] private bool oilInitialized;
    [SerializeField] private bool OILLOCKUPDATE;

    private const PieceType typeToUse = PieceType.OIL;
    private const float healthToUse = OIL_PIECE_HEALTH;
    private const float massToUse = OIL_PIECE_WEIGHT;

    protected void InitializeOil(bool forceUpdate = false)
    {
        if ((!oilInitialized || forceUpdate) && !OILLOCKUPDATE)
        {
            base.InitializeCombatPiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    private void Awake()
    {
        InitializeOil();
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
