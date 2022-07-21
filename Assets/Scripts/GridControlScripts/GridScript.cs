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
    public Material lockedMaterial;
    public Material unlockedMaterial;

    public GameObject acidPrefab;
    public GameObject firePrefab;
    public GameObject tirePrefab;
    public GameObject mtirePrefab;
    public GameObject corePrefab;
    public GameObject spikePrefab;
    public GameObject sawPrefab;
    public GameObject oilPrefab;
    public GameObject shockPrefab;
    public GameObject macePrefab;

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
        //DontDestroyOnLoad(this.gameObject);
        
        spawnedPieces = new List<PieceScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cellPrefab != null)
        {
            int tempI = -1;
            int tempJ = -1;

            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    grid[i, j] = Instantiate(cellPrefab, transform);

                    grid[i, j].isLocked = true;
                    grid[i, j].meshRenderer.material = lockedMaterial;
                    grid[i, j].pieceType = PieceType.NONE;
                    grid[i, j].x = i;
                    grid[i, j].y = j;

                    if (i == Mathf.Round(GRIDSIZE / 2) && j == Mathf.Round(GRIDSIZE / 2))
                    {
                        tempI = i;
                        tempJ = j;
                        grid[i, j].pieceType = PieceType.CORE;
                        grid[i, j].meshRenderer.material = coreMaterial;
                        grid[i, j].background.SetActive(false);
                    }

                    grid[i, j].transform.position = startingPos.position + transform.right * i * 0.065f + transform.forward * j * -0.065f;
                    grid[i, j].transform.Translate(transform.up * 0.001f);
                    //grid[i, j].transform.Translate(transform.forward * j * 0.07f);
                }
            }
            if (tempI != -1 && tempJ != -1)
            {
                UnlockNextCells(tempI, tempJ);
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

    public void RotateCell(int x, int y)
    {
        Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction);
        grid[x, y].Rotate();
        if (grid[x, y].direction.Equals(OutDirection.UP)) // FROM UP TO RIGHT
        {
            grid[x, y - 1].isTargetted--;
            grid[x, y - 1].targets.text = grid[x, y - 1].isTargetted.ToString();
            if (grid[x, y - 1].pieceType.Equals(PieceType.NONE))
            {
                if (grid[x, y - 1].isTargetted == 0)
                {
                    grid[x, y - 1].isLocked = false;
                    grid[x, y - 1].meshRenderer.material = unlockedMaterial;
                }
            }

            grid[x, y].direction = OutDirection.RIGHT;
            grid[x + 1, y].isTargetted++;
            if (!grid[x + 1, y].pieceType.Equals(PieceType.NONE))
            {
                Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction + " pointing to " + grid[x + 1, y].pieceType);
                RotateCell(x, y);
            }
            else
            {
                grid[x + 1, y].isLocked = true;
                grid[x + 1, y].targets.text = grid[x + 1, y].isTargetted.ToString();
                grid[x + 1, y].meshRenderer.material = lockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.RIGHT)) // FROM RIGHT TO DOWN
        {
            grid[x + 1, y].isTargetted--;
            grid[x + 1, y].targets.text = grid[x + 1, y].isTargetted.ToString();
            if (grid[x + 1, y].pieceType.Equals(PieceType.NONE))
            {
                if (grid[x + 1, y].isTargetted == 0)
                {
                    grid[x + 1, y].isLocked = false;
                    grid[x + 1, y].meshRenderer.material = unlockedMaterial;
                }
            }

            grid[x, y].direction = OutDirection.DOWN;
            grid[x, y + 1].isTargetted++;
            if (!grid[x, y + 1].pieceType.Equals(PieceType.NONE))
            {
                Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction + " pointing to " + grid[x, y + 1].pieceType);
                RotateCell(x, y);
            }
            else
            {
                grid[x, y + 1].isLocked = true;
                grid[x, y + 1].targets.text = grid[x, y + 1].isTargetted.ToString();
                grid[x, y + 1].meshRenderer.material = lockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.DOWN)) // FROM DOWN TO LEFT
        {
            grid[x, y + 1].isTargetted--;
            grid[x, y + 1].targets.text = grid[x, y + 1].isTargetted.ToString();
            if (grid[x, y + 1].pieceType.Equals(PieceType.NONE))
            {
                if (grid[x, y + 1].isTargetted == 0)
                {
                    grid[x, y + 1].isLocked = false;
                    grid[x, y + 1].meshRenderer.material = unlockedMaterial;
                }
            }

            grid[x, y].direction = OutDirection.LEFT;
            grid[x - 1, y].isTargetted++;
            if (!grid[x - 1, y].pieceType.Equals(PieceType.NONE))
            {
                Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction + " pointing to " + grid[x - 1, y].pieceType);
                RotateCell(x, y);
            }
            else
            {
                grid[x - 1, y].isLocked = true;
                grid[x - 1, y].targets.text = grid[x - 1, y].isTargetted.ToString();
                grid[x - 1, y].meshRenderer.material = lockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.LEFT)) // FROM LEFT TO UP
        {
            grid[x - 1, y].isTargetted--;
            grid[x - 1, y].targets.text = grid[x - 1, y].isTargetted.ToString();
            if (grid[x - 1, y].pieceType.Equals(PieceType.NONE))
            {
                if (grid[x - 1, y].isTargetted == 0)
                {
                    grid[x - 1, y].isLocked = false;
                    grid[x - 1, y].meshRenderer.material = unlockedMaterial;
                }
            }

            grid[x, y].direction = OutDirection.UP;
            grid[x, y - 1].isTargetted++;
            if (!grid[x, y - 1].pieceType.Equals(PieceType.NONE))
            {
                Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction + " pointing to " + grid[x, y - 1].pieceType);
                RotateCell(x, y);
            }
            else
            {
                grid[x, y - 1].isLocked = true;
                grid[x, y - 1].targets.text = grid[x, y - 1].isTargetted.ToString();
                grid[x, y - 1].meshRenderer.material = lockedMaterial;
            }
        }
    }

    public void SetTireCells(int x, int y)
    {
        Debug.Log("Tire cell: " + x + "/" + y + " - " + grid[x, y].direction);
        grid[x, y].Rotate();
        if (grid[x, y].direction.Equals(OutDirection.UP))
        {
            grid[x, y].direction = OutDirection.RIGHT;
            if (!grid[x + 1, y].pieceType.Equals(PieceType.NONE))
            {
                Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction + " pointing to " + grid[x + 1, y].pieceType);
                RotateCell(x, y);
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.RIGHT))
        {
            grid[x, y].direction = OutDirection.DOWN;
            if (!grid[x, y + 1].pieceType.Equals(PieceType.NONE))
            {
                Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction + " pointing to " + grid[x, y + 1].pieceType);
                RotateCell(x, y);
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.DOWN))
        {
            grid[x, y].direction = OutDirection.LEFT;
            if (!grid[x - 1, y].pieceType.Equals(PieceType.NONE))
            {
                Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction + " pointing to " + grid[x - 1, y].pieceType);
                RotateCell(x, y);
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.LEFT))
        {
            grid[x, y].direction = OutDirection.UP;
            if (!grid[x, y - 1].pieceType.Equals(PieceType.NONE))
            {
                Debug.Log("Rotation cell: " + x + "/" + y + " - " + grid[x, y].direction + " pointing to " + grid[x, y - 1].pieceType);
                RotateCell(x, y);
            }
        }
    }

    public void UnlockNextCells(int x, int y)
    {
        grid[x, y].nextCellsCount = 0;
        if (x - 1 >= 0) // LEFT CELL
        {
            grid[x - 1, y].nextCellsCount++;
            grid[x - 1, y].cellCount.text = grid[x - 1, y].nextCellsCount.ToString();

            if (grid[x - 1, y].pieceType.Equals(PieceType.NONE))
            {
                if (grid[x - 1, y].isTargetted == 0)
                {
                    grid[x - 1, y].isLocked = false;
                    grid[x - 1, y].meshRenderer.material = unlockedMaterial;
                }
            }
            else
            {
                grid[x, y].nextCellsCount++;
                grid[x, y].cellCount.text = grid[x, y].nextCellsCount.ToString();
            }
        }

        if (y - 1 >= 0)  // TOP CELL
        {
            grid[x, y - 1].nextCellsCount++;
            grid[x, y - 1].cellCount.text = grid[x, y - 1].nextCellsCount.ToString();

            if (grid[x, y - 1].pieceType.Equals(PieceType.NONE))
            {
                if (grid[x, y - 1].isTargetted == 0)
                {
                    grid[x, y - 1].isLocked = false;
                    grid[x, y - 1].meshRenderer.material = unlockedMaterial;
                }
            }
            else
            {
                grid[x, y].nextCellsCount++;
                grid[x, y].cellCount.text = grid[x, y].nextCellsCount.ToString();
            }
        }

        if (y + 1 < GRIDSIZE)  // BOTTOM CELL
        {
            grid[x, y + 1].nextCellsCount++;
            grid[x, y + 1].cellCount.text = grid[x, y + 1].nextCellsCount.ToString();

            if (grid[x, y + 1].pieceType.Equals(PieceType.NONE))
            {
                if (grid[x, y + 1].isTargetted == 0)
                {
                    grid[x, y + 1].isLocked = false;
                    grid[x, y + 1].meshRenderer.material = unlockedMaterial;
                }
            }
            else
            {
                grid[x, y].nextCellsCount++;
                grid[x, y].cellCount.text = grid[x, y].nextCellsCount.ToString();
            }
        }

        if (x + 1 < GRIDSIZE) // RIGHT CELL
        {
            grid[x + 1, y].nextCellsCount++;
            grid[x + 1, y].cellCount.text = grid[x + 1, y].nextCellsCount.ToString();

            if (grid[x + 1, y].pieceType.Equals(PieceType.NONE))
            {
                if (grid[x + 1, y].isTargetted == 0)
                {
                    grid[x + 1, y].isLocked = false;
                    grid[x + 1, y].meshRenderer.material = unlockedMaterial;
                }
            }
            else
            {
                grid[x, y].nextCellsCount++;
                grid[x, y].cellCount.text = grid[x, y].nextCellsCount.ToString();
            }
        }

        if (grid[x, y].direction.Equals(OutDirection.UP))
        {
            grid[x, y - 1].isTargetted++;
            if (!grid[x, y - 1].pieceType.Equals(PieceType.NONE))
            {
                RotateCell(x, y);
            }
            else if (!grid[x, y].pieceType.Equals(PieceType.CORE) && !grid[x, y].pieceType.Equals(PieceType.FRAME))
            {
                grid[x, y - 1].isLocked = true;
                grid[x, y - 1].targets.text = grid[x, y - 1].isTargetted.ToString();
                grid[x, y - 1].meshRenderer.material = lockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.RIGHT))
        {
            grid[x + 1, y].isTargetted++;
            if (!grid[x + 1, y].pieceType.Equals(PieceType.NONE))
            {
                RotateCell(x, y);
            }
            else if (!grid[x, y].pieceType.Equals(PieceType.CORE) && !grid[x, y].pieceType.Equals(PieceType.FRAME))
            {
                grid[x + 1, y].isLocked = true;
                grid[x + 1, y].targets.text = grid[x + 1, y].isTargetted.ToString();
                grid[x + 1, y].meshRenderer.material = lockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.DOWN))
        {
            grid[x, y + 1].isTargetted++;
            if (!grid[x, y + 1].pieceType.Equals(PieceType.NONE))
            {
                RotateCell(x, y);
            }
            else if (!grid[x, y].pieceType.Equals(PieceType.CORE) && !grid[x, y].pieceType.Equals(PieceType.FRAME))
            {
                grid[x, y + 1].isLocked = true;
                grid[x, y + 1].targets.text = grid[x, y + 1].isTargetted.ToString();
                grid[x, y + 1].meshRenderer.material = lockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.LEFT))
        {
            grid[x - 1, y].isTargetted++;
            if (!grid[x - 1, y].pieceType.Equals(PieceType.NONE))
            {
                RotateCell(x, y);
            }
            else if (!grid[x, y].pieceType.Equals(PieceType.CORE) && !grid[x, y].pieceType.Equals(PieceType.FRAME))
            {
                grid[x - 1, y].isLocked = true;
                grid[x - 1, y].targets.text = grid[x - 1, y].isTargetted.ToString();
                grid[x - 1, y].meshRenderer.material = lockedMaterial;
            }
        }
    }

    public void LockNextCells(int x, int y)
    {
        if (x - 1 >= 0)  // LEFT CELL
        {
            grid[x - 1, y].nextCellsCount--;
            grid[x - 1, y].cellCount.text = grid[x - 1, y].nextCellsCount.ToString();

            if (grid[x - 1, y].pieceType.Equals(PieceType.NONE) && grid[x - 1, y].nextCellsCount < 1)
            {
                grid[x - 1, y].isLocked = true;
                grid[x - 1, y].meshRenderer.material = lockedMaterial;
            }
            else if (!grid[x - 1, y].pieceType.Equals(PieceType.CORE) && grid[x - 1, y].nextCellsCount < 1)
            {
                grid[x - 1, y].isLocked = true;
                grid[x - 1, y].meshRenderer.material = lockedMaterial;
                grid[x - 1, y].pieceType = PieceType.NONE;
                LockNextCells(x - 1, y);
                grid[x - 1, y].ResetRotation();
                grid[x - 1, y].background.SetActive(false);
            }
        }

        if (y - 1 >= 0)  // TOP CELL
        {
            grid[x, y - 1].nextCellsCount--;
            grid[x, y - 1].cellCount.text = grid[x, y - 1].nextCellsCount.ToString();

            if (grid[x, y - 1].pieceType.Equals(PieceType.NONE) && grid[x, y - 1].nextCellsCount < 1)
            {
                grid[x, y - 1].isLocked = true;
                grid[x, y - 1].meshRenderer.material = lockedMaterial;
            }
            else if (!grid[x, y - 1].pieceType.Equals(PieceType.CORE) && grid[x, y - 1].nextCellsCount < 1)
            {
                grid[x, y - 1].isLocked = true;
                grid[x, y - 1].meshRenderer.material = lockedMaterial;
                grid[x, y - 1].pieceType = PieceType.NONE;
                LockNextCells(x, y - 1);
                grid[x, y - 1].ResetRotation();
                grid[x, y - 1].background.SetActive(false);
            }
        }

        if (y + 1 < GRIDSIZE)  // BOTTOM CELL
        {
            grid[x, y + 1].nextCellsCount--;
            grid[x, y + 1].cellCount.text = grid[x, y + 1].nextCellsCount.ToString();

            if (grid[x, y + 1].pieceType.Equals(PieceType.NONE) && grid[x, y + 1].nextCellsCount < 1)
            {
                grid[x, y + 1].isLocked = true;
                grid[x, y + 1].meshRenderer.material = lockedMaterial;
            }
            else if (!grid[x, y + 1].pieceType.Equals(PieceType.CORE) && grid[x, y + 1].nextCellsCount < 1)
            {
                grid[x, y + 1].isLocked = true;
                grid[x, y + 1].meshRenderer.material = lockedMaterial;
                grid[x, y + 1].pieceType = PieceType.NONE;
                LockNextCells(x, y + 1);
                grid[x, y + 1].ResetRotation();
                grid[x, y + 1].background.SetActive(false);
            }
        }

        if (x + 1 < GRIDSIZE) // RIGHT CELL
        {
            grid[x + 1, y].nextCellsCount--;
            grid[x + 1, y].cellCount.text = grid[x + 1, y].nextCellsCount.ToString();

            if (grid[x + 1, y].pieceType.Equals(PieceType.NONE) && grid[x + 1, y].nextCellsCount < 1)
            {
                grid[x + 1, y].isLocked = true;
                grid[x + 1, y].meshRenderer.material = lockedMaterial;
            }
            else if (!grid[x + 1, y].pieceType.Equals(PieceType.CORE) && grid[x + 1, y].nextCellsCount < 1)
            {
                grid[x + 1, y].isLocked = true;
                grid[x + 1, y].meshRenderer.material = lockedMaterial;
                grid[x + 1, y].pieceType = PieceType.NONE;
                LockNextCells(x + 1, y);
                grid[x + 1, y].ResetRotation();
                grid[x + 1, y].background.SetActive(false);
            }
        }

        if (grid[x, y].direction.Equals(OutDirection.UP))
        {
            grid[x, y - 1].isTargetted--;
            grid[x, y - 1].targets.text = grid[x, y - 1].isTargetted.ToString();

            if (grid[x, y - 1].isTargetted == 0 && grid[x, y - 1].nextCellsCount > 0)
            {
                grid[x, y - 1].isLocked = false;
                grid[x, y - 1].meshRenderer.material = unlockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.RIGHT))
        {
            grid[x + 1, y].isTargetted--;
            grid[x + 1, y].targets.text = grid[x + 1, y].isTargetted.ToString();

            if (grid[x + 1, y].isTargetted == 0 && grid[x + 1, y].nextCellsCount > 0)
            {
                grid[x + 1, y].isLocked = false;
                grid[x + 1, y].meshRenderer.material = unlockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.DOWN))
        {
            grid[x, y + 1].isTargetted--;
            grid[x, y + 1].targets.text = grid[x, y + 1].isTargetted.ToString();

            if (grid[x, y + 1].isTargetted == 0 && grid[x, y + 1].nextCellsCount > 0)
            {
                grid[x, y + 1].isLocked = false;
                grid[x, y + 1].meshRenderer.material = unlockedMaterial;
            }
        }
        else if (grid[x, y].direction.Equals(OutDirection.LEFT))
        {
            grid[x - 1, y].isTargetted--;
            grid[x - 1, y].targets.text = grid[x - 1, y].isTargetted.ToString();

            if (grid[x - 1, y].isTargetted == 0 && grid[x - 1, y].nextCellsCount > 0)
            {
                grid[x - 1, y].isLocked = false;
                grid[x - 1, y].meshRenderer.material = unlockedMaterial;
            }
        }
    }

    void SpawnRobot()
    {
        Debug.Log("Spawning Robot");
        for (int i = 0; i < GRIDSIZE; i++)
        {
            for (int j = 0; j < GRIDSIZE; j++)
            {
                Quaternion rotation = Quaternion.identity;
                if (grid[i, j].direction.Equals(OutDirection.RIGHT))
                {
                    rotation.eulerAngles = Vector3.up * 90;
                }
                else if (grid[i, j].direction.Equals(OutDirection.DOWN))
                {
                    rotation.eulerAngles = Vector3.up * 180;
                }
                else if (grid[i, j].direction.Equals(OutDirection.LEFT))
                {
                    rotation.eulerAngles = Vector3.up * 270;
                }

                Debug.Log("Checking CELL: " + i + " - " + j + "  TYPE: " + grid[i, j].type.ToString());
                if (grid[i, j].pieceType == PieceType.ACID)
                {
                    Debug.Log("spawning ACID: " + i + " - " + j);
                    spawnedPieces.Add(Instantiate(acidPrefab, spawnPosition.position + (Vector3.right * (i + 1)) + (Vector3.forward * (GRIDSIZE - j)) + (Vector3.up * 1), rotation).GetComponent<PieceScript>());
                }
                else if (grid[i, j].pieceType == PieceType.FIRE)
                {
                    Debug.Log("spawning FIRE: " + i + " - " + j);
                    Instantiate(firePrefab, spawnPosition.position + (Vector3.right * (i + 1)) + (Vector3.forward * (GRIDSIZE - j)) + (Vector3.up * 1), rotation);
                }
                else if (grid[i, j].pieceType == PieceType.TIRE)
                {
                    Debug.Log("spawning TIRE: " + i + " - " + j);
                    rotation.eulerAngles += Vector3.right * 90;
                    if (grid[i, j].direction.Equals(OutDirection.UP))
                    {
                    }

                    Instantiate(tirePrefab, spawnPosition.position + (Vector3.right * (i + 1)) + (Vector3.forward * (GRIDSIZE - j)) + (Vector3.up * 0.5f), rotation);
                }
                else if (grid[i, j].pieceType == PieceType.CORE)
                {
                    Debug.Log("spawning CORE: " + i + " - " + j);
                    Instantiate(corePrefab, spawnPosition.position + (Vector3.right * (i + 1)) + (Vector3.forward * (GRIDSIZE - j)) + (Vector3.up * 1), rotation);
                }
                else if (grid[i, j].pieceType == PieceType.SAW)
                {
                    Debug.Log("spawning SAW: " + i + " - " + j);
                    Instantiate(sawPrefab, spawnPosition.position + (Vector3.right * (i + 1)) + (Vector3.forward * (GRIDSIZE - j)) + (Vector3.up * 1), rotation);
                }
            }
        }
    }
}
