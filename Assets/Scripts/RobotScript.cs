using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TypeHelper;

public class RobotScript : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public List<TireScript> tires;
    public List<AcidPieceScript> acids;
    public List<FirePieceScript> fires;
    public List<ShockPieceScript> shocks;
    public List<OilPieceScript> oils;

    private void Awake()
    {
        health = maxHealth;

        foreach (Transform child in transform)
        {
            if (child != null)
            {
                if (child.GetComponent<PieceScript>() != null)
                {
                    switch (child.GetComponent<PieceScript>().pieceType)
                    {
                        case PieceType.TIRE:tires.Add(child.GetComponent<TireScript>());
                            break;
                        case PieceType.MTIRE:tires.Add(child.GetComponent<MTireScript>());
                            break;
                        case PieceType.FRAME:
                            break;
                        case PieceType.ACID: acids.Add(child.GetComponent<AcidPieceScript>());
                            break;
                        case PieceType.FIRE: fires.Add(child.GetComponent<FirePieceScript>());
                            break;
                        case PieceType.SHOCK: shocks.Add(child.GetComponent<ShockPieceScript>());
                            break;
                        case PieceType.OIL: oils.Add(child.GetComponent<OilPieceScript>());
                            break;
                        case PieceType.BOMB:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void Start()
    {
        health = maxHealth;
        
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
        //Stop all tires .
        foreach (TireScript tire in tires)
        {
            tire.RotateForward(0);
        }
    }

    public void MoveForward()
    {
        //Rotate all tires forward.
        foreach(TireScript tire in tires)
        {
            tire.RotateForward(speed);
        }
    }

    public void MoveBack()
    {
        //Rotate all tires back.
        foreach (TireScript tire in tires)
        {
            tire.RotateForward(-speed);
        }
    }

    public void MoveLeft()
    {
        //Rotate all left tires back and all right tires forward.
        foreach (TireScript tire in tires)
        {
            if (tire.side.Equals(TireSide.LEFT))
            {
                tire.RotateForward(-speed);
            }
            else if (tire.side.Equals(TireSide.RIGHT))
            {
                tire.RotateForward(speed);
            }
        }
    }

    public void MoveRight()
    {
        //Rotate all left tires forward and all right tires back.
        foreach (TireScript tire in tires)
        {
            if (tire.side.Equals(TireSide.LEFT))
            {
                tire.RotateForward(speed);
            }
            else if (tire.side.Equals(TireSide.RIGHT))
            {
                tire.RotateForward(-speed);
            }
        }
    }

    #endregion
}
