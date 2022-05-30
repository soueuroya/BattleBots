using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

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
    [SerializeField] protected List<FramePieceScript> frames = new List<FramePieceScript>();
    [SerializeField] protected List<CorePieceScript> cores = new List<CorePieceScript>();

    [SerializeField] protected bool initialized;
    [SerializeField] protected bool LOCKUPDATE;
    protected void Initialize(bool forceUpdate = false)
    {
        if ((!initialized || forceUpdate) && !LOCKUPDATE)
        {
            initialized = true;
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

            foreach (Transform child in transform)
            {
                if (child != null)
                {
                    if (child.GetComponent<PieceScript>() != null)
                    {
                        maxHealth += child.GetComponent<PieceScript>().Health;
                        switch (child.GetComponent<PieceScript>().PieceType)
                        {
                            case PieceType.TIRE: tires.Add(child.GetComponent<TirePieceScript>());
                                break;
                            case PieceType.FRAME: frames.Add(child.GetComponent<FramePieceScript>());
                                break;
                            case PieceType.ACID: acids.Add(child.GetComponent<AcidPieceScript>());
                                break;
                            case PieceType.FIRE: fires.Add(child.GetComponent<FirePieceScript>());
                                break;
                            case PieceType.SHOCK: shocks.Add(child.GetComponent<ShockPieceScript>());
                                break;
                            case PieceType.OIL: oils.Add(child.GetComponent<OilPieceScript>());
                                break;
                            case PieceType.BOMB: bombs.Add(child.GetComponent<BombPieceScript>());
                                break;
                            case PieceType.CORE:
                                cores.Add(child.GetComponent<CorePieceScript>());
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            health = maxHealth;
        }
    }

    private void Awake()
    {
        Initialize();
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
        Initialize(true);
    }
    #region POWER

    public void StartPower(PowerType power)
    {
        switch (power)
        {
            case PowerType.ACID: StartAcids();
                break;
            case PowerType.FIRE: StartFires();
                break;
            case PowerType.SHOCK: StartShocks();
                break;
            case PowerType.OIL: StartOils();
                break;
            case PowerType.SPIKE: StartSpikes();
                break;
            default:
                break;
        }
    }

    public void StartAcids()
    {
        Debug.Log("STARTING ACIDS");
        foreach (AcidPieceScript acid in acids)
        {
            Debug.Log("STARTING acid");
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

    public void StopPower(PowerType power)
    {
        switch (power)
        {
            case PowerType.ACID: StopAcids();
                break;
            case PowerType.FIRE: StopFires();
                break;
            case PowerType.SHOCK: StopShocks();
                break;
            case PowerType.OIL: StopOils();
                break;
            case PowerType.SPIKE: StopSpikes();
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
            case MovementDirection.LEFT: MoveLeft();
                break;
            case MovementDirection.RIGHT: MoveRight();
                break;
            case MovementDirection.FORWARD: MoveForward();
                break;
            case MovementDirection.DOWN: MoveBack();
                break;
            default:
                break;
        }
    }

    public void Stop()
    {
        //Stop all tires
        foreach (TirePieceScript tire in tires)
        {
            tire.RotateForward(0);
        }
    }

    public void MoveForward()
    {
        //Rotate all tires forward.
        foreach(TirePieceScript tire in tires)
        {
            tire.RotateForward(speed);
        }
    }

    public void MoveBack()
    {
        //Rotate all tires back.
        foreach (TirePieceScript tire in tires)
        {
            tire.RotateForward(-speed);
        }
    }

    public void MoveLeft()
    {
        //Rotate all left tires back and all right tires forward.
        foreach (TirePieceScript tire in tires)
        {
            if (tire.TireSide.Equals(TireSide.LEFT))
            {
                tire.RotateForward(-speed);
            }
            else if (tire.TireSide.Equals(TireSide.RIGHT))
            {
                tire.RotateForward(speed);
            }
        }
    }

    public void MoveRight()
    {
        //Rotate all left tires forward and all right tires back.
        foreach (TirePieceScript tire in tires)
        {
            if (tire.TireSide.Equals(TireSide.LEFT))
            {
                tire.RotateForward(speed);
            }
            else if (tire.TireSide.Equals(TireSide.RIGHT))
            {
                tire.RotateForward(-speed);
            }
        }
    }

    #endregion
}
