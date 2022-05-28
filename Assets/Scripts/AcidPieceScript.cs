using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TypeHelper;

public class AcidPieceScript : CombatPieceScript
{
    private void Reset() // Setting up default values over base class
    {
        pieceType = PieceType.ACID;
    }

    private void OnDrawGizmosSelected() // Setting up default values on editor selection.
    {
        base.OnDrawGizmosSelected();
    }
}
