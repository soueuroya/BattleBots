using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class RobotControlScript : MonoBehaviour
{
    public static RobotControlScript Instance;
    public List<RobotScript> robots;
    public int currentRobot = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        robots = new List<RobotScript>(FindObjectsOfType<RobotScript>()); // Look for all robots
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentRobot++;
            if (currentRobot >= robots.Count)
            {
                currentRobot = 0;
            }
        }
    }

    public void Move(MovementDirection dir)
    {
        robots[currentRobot].Move(dir);
    }

    public void Stop()
    {
        robots[currentRobot].Stop();
    }
}
