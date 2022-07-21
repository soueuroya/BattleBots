public class StaticHelper
{
    public enum PieceType { ACID, FIRE, SHOCK, OIL, BOMB, SPIKE, TIRE, MTIRE, FRAME, CORE, SAW, MACE, NONE }; // Type of piece
    public enum OutDirection { LEFT, RIGHT, UP, DOWN, NONE }; // Output direction of power piece
    public enum TireHSide { LEFT, RIGHT, CENTER }; // If this tire is on the left, right or center of robot. Used for turning
    public enum TireVSide { UP, DOWN, CENTER }; // If this tire is on the left, right or center of robot. Used for turning
    public enum TireOrientation { LEFT, RIGHT, UP, DOWN }; // If this tire is rotated, relative to the piece its connected. Used for turning
    public enum MovementDirection { LEFT, RIGHT, FORWARD, DOWN, TLEFT, TRIGHT, BLEFT, BRIGHT }; // Directions that the robot can receive
    public enum PowerType { ACID, FIRE, SHOCK, OIL, BOMB, SPIKE, SAW }; // Powers to be used in battle

    public const int TIRE_AXIS_FORCE = 100;
    public const int TIRE_FORCE = 55;
    public const int TIRE_PIVOT_ANGLE = 10;
    public const int SAW_FORCE = 200;
    public const int SAW_SPEED = 2000;

    public const int GRIDSIZE = 9;

    public const float ACID_PIECE_WEIGHT = 5; public const float ACID_PIECE_HEALTH = 40; // ACID
    public const float FIRE_PIECE_WEIGHT = 5; public const float FIRE_PIECE_HEALTH = 40; // FIRE
    public const float SHOCK_PIECE_WEIGHT = 5; public const float SHOCK_PIECE_HEALTH = 40; // SHOCK
    public const float BOMB_PIECE_WEIGHT = 5; public const float BOMB_PIECE_HEALTH = 40; // BOMB
    public const float OIL_PIECE_WEIGHT = 5; public const float OIL_PIECE_HEALTH = 40; // OIL
    public const float FRAME_PIECE_WEIGHT = 5; public const float FRAME_PIECE_HEALTH = 80; // FRAME
    public const float SPIKE_PIECE_WEIGHT = 5; public const float SPIKE_PIECE_HEALTH = 40; // SPIKE
    public const float SAW_PIECE_WEIGHT = 5; public const float SAW_PIECE_HEALTH = 40; // SAW
    public const float TIRE_PIECE_WEIGHT = 2; public const float TIRE_PIECE_HEALTH = 60; // TIRE
    public const float MTIRE_PIECE_WEIGHT = 2; public const float MTIRE_PIECE_HEALTH = 80; // MTIRE
    public const float CORE_PIECE_WEIGHT = 5; public const float CORE_PIECE_HEALTH = 80; // CORE
}
