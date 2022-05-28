using UnityEngine;
using static TypeHelper;

public class PieceScript : MonoBehaviour
{
    public PieceType pieceType;
    public Rigidbody rb;
    public Transform tr;
    public HingeJoint[] joints;
    public HingeJoint joint;

    public float health;
    public float maxHealth;

    protected void OnDrawGizmosSelected() // Setting up default values on editor selection.
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        joint = GetComponent<HingeJoint>();
        joints = GetComponents<HingeJoint>();
    }

    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        joint = GetComponent<HingeJoint>();
        joints = GetComponents<HingeJoint>();
    }

    private void Awake()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        joint = GetComponent<HingeJoint>();
        joints = GetComponents<HingeJoint>();
    }
}