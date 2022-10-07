using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float distance = 10;

    [SerializeField]
    float minimumAngle = 10f;

    [SerializeField]
    float maximumAngle = 90f;

    [SerializeField]
    float sensitivity = 200f;

    [SerializeField]
    float scrollSpeed = 20f;

    float xRot = 0f;
    float yRot = 0f;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Target").transform;
        }

        if (distance <= 0)
        {
            distance = 10;
        }

        if (minimumAngle == 0)
        {
            minimumAngle = 10f;
        }

        if (maximumAngle == 0)
        {
            maximumAngle = 90f;
        }
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        }

        Track(Camera.main.transform, ControlScript.Instance.controllables[ControlScript.Instance.currentControllable].transform.GetChild(0).transform);
    }

    public void Track(Transform Tracker, Transform Tracked)
    {
        //Defining camera rotation through mouse movement (Inverse because when we move up and down we rotate the camera X not Y)
        xRot -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        yRot += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        //Limiting the rotation to a comfortable view
        if (xRot > maximumAngle)
        {
            xRot = maximumAngle;
        }
        else if (xRot < minimumAngle)
        {
            xRot = minimumAngle;
        }

        //Creating quaternion from rotation
        /*
        float cy = Mathf.Cos(0);
        float sy = Mathf.Sin(0);
        float cp = Mathf.Cos(yRot);
        float sp = Mathf.Sin(yRot);
        float cr = Mathf.Cos(xRot);
        float sr = Mathf.Sin(xRot);

        Quaternion q;
        q.w = cy * cp * cr + sy * sp * sr;
        q.x = cy * cp * sr - sy * sp * cr;
        q.y = sy * cp * sr + cy * sp * cr;
        q.z = sy * cp * cr - cy * sp * sr;
        */

        //Tracker.localRotation = q; //Updating the rotation of the camera
        Tracker.localRotation = Quaternion.Euler(xRot, yRot, 0); //Updating the rotation of the camera
        Tracker.position = Tracked.position + (distance * -Tracker.transform.forward); //Updating camera position with the distance offset
    }

    /*
    public Transform target;
    public Transform tr;
    public Vector3 offset;
    public Vector3 angle;
    void Start()
    {
        tr = transform;
        offset = tr.position - target.position;
    }
    void Update()
    {
        if (RobotControlScript.Instance.robots != null && RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].transform.GetChild(0) != null)
        {
            tr.position = offset + RobotControlScript.Instance.robots[RobotControlScript.Instance.currentRobot].transform.GetChild(0).transform.position;
        }
    }
    */
}
