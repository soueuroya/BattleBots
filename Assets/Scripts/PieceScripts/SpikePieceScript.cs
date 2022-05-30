using UnityEngine;
using static StaticHelper;

public class SpikePieceScript : CombatPieceScript
{
    [SerializeField] private bool spikeInitialized;
    [SerializeField] private bool SPIKELOCKUPDATE;

    private const PieceType typeToUse = PieceType.SPIKE;
    private const float healthToUse = SPIKE_PIECE_HEALTH;
    private const float massToUse = SPIKE_PIECE_WEIGHT;

    protected void InitializeSpike(bool forceUpdate = false)
    {
        if ((!spikeInitialized || forceUpdate) && !SPIKELOCKUPDATE)
        {
            base.InitializeCombatPiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    private void Awake()
    {
        InitializeSpike();
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
