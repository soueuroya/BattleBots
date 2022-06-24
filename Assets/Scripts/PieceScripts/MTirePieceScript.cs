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
        base.InitializeTire();
        pieceType = typeToUse;
        health = maxHealth = healthToUse;
        rb.mass = massToUse;
    }

    private void OnValidate()
    {
        InitializeMTire();
    }

    private void Reset()
    {
        InitializeMTire();
    }
}
