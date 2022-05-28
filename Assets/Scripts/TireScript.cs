using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TypeHelper;

public class TireScript : PieceScript
{
    
    public TireSide side;

    public void RotateForward(float speed)
    {
        //rb.angularVelocity = tr.up * speed;
        // Make the hinge motor rotate with 90 degrees per second and a strong force.
        if (speed == 0)
        {
            joint.useMotor = false;
        }
        else
        {
            var motor = joint.motor;
            motor.force = 100;
            motor.targetVelocity = speed;
            motor.freeSpin = false;
            joint.motor = motor;
            joint.useMotor = true;
        }
    }

    private void Reset()  // Setting up default values over base class
    {
        pieceType = PieceType.TIRE;
    }
}
