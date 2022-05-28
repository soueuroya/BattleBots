public class TypeHelper
{
    public enum PieceType { TIRE, MTIRE, FRAME, ACID, FIRE, SHOCK, OIL, BOMB }; // Type of piece

    public enum OutDirection { LEFT, RIGHT, UP, DOWN }; // Output direction of power piece

    public enum TireSide { LEFT, RIGHT, CENTER }; // If this tire is on the left, right or center of robot. Used for turning

    public enum MovementDirection { LEFT, RIGHT, FORWARD, DOWN }; // Directions that the robot can receive

    public enum PowerType { ACID, FIRE, SHOCK, OIL }; // Powers to be used in battle
}
