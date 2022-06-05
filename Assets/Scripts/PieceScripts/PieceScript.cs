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
    [SerializeField] protected float maxHealth;
    [SerializeField] protected bool alive;
    [SerializeField] protected Canvas healthbarCanvas;
    [SerializeField] protected HealthbarScript healthbar;
    [SerializeField] protected Transform tr;
    [SerializeField] protected MeshRenderer mr;
    [SerializeField] private Color originalColor;
    [SerializeField] protected RobotScript robot;

    protected void InitializePiece()
    {
        alive = true;
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
            if (IsFirstMaterial())
            {
                originalColor = mr.materials[0].color;
            }
            else
            {
                originalColor = mr.materials[1].color;
            }
        }

        //SET HEALTHBAR
        healthbarCanvas = GetComponentInChildren<Canvas>(true);
        healthbar = healthbarCanvas.GetComponentInChildren<HealthbarScript>(true);
    }

    private void OnMouseDown()
    {
        TakeDamage(10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Acid":
                break;
            case "Fire":
                break;
            case "Shock":
                break;
            case "Spike":
                break;
            case "Oil":
                break;
            default: 
                break;
        }
    }

    public void SetRobot(RobotScript _robot)
    {
        robot = _robot;
    }
    private void TakeDamage(float damage)
    {
        if (mr != null)
        {
            if (IsFirstMaterial())
            {
                mr.materials[0].color = Color.white;
            }
            else
            {
                mr.materials[1].color = Color.white;
            }
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                DestroyPiece();
                //TODO Destroy piece, if core, game over for this car.
            }
            ShowHealthbarCanvas();
            healthbar.UpdateHealth(health/maxHealth);
            Invoke("RestoreColor", 0.2f);
        }
    }

    protected virtual void DestroyPiece(){}

    private void RestoreColor()
    {
        if (mr != null)
        {
            if (IsFirstMaterial())
            {
                mr.materials[0].color = originalColor;
            }
            else
            {
                mr.materials[1].color = originalColor;
            }
        }
    }

    private bool IsFirstMaterial()
    {
        return pieceType.Equals(PieceType.FRAME) || pieceType.Equals(PieceType.TIRE) || pieceType.Equals(PieceType.MTIRE);
    }

    private void ShowHealthbarCanvas()
    {
        healthbarCanvas.gameObject.SetActive(true);
        CancelInvoke("HideHealthbarCanvas");
        Invoke("HideHealthbarCanvas", 0.5f);
    }

    private void HideHealthbarCanvas()
    {
        healthbarCanvas.gameObject.SetActive(false);
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
        InitializePiece();
    }
}