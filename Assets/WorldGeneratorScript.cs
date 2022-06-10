using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneratorScript : MonoBehaviour
{
    public GameObject water, sand, grass, rock;
    public float waterLimit, sandLimit, grassLimit;
    public float gridSize;
    public float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        float halfway = gridSize / 2;


        for (float i = -halfway; i < halfway; i++)
        {
            for (float j = -halfway; j < halfway; j++)
            {
                float random = Mathf.PerlinNoise((i-Mathf.Abs(i-halfway))*0.15f, (j-Mathf.Abs(j-halfway)*0.15f));
                random /= 1 + (Mathf.Abs(gridSize - i * - j)/50);
                Debug.Log(random);
                if (random < waterLimit)
                {
                    Instantiate(water, new Vector3(i*10, random*50, j*10), Quaternion.identity, transform);
                    
                }
                else if (random < sandLimit)
                {
                    Instantiate(sand, new Vector3(i * 10, random * 50, j * 10), Quaternion.identity, transform);
                }
                else if (random < grassLimit)
                {
                    Instantiate(grass, new Vector3(i * 10, random * 50, j * 10), Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(rock, new Vector3(i * 10, random * 50, j * 10), Quaternion.identity, transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
