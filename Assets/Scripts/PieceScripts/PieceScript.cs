using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;
using static UnityEngine.ParticleSystem;

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
    [SerializeField] private List<GameObject> collidedWith;
    private bool cleanCollisions;

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
                try
                {
                    originalColor = mr.materials[0].color;
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            else
            {
                try
                {
                    originalColor = mr.materials[1].color;
                }
                catch (System.Exception)
                {

                    throw;
                }
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

    private void Update()
    {
        foreach (GameObject go in collidedWith)
        {
            switch (go.gameObject.tag)
            {
                case "LavaPool":
                    TakeDamage(0.1f);
                    break;
                case "AcidPool":
                    TakeDamage(0.1f);
                    break;
                default:
                    break;
            }
        }
        if (cleanCollisions)
        {
            collidedWith.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "LavaPool":
                if (!collidedWith.Contains(other.gameObject))
                {
                    collidedWith.Add(other.gameObject);
                }
                TakeDamage(1);
                break;
            case "AcidPool":
                if (!collidedWith.Contains(other.gameObject))
                {
                    collidedWith.Add(other.gameObject);
                }
                TakeDamage(1);
                break;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.gameObject.tag + " - " + other.gameObject.name + " TOUCHING: " + gameObject.name + " - " + robot.name);
        //if (other.gameObject.tag == "Sub")
        {
            //ParticleSystem ps = other.GetComponent<ParticleSystem>();
            //MainModule main = ps.main;
            //main.simulationSpace = ParticleSystemSimulationSpace.Custom;
            //main.customSimulationSpace = tr;
            //ps.simulationSpace = ParticleSystemSimulationSpace.Custom;
        }

        switch (other.gameObject.tag)
        {
            case "Acid":
                TakeDamage(1);
                break;
            case "Fire":
                TakeDamage(1);
                break;
            case "Shock":
                TakeDamage(1);
                break;
            case "Spike":
                TakeDamage(1);
                break;
            case "Oil":
                TakeDamage(1);
                break;
            case "Saw":
                TakeDamage(1);
                break;
            case "Bomb":
                TakeDamage(1);
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
        if (health > 0)
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
                healthbar.UpdateHealth(health / maxHealth);
                Invoke("RestoreColor", 0.2f);
            }
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