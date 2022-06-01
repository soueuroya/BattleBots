using System.Collections.Generic;
using UnityEngine;

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

        robots = new List<RobotScript>(FindObjectsOfType<RobotScript>());
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
    private void OnDrawGizmosSelected()
    {
        robots.Clear();
        robots = new List<RobotScript>(FindObjectsOfType<RobotScript>());
    }
}
