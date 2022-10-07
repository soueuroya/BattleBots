using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class Controllable : MonoBehaviour
{
    [SerializeField] protected MovementDirection currentDirection;
    public virtual void Move(MovementDirection dir) { currentDirection = dir; }

    public virtual void Stop(MovementDirection dir) { }

    public virtual void StartPower(PowerType power) { }

    public virtual void StopPower(PowerType power) { }
}
