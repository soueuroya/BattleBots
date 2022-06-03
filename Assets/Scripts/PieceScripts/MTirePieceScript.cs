using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class MTirePieceScript : TirePieceScript
{
    [SerializeField] private const PieceType typeToUse = PieceType.MTIRE;
    [SerializeField] private const float healthToUse = MTIRE_PIECE_HEALTH;
    [SerializeField] private const float massToUse = MTIRE_PIECE_WEIGHT;

    protected void InitializeMTire()
    {
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        tr = GetComponent<Transform>();

        //SET RIGIDBODY
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.mass = massToUse;

        //SET MESHCOLLIDER
        MeshCollider mc = GetComponent<MeshCollider>();
        if (mc == null)
        {
            mc = gameObject.AddComponent<MeshCollider>();
        }
        mc.convex = true;

        //TODO SET JOINTS
        hJoint = GetComponent<HingeJoint>();
        //joints = new List<HingeJoint>(GetComponents<HingeJoint>());
    }

    private void Start()
    {
        InitializeMTire();
    }

    private void Reset()
    {
        InitializeMTire();
    }

    private void OnDrawGizmosSelected()
    {
        InitializeMTire();
    }
}
