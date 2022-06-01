using UnityEngine;
using static StaticHelper;

public class PieceScript : MonoBehaviour
{
    public PieceType PieceType { get { return pieceType; } private set { pieceType = value; } }
    [SerializeField] protected PieceType pieceType;
    public Rigidbody RB { get { return rb; } private set { rb = value; } }
    [SerializeField] protected Rigidbody rb;
    public float Health { get { return health; } private set { health = value; } }
    [SerializeField] protected float health;

    [SerializeField] protected Transform tr;
    [SerializeField] protected MeshRenderer mr;
    [SerializeField] protected float maxHealth;
    [SerializeField] private bool pieceInitialized;
    [SerializeField] private bool PIECELOCKUPDATE;

    [SerializeField] private Color originalColor;

    protected void InitializePiece(bool forceUpdate = false)
    {
        if ((!pieceInitialized || forceUpdate) && !PIECELOCKUPDATE)
        {
            pieceInitialized = true;
            Debug.Log("INITIALIZING PIECE: " + gameObject.name);
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

            //SET MESHRENDERER
            mr = GetComponent<MeshRenderer>();
            if (mr != null)
            {
                if (pieceType.Equals(PieceType.FRAME))
                {
                    originalColor = mr.materials[0].color;
                }
                else if (!pieceType.Equals(PieceType.CORE))
                {
                    originalColor = mr.materials[1].color;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("CLICKED");
        TakeDamage();
    }

    private void TakeDamage()
    {
        if (mr != null)
        {
            if (pieceType.Equals(PieceType.FRAME))
            {
                mr.materials[0].color = Color.white;
            }
            else if (!pieceType.Equals(PieceType.CORE))
            {
                mr.materials[1].color = Color.white;
            }

            Invoke("RestoreColor", 0.2f);
        }
    }

    private void RestoreColor()
    {
        if (mr != null)
        {
            if (pieceType.Equals(PieceType.FRAME))
            {
                mr.materials[0].color = originalColor;
            }
            else if (!pieceType.Equals(PieceType.CORE))
            {
                mr.materials[1].color = originalColor;
            }
            else
            {

            }
        }
    }

    private void Start()
    {
        pieceInitialized = false;
        InitializePiece(true);
    }

    private void Reset()
    {
        InitializePiece();
    }

    private void OnDrawGizmosSelected()
    {
        pieceInitialized = false;
        InitializePiece(true);
    }
}