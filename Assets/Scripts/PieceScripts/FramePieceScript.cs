using UnityEngine;
using static StaticHelper;

public class FramePieceScript : PieceScript
{
    [SerializeField] private bool frameInitialized;
    [SerializeField] private bool FRAMELOCKUPDATE;

    private const PieceType typeToUse = PieceType.FRAME;
    private const float healthToUse = FRAME_PIECE_HEALTH;
    private const float massToUse = FRAME_PIECE_WEIGHT;
    protected void InitializeFrame(bool forceUpdate = false)
    {
        if ((!frameInitialized || forceUpdate) && !FRAMELOCKUPDATE)
        {
            base.InitializePiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;
        }
    }

    private void Awake()
    {
        InitializeFrame();
    }

    private void Start()
    {
        InitializeFrame();
    }

    private void Reset()
    {
        InitializeFrame();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeFrame();
    }
}
