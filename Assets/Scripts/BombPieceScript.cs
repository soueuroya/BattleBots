using static TypeHelper;

public class BombPieceScript : PieceScript
{
    private void Reset() // Setting up default values over base class
    {
        pieceType = PieceType.BOMB;
    }
}
