using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;
using static UnityEngine.ParticleSystem;

public class RobotScript : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected List<TirePieceScript> tires = new List<TirePieceScript>();
    [SerializeField] protected List<AcidPieceScript> acids = new List<AcidPieceScript>();
    [SerializeField] protected List<FirePieceScript> fires = new List<FirePieceScript>();
    [SerializeField] protected List<ShockPieceScript> shocks = new List<ShockPieceScript>();
    [SerializeField] protected List<SpikePieceScript> spikes = new List<SpikePieceScript>();
    [SerializeField] protected List<OilPieceScript> oils = new List<OilPieceScript>();
    [SerializeField] protected List<BombPieceScript> bombs = new List<BombPieceScript>();
    [SerializeField] protected List<SawPieceScript> saws = new List<SawPieceScript>();
    [SerializeField] protected List<FramePieceScript> frames = new List<FramePieceScript>();
    [SerializeField] protected List<CorePieceScript> cores = new List<CorePieceScript>();

    protected void Initialize()
    {
        maxHealth = 0;
        if (tires != null)
        {
            tires.Clear();
        }
        if (acids != null)
        {
            acids.Clear();
        }
        if (fires != null)
        {
            fires.Clear();
        }
        if (oils != null)
        {
            oils.Clear();
        }
        if (shocks != null)
        {
            shocks.Clear();
        }
        if (frames != null)
        {
            frames.Clear();
        }
        if (cores != null)
        {
            cores.Clear();
        }
        //Arrange child objects
        foreach (Transform child in transform) // FOR EACH GAMEOBJECT IN THE ROBOT
        {
            if (child != null)
            {
                PieceScript piece = child.GetComponent<PieceScript>(); // GET THE PIECE SCRIPT
                if (piece != null)
                {
                    piece.SetRobot(this);
                    FramePieceScript framePiece = piece as FramePieceScript; // GET THE FRAMESCRIPT (CORES AND BATTLE PIECES, NO TIRES)
                    if (framePiece != null)
                    {
                        framePiece.ClearJoints();
                        foreach (Transform child2 in transform)
                        {
                            if (child != child2) // IF NOT THE SAME PIECE
                            {
                                PieceScript piece2 = child2.GetComponent<PieceScript>();
                                FramePieceScript framePiece2 = piece2 as FramePieceScript;
                                if (framePiece2 != null)
                                {
                                    framePiece.AddJoint(framePiece2.RB);
                                }
                            }
                        }
                    }
                    CombatPieceScript combatPiece = piece as CombatPieceScript; // GET THE COMBAT SCRIPT, SO WE CAN MAKE THE WEAPONS IGNORE OWN ROBOT
                    if (combatPiece != null)
                    {
                        if (combatPiece.partSyst != null)
                        {
                            CollisionModule col = combatPiece.partSyst.collision;
                            col.collidesWith = col.collidesWith & ~(1 << this.gameObject.layer);
                        }
                    }
                    maxHealth += piece.Health;
                    switch (piece.PieceType)
                    {
                        case PieceType.TIRE:
                            tires.Add(piece as TirePieceScript);
                            break;
                        case PieceType.MTIRE:
                            tires.Add(piece as MTirePieceScript);
                            break;
                        case PieceType.FRAME:
                            frames.Add(piece as FramePieceScript);
                            break;
                        case PieceType.ACID:
                            acids.Add(piece as AcidPieceScript);
                            break;
                        case PieceType.FIRE:
                            fires.Add(piece as FirePieceScript);
                            break;
                        case PieceType.SHOCK:
                            shocks.Add(piece as ShockPieceScript);
                            break;
                        case PieceType.OIL:
                            oils.Add(piece as OilPieceScript);
                            break;
                        case PieceType.BOMB:
                            bombs.Add(piece as BombPieceScript);
                            break;
                        case PieceType.SPIKE:
                            spikes.Add(piece as SpikePieceScript);
                            break;
                        case PieceType.SAW:
                            saws.Add(piece as SawPieceScript);
                            break;
                        case PieceType.CORE:
                            cores.Add(piece as CorePieceScript);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        foreach (Transform child in transform)
        {
            Rigidbody _rb = child.GetComponent<Rigidbody>();
            if (_rb != null)
            {
                _rb.isKinematic = false;
                _rb.centerOfMass += new Vector3(0, -0.2f, 0);
            }
        }
        health = maxHealth;
    }

    private void Start()
    {
        Initialize();
    }

    private void Reset()
    {
        Initialize();
    }

    private void OnDrawGizmosSelected()
    {
        Initialize();
    }
    #region POWER

    public void StartPower(PowerType power)
    {
        switch (power)
        {
            case PowerType.ACID:
                StartAcids();
                break;
            case PowerType.FIRE:
                StartFires();
                break;
            case PowerType.SHOCK:
                StartShocks();
                break;
            case PowerType.OIL:
                StartOils();
                break;
            case PowerType.SPIKE:
                StartSpikes();
                break;
            case PowerType.BOMB:
                StartBombs();
                break;
            case PowerType.SAW:
                StartSaws();
                break;
            default:
                break;
        }
    }

    public void LosePiece(PieceScript _piece)
    {
        _piece.gameObject.layer = LayerMask.NameToLayer("Default");
        switch (_piece.PieceType)
        {
            case PieceType.TIRE:
                if (tires.Contains(_piece as TirePieceScript))
                {
                    tires.Remove(_piece as TirePieceScript);
                }
                break;
            case PieceType.MTIRE:
                if (tires.Contains(_piece as TirePieceScript))
                {
                    tires.Remove(_piece as TirePieceScript);
                }
                break;
            case PieceType.FRAME:
                if (frames.Contains(_piece as FramePieceScript))
                {
                    frames.Remove(_piece as FramePieceScript);
                }
                break;
            case PieceType.ACID:
                if (acids.Contains(_piece as AcidPieceScript))
                {
                    acids.Remove(_piece as AcidPieceScript);
                }
                break;
            case PieceType.FIRE:
                if (fires.Contains(_piece as FirePieceScript))
                {
                    fires.Remove(_piece as FirePieceScript);
                }
                break;
            case PieceType.SHOCK:
                if (shocks.Contains(_piece as ShockPieceScript))
                {
                    shocks.Remove(_piece as ShockPieceScript);
                }
                break;
            case PieceType.OIL:
                if (oils.Contains(_piece as OilPieceScript))
                {
                    oils.Remove(_piece as OilPieceScript);
                }
                break;
            case PieceType.BOMB:
                if (bombs.Contains(_piece as BombPieceScript))
                {
                    bombs.Remove(_piece as BombPieceScript);
                }
                break;
            case PieceType.SPIKE:
                if (spikes.Contains(_piece as SpikePieceScript))
                {
                    spikes.Remove(_piece as SpikePieceScript);
                }
                break;
            case PieceType.CORE:
                if (cores.Contains(_piece as CorePieceScript))
                {
                    cores.Remove(_piece as CorePieceScript);
                }
                break;
            default:
                break;
        }
    }

    public void StartAcids()
    {
        foreach (AcidPieceScript acid in acids)
        {
            acid.Activate();
        }
    }
    public void StopAcids()
    {
        foreach (AcidPieceScript acid in acids)
        {
            acid.Deactivate();
        }
    }
    public void StartFires()
    {
        foreach (FirePieceScript fire in fires)
        {
            fire.Activate();
        }
    }
    public void StopFires()
    {
        foreach (FirePieceScript fire in fires)
        {
            fire.Deactivate();
        }
    }
    public void StartShocks()
    {
        foreach (ShockPieceScript schock in shocks)
        {
            schock.Activate();
        }
    }
    public void StopShocks()
    {
        foreach (ShockPieceScript schock in shocks)
        {
            schock.Deactivate();
        }
    }
    public void StartOils()
    {
        foreach (OilPieceScript oil in oils)
        {
            oil.Activate();
        }
    }
    public void StopOils()
    {
        foreach (OilPieceScript oil in oils)
        {
            oil.Deactivate();
        }
    }
    public void StartSpikes()
    {
        foreach (SpikePieceScript spike in spikes)
        {
            spike.Activate();
        }
    }
    public void StopSpikes()
    {
        foreach (SpikePieceScript spike in spikes)
        {
            spike.Deactivate();
        }
    }
    public void StartBombs()
    {
        foreach (BombPieceScript bomb in bombs)
        {
            bomb.Activate();
        }
    }
    public void StopBombs()
    {
        foreach (BombPieceScript bomb in bombs)
        {
            bomb.Deactivate();
        }
    }

    public void StartSaws()
    {
        foreach (SawPieceScript saw in saws)
        {
            saw.Activate();
        }
    }
    public void StopSaws()
    {
        foreach (SawPieceScript saw in saws)
        {
            saw.Deactivate();
        }
    }

    public void StopPower(PowerType power)
    {
        switch (power)
        {
            case PowerType.ACID:
                StopAcids();
                break;
            case PowerType.FIRE:
                StopFires();
                break;
            case PowerType.SHOCK:
                StopShocks();
                break;
            case PowerType.OIL:
                StopOils();
                break;
            case PowerType.SPIKE:
                StopSpikes();
                break;
            case PowerType.BOMB:
                StopBombs();
                break;
            case PowerType.SAW:
                StopSaws();
                break;
            default:
                break;
        }
    }

    #endregion

    #region MOVEMENT
    public void Move(MovementDirection dir)
    {
        switch (dir)
        {
            case MovementDirection.LEFT:
                MoveLeft();
                break;
            case MovementDirection.RIGHT:
                MoveRight();
                break;
            case MovementDirection.FORWARD:
                MoveForward();
                break;
            case MovementDirection.DOWN:
                MoveBack();
                break;
            case MovementDirection.TLEFT:
                MoveForwardLeft();
                break;
            case MovementDirection.TRIGHT:
                MoveForwardRight();
                break;
            case MovementDirection.BLEFT:
                MoveBackLeft();
                break;
            case MovementDirection.BRIGHT:
                MoveBackRight();
                break;
            default:
                break;
        }
    }

    public void Stop()
    {
        //Stop all tires
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                tires[i].RotateForward(0);
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }

    public void MoveForward()
    {
        //Rotate all tires forward.
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                if (tires[i].TireHSide == TireHSide.LEFT && tires[i].TireOrientation == TireOrientation.LEFT) // LEFT LEFT
                {
                    tires[i].RotateForward(-speed);
                }
                else if (tires[i].TireHSide == TireHSide.RIGHT && tires[i].TireOrientation == TireOrientation.LEFT) // RIGHT LEFT
                {
                    tires[i].RotateForward(-speed);
                }
                else
                {
                    tires[i].RotateForward(speed);
                }
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }

    public void MoveBack()
    {
        //Rotate all tires back.
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                if (tires[i].TireHSide == TireHSide.LEFT && tires[i].TireOrientation == TireOrientation.LEFT) // LEFT LEFT
                {
                    tires[i].RotateForward(speed);
                }
                else if (tires[i].TireHSide == TireHSide.RIGHT && tires[i].TireOrientation == TireOrientation.LEFT) // RIGHT LEFT
                {
                    tires[i].RotateForward(speed);
                }
                else
                {
                    tires[i].RotateForward(-speed);
                }
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }

    public void MoveLeft()
    {
        //Rotate all left tires back and all right tires forward.
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                if (tires[i].TireHSide == TireHSide.LEFT) // LEFT of the car
                {
                    if (tires[i].TireOrientation == TireOrientation.LEFT) // default
                    {
                        tires[i].RotateForward(speed, true);
                    }
                    else // inverted to the right
                    {
                        tires[i].RotateForward(-speed, true);
                    }
                }
                else if (tires[i].TireHSide == TireHSide.RIGHT) // RIGHT of the car
                {
                    if (tires[i].TireOrientation == TireOrientation.LEFT) // inverted to the left
                    {
                        tires[i].RotateForward(-speed, true);
                    }
                    else // default
                    {
                        tires[i].RotateForward(speed, true);
                    }
                }
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }

    public void MoveForwardLeft()
    {
        //Just rotate right tires forward.
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                if (tires[i].TireHSide == TireHSide.RIGHT) // RIGHT of the car
                {
                    if (tires[i].TireOrientation == TireOrientation.LEFT) // inverted to the left
                    {
                        tires[i].RotateForward(-speed, true);
                    }
                    else // default
                    {
                        tires[i].RotateForward(speed, true);
                    }
                }
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }

    public void MoveBackLeft()
    {
        //Just rotate right tires back.
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                if (tires[i].TireHSide == TireHSide.RIGHT) // RIGHT of the car
                {
                    if (tires[i].TireOrientation == TireOrientation.LEFT) // inverted to the left
                    {
                        tires[i].RotateForward(speed, true);
                    }
                    else // default
                    {
                        tires[i].RotateForward(-speed, true);
                    }
                }
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }

    public void MoveRight()
    {
        //Rotate all left tires forward and all right tires back.
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                if (tires[i].TireHSide == TireHSide.LEFT) // LEFT of the car
                {
                    if (tires[i].TireOrientation == TireOrientation.LEFT) // default
                    {
                        tires[i].RotateForward(-speed, true);
                    }
                    else // inverted to the right
                    {
                        tires[i].RotateForward(speed, true);
                    }
                }
                else if (tires[i].TireHSide == TireHSide.RIGHT) // RIGHT of the car
                {
                    if (tires[i].TireOrientation == TireOrientation.LEFT) // inverted to the left
                    {
                        tires[i].RotateForward(speed, true);
                    }
                    else // default
                    {
                        tires[i].RotateForward(-speed, true);
                    }
                }
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }

    public void MoveForwardRight()
    {
        //Rotate all left tires forward
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                if (tires[i].TireHSide == TireHSide.LEFT) // LEFT of the car
                {
                    if (tires[i].TireOrientation == TireOrientation.LEFT) // default
                    {
                        tires[i].RotateForward(-speed, true);
                    }
                    else // inverted to the right
                    {
                        tires[i].RotateForward(speed, true);
                    }
                }
                else if (tires[i].TireHSide == TireHSide.RIGHT) // RIGHT of the car
                {
                    /*if (tires[i].TireOrientation == TireOrientation.LEFT) // inverted to the left
                    {
                        tires[i].RotateForward(speed, true);
                    }
                    else // default
                    {
                        tires[i].RotateForward(-speed, true);
                    }*/
                }
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }

    public void MoveBackRight()
    {
        //Rotate all left tires back
        int? tireIndex = null;
        for (int i = 0; i < tires.Count; i++)
        {
            if (tires[i].HJoint != null)
            {
                if (tires[i].TireHSide == TireHSide.LEFT) // LEFT of the car
                {
                    if (tires[i].TireOrientation == TireOrientation.LEFT) // default
                    {
                        tires[i].RotateForward(speed, true);
                    }
                    else // inverted to the right
                    {
                        tires[i].RotateForward(-speed, true);
                    }
                }
                else if (tires[i].TireHSide == TireHSide.RIGHT) // RIGHT of the car
                {
                    /*if (tires[i].TireOrientation == TireOrientation.LEFT) // inverted to the left
                    {
                        tires[i].RotateForward(speed, true);
                    }
                    else // default
                    {
                        tires[i].RotateForward(-speed, true);
                    }*/
                }
            }
            else
            {
                tireIndex = i;
            }
        }
        if (tireIndex.HasValue)
        {
            tires.RemoveAt(tireIndex.Value);
        }
    }
    #endregion
}