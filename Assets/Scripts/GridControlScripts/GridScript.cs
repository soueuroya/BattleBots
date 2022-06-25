using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticHelper;

public class GridScript : MonoBehaviour
{
    public SocketCellScript cellPrefab;
    public CellSelectScript currentCell;
    public SocketCellScript[,] grid = new SocketCellScript[GRIDSIZE, GRIDSIZE]; // 9x9 = 81
    public Transform startingPos;
    public static GridScript Instance;
    public Material coreMaterial;

    public GameObject acidPrefab;
    public GameObject firePrefab;
    public GameObject tirePrefab;

    public Transform spawnPosition;

    public List<PieceScript> spawnedPieces;

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

        spawnedPieces = new List<PieceScript>();
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

                    if (i == Mathf.Round(GRIDSIZE / 2) && j == Mathf.Round(GRIDSIZE / 2))
                    {
                        grid[i, j].isLocked = true;
                        grid[i, j].type = PieceType.CORE;
                        grid[i, j].meshRenderer.material = coreMaterial;
                    }

                    grid[i, j].transform.position = startingPos.position + transform.right * i * 0.065f + transform.forward * j * -0.065f;

                    grid[i, j].transform.Translate(transform.up * 0.001f);
                    //grid[i, j].transform.Translate(transform.forward * j * 0.07f);

                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SpawnRobot();
        }
    }

    void SpawnRobot()
    {
        Debug.Log("Spawning Robot");
        for (int i = 0; i < GRIDSIZE; i++)
        {
            for (int j = 0; j < GRIDSIZE; j++)
            {
                Debug.Log("Checking CELL: " + i + " - " + j + "  TYPE: " + grid[i, j].type.ToString());
                if (grid[i, j].socketedCell == PieceType.ACID)
                {
                    Debug.Log("spawning ACID: " + i + " - " + j);
                    spawnedPieces.Add(Instantiate(acidPrefab, spawnPosition.position + (Vector3.right * (i + 1)) + (Vector3.forward * (GRIDSIZE - j)) + (Vector3.up * 1), Quaternion.identity).GetComponent<PieceScript>());
                }
                if (grid[i, j].socketedCell == PieceType.FIRE)
                {
                    Debug.Log("spawning FIRE: " + i + " - " + j);
                    Instantiate(firePrefab, spawnPosition.position + (Vector3.right * (i + 1)) + (Vector3.forward * (GRIDSIZE - j)) + (Vector3.up * 1), Quaternion.identity);
                }
                if (grid[i, j].socketedCell == PieceType.TIRE)
                {
                    Debug.Log("spawning TIRE: " + i + " - " + j);
                    Instantiate(tirePrefab, spawnPosition.position + (Vector3.right * (i + 1)) + (Vector3.forward * (GRIDSIZE - j)) + (Vector3.up * 0.5f), Quaternion.Euler(new Vector3(90, 0, 0)));

                    //TODO now we need to check if i-1 is a piece, or i+1, or j-1 or j+1
                }

            }
        }
    }
}
