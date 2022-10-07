using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class ControlScript : MonoBehaviour
{
    public static ControlScript Instance;
    public List<Controllable> controllables;
    public int currentControllable = 0;

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
        controllables = new List<Controllable>(FindObjectsOfType<Controllable>()); // Look for all robots
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentControllable++;
            if (currentControllable >= controllables.Count)
            {
                currentControllable = 0;
            }
        }
    }

    public void Move(MovementDirection dir)
    {
        controllables[currentControllable].Move(dir);
    }

    public void Stop(MovementDirection dir)
    {
        controllables[currentControllable].Stop(dir);
    }
}
