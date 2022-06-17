using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class GridScript : MonoBehaviour
{
    public CellScript cellPrefab;
    public CellSelectScript currentCell;
    public CellScript[,] grid = new CellScript[GRIDSIZE, GRIDSIZE]; // 9x9 = 81
    public Transform startingPos;
    public static GridScript Instance;

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

        // Start is called before the first frame update
        void Start()
    {
        if (cellPrefab != null)
        {
            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    grid[i, j] = Instantiate(cellPrefab, transform);
                    grid[i, j].transform.position = startingPos.position + transform.right * i * 0.065f + transform.forward * j * -0.065f;

                    grid[i, j].transform.Translate(transform.up * 0.001f);
                    //grid[i, j].transform.Translate(transform.forward * j * 0.07f);

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
