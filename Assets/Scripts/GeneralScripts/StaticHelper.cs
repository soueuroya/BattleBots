public class StaticHelper
{
    public enum PieceType { ACID, FIRE, SHOCK, OIL, BOMB, SPIKE, TIRE, MTIRE, FRAME, CORE, NONE }; // Type of piece
    public enum OutDirection { LEFT, RIGHT, UP, DOWN, NONE }; // Output direction of power piece
    public enum TireHSide { LEFT, RIGHT, CENTER }; // If this tire is on the left, right or center of robot. Used for turning
    public enum TireVSide { UP, DOWN, CENTER }; // If this tire is on the left, right or center of robot. Used for turning
    public enum TireOrientation { LEFT, RIGHT, UP, DOWN }; // If this tire is rotated, relative to the piece its connected. Used for turning
    public enum MovementDirection { LEFT, RIGHT, FORWARD, DOWN }; // Directions that the robot can receive
    public enum PowerType { ACID, FIRE, SHOCK, OIL, BOMB, SPIKE }; // Powers to be used in battle

    public const float ACID_PIECE_WEIGHT = 10;
    public const float ACID_PIECE_HEALTH = 40;

    public const float FIRE_PIECE_WEIGHT = 10;
    public const float FIRE_PIECE_HEALTH = 40;

    public const float SHOCK_PIECE_WEIGHT = 10;
    public const float SHOCK_PIECE_HEALTH = 40;

    public const float BOMB_PIECE_WEIGHT = 10;
    public const float BOMB_PIECE_HEALTH = 40;

    public const float OIL_PIECE_WEIGHT = 10;
    public const float OIL_PIECE_HEALTH = 40;

    public const float FRAME_PIECE_WEIGHT = 5;
    public const float FRAME_PIECE_HEALTH = 80;

    public const float SPIKE_PIECE_WEIGHT = 10;
    public const float SPIKE_PIECE_HEALTH = 40;

    public const float TIRE_PIECE_WEIGHT = 2;
    public const float TIRE_PIECE_HEALTH = 60;

    public const float MTIRE_PIECE_WEIGHT = 4;
    public const float MTIRE_PIECE_HEALTH = 80;

    public const float CORE_PIECE_WEIGHT = 5;
    public const float CORE_PIECE_HEALTH = 80;
}
