using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class FramePieceScript : PieceScript
{
    [SerializeField] private bool frameInitialized;
    [SerializeField] private bool FRAMELOCKUPDATE;

    [SerializeField] protected List<FixedJoint> joints;
    [SerializeField] protected FixedJoint joint;

    private const PieceType typeToUse = PieceType.FRAME;
    private const float healthToUse = FRAME_PIECE_HEALTH;
    private const float massToUse = FRAME_PIECE_WEIGHT;
    protected void InitializeFrame(bool forceUpdate = false)
    {
        if ((!frameInitialized || forceUpdate) && !FRAMELOCKUPDATE)
        {
            frameInitialized = true;
            base.InitializePiece(forceUpdate);
            pieceType = typeToUse;
            health = maxHealth = healthToUse;
            rb.mass = massToUse;

            //TODO SET JOINTS
            joint = GetComponent<FixedJoint>();
            joints = new List<FixedJoint>(GetComponents<FixedJoint>());
        }
    }

    public void AddJoint(Rigidbody _rb)
    {
        FixedJoint _joint = gameObject.AddComponent<FixedJoint>();
        _joint.connectedBody = _rb;
        if (joint == null)
        {
            joint = _joint;
        }
        else
        {
            joints.Add(_joint);
        }
    }

    private void Start()
    {
        frameInitialized = false;
        InitializeFrame(true);
    }

    private void Reset()
    {
        InitializeFrame();
    }

    private void OnDrawGizmosSelected()
    {
        frameInitialized = false;
        InitializeFrame(true);
    }
}
