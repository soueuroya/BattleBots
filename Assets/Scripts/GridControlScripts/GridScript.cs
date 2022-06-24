using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    public CellScript cellPrefab;
    public CellSelectScript currentCell;
    public CellScript[,] grid = new CellScript[GRIDSIZE, GRIDSIZE]; // 9x9 = 81
    public Transform startingPos;
    public static GridScript Instance;
    public Material coreMat;
>>>>>>> Stashed changes

    public CellScript cells;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        
=======
        if (cellPrefab != null)
        {
            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    grid[i, j] = Instantiate(cellPrefab, transform);
                    if (i == Mathf.Round(GRIDSIZE/2) && j == Mathf.Round(GRIDSIZE/2))
                    {
                        grid[i, j].locked = true;
                        grid[i, j].type = PieceType.CORE;
                        grid[i, j].meshRenderer.material = coreMat;
                    }
                    grid[i, j].transform.position = startingPos.position + transform.right * i * 0.065f + transform.forward * j * -0.065f;

                    grid[i, j].transform.Translate(transform.up * 0.001f);
                    //grid[i, j].transform.Translate(transform.forward * j * 0.07f);

                }
            }
        }
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
