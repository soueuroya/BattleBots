using static TypeHelper;

public class ShockPieceScript : CombatPieceScript
{
    private void Reset() // Setting up default values over base class
    {
        pieceType = PieceType.SHOCK;
    }
}
