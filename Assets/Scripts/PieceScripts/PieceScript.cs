using UnityEngine;
using static StaticHelper;

public class PieceScript : MonoBehaviour
{
    public PieceType PieceType { get { return pieceType; } private set { pieceType = value; } }
    [SerializeField] protected PieceType pieceType;
    [SerializeField] protected Transform tr;
    [SerializeField] protected Rigidbody rb;
    public Rigidbody RB { get { return rb; } private set { rb = value; } }

    public float Health { get { return health; } private set { health = value; } }
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;

    [SerializeField] private bool pieceInitialized;
    [SerializeField] private bool PIECELOCKUPDATE;

    protected void InitializePiece(bool forceUpdate = false)
    {
        if ((!pieceInitialized || forceUpdate) && !PIECELOCKUPDATE)
        {
            tr = GetComponent<Transform>();

            //SET RIGIDBODY
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }

            //SET MESHCOLLIDER
            MeshCollider mc = GetComponent<MeshCollider>();
            if (mc == null)
            {
                mc = gameObject.AddComponent<MeshCollider>();
            }
            mc.convex = true;
        }
    }

    private void Awake()
    {
        InitializePiece();
    }

    private void Start()
    {
        InitializePiece();
    }

    private void Reset()
    {
        InitializePiece();
    }

    private void OnDrawGizmosSelected()
    {
        InitializePiece(true);
    }
}