using System.Collections.Generic;
using UnityEngine;

public class RobotControlScript : MonoBehaviour
{
    public static RobotControlScript Instance;

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

    public List<RobotScript> robots;
    public int currentRobot = 0;
}
