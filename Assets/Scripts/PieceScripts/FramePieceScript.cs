using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class FramePieceScript : PieceScript
{
    [SerializeField] protected HingeJoint leftTire;
    [SerializeField] protected HingeJoint rightTire;
    [SerializeField] protected HingeJoint leftAxis;
    [SerializeField] protected HingeJoint rightAxis;
    [SerializeField] protected List<FixedJoint> joints;
    [SerializeField] private const PieceType typeToUse = PieceType.FRAME;
    [SerializeField] private const float healthToUse = FRAME_PIECE_HEALTH;
    [SerializeField] private const float massToUse = FRAME_PIECE_WEIGHT;

    protected void InitializeFrame()
    {
        base.InitializePiece();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;

        joints = new List<FixedJoint>(GetComponents<FixedJoint>());
    }

    public void AddJoint(Rigidbody _rb)
    {
        FixedJoint _joint = gameObject.AddComponent<FixedJoint>();
        _joint.connectedBody = _rb;
        {
            joints.Add(_joint);
        }
    }

    public void ClearJoints()
    {
        foreach (FixedJoint fj in joints)
        {
            if (Application.isPlaying || !Application.isEditor)
            {
                Destroy(fj);
            }
            else
            {
                DestroyImmediate(fj);
            }
        }
        joints.Clear();
    }

    protected override void DestroyPiece()
    {
        if (alive)
        {
            alive = false;
            Debug.Log("Destroying piece");
            foreach (FixedJoint fj in joints)
            {
                if (fj != null)
                {
                    foreach (FixedJoint _fj in fj.connectedBody.GetComponents<FixedJoint>())
                    {
                        if (_fj != null && _fj.connectedBody == rb)
                        {
                            _fj.breakForce = 0;
                        }
                    }
                    fj.breakForce = 0;
                }
            }
            if (rightAxis != null)
            {
                if (rightAxis.connectedBody != null)
                {
                    rightTire = rightAxis.connectedBody.GetComponent<HingeJoint>();
                    if (rightTire != null)
                    {
                        rightTire.breakForce = 0;
                        Debug.Log("Destroy right tire");
                        Destroy(rightTire);
                    }
                    rightAxis.connectedBody = null;
                }
                rightAxis.breakForce = 0;
                Debug.Log("Destroy right axis");
                Destroy(rightAxis);
            }
            if (leftAxis != null)
            {
                if (leftAxis.connectedBody != null)
                {
                    leftTire = leftAxis.connectedBody.GetComponent<HingeJoint>();
                    if (leftTire != null)
                    {
                        leftTire.breakForce = 0;
                        Debug.Log("Destroy left tire");
                        Destroy(leftTire);
                    }
                    leftAxis.connectedBody = null;
                }
                leftAxis.breakForce = 0;
                Debug.Log("Destroy left axis");
                Destroy(leftAxis);
            }

            /*
            if (joints != null)
            {
                int? jointIndex = null;
                for (int j = 0; j < joints.Count; j++)
                {
                    Debug.Log("Joint not null");
                    if (joints[j] != null) // TODO check this
                    {
                        List<FixedJoint> otherFixedJoints = new List<FixedJoint>(joints[j].connectedBody.gameObject.GetComponents<FixedJoint>());
                        int jointToRemove = -1;
                        for (int i = 0; i < otherFixedJoints.Count; i++)
                        {
                            if (otherFixedJoints[i].connectedBody == rb)
                            {
                                Debug.Log("Other joint not null");
                                otherFixedJoints[i].breakForce = 0;
                                jointToRemove = i;
                                //Destroy(otherFixedJoint);
                            }
                        }
                        if (jointToRemove > -1)
                        {
                            otherFixedJoints.RemoveAt(jointToRemove);
                        }
                        joints[j].breakForce = 0;
                        //Destroy(fj);
                    }
                    else
                    {
                        jointIndex = j;
                    }
                }
                if(jointIndex.HasValue)
                {
                    Debug.Log("[FramePieceScript] - joint at index '" + jointIndex.Value + "' is null");
                }
                joints.Clear();
            }
            */
        }
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
