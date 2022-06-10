using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorManagerScript : MonoBehaviour
{
    [SerializeField] List<TrapdoorScript> trapdoors;

    private void OnValidate()
    {
        trapdoors = new List<TrapdoorScript>(GetComponentsInChildren<TrapdoorScript>());
    }

    void Start()
    {
        InvokeRepeating("ToggleTrapdoors", 8f, 5f);
    }

    public void ToggleTrapdoors()
    {
        foreach (TrapdoorScript trapdoor in trapdoors)
        {
            trapdoor.Activate();
        }
    }
}
