using UnityEngine;
using UnityEngine.UI;
using static StaticHelper;

public class SocketCellScript : CellScript
{
    public bool isSocketed = false;
    public int isTargetted = 0;
    public int x, y;
    public int nextCellsCount = 0;
    public PieceType pieceType;
    public OutDirection direction;
    public MeshRenderer meshRenderer;
    public Material originalMaterial;
    public GameObject background;
    public TMPro.TextMeshProUGUI cellCount;
    public TMPro.TextMeshProUGUI targets;

    public void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !isLocked)
        {
            ClearCell();
        }
    }

    private void OnMouseDown()
    {
        if (!isSelected && !isLocked)
        {
            SelectCell();
        }
    }

    public void SelectCell()
    {
        if (GridScript.Instance.currentCell != null)
        {
            if (!pieceType.Equals(GridScript.Instance.currentCell.type))
            {
                if (pieceType.Equals(PieceType.NONE))
                {
                    GridScript.Instance.UnlockNextCells(x, y);
                }
                pieceType = GridScript.Instance.currentCell.type;
                meshRenderer.material = GridScript.Instance.currentCell.GetComponent<MeshRenderer>().material;
                background.SetActive(true);
            }
            else
            {
                GridScript.Instance.RotateCell(x, y);
            }
        }
    }

    public void ResetRotation()
    {
        Debug.Log("resetting cell");
        direction = OutDirection.UP;
        background.transform.localRotation = Quaternion.identity;
    }

    public void Rotate()
    {
        Debug.Log("rotating cell");
        background.transform.Rotate(Vector3.up, 90);
    }

    public void ClearCell()
    {
        if (!pieceType.Equals(PieceType.NONE))
        {
            meshRenderer.material = originalMaterial;
            pieceType = PieceType.NONE;
            GridScript.Instance.LockNextCells(x, y);
            ResetRotation();
            background.SetActive(false);
        }
    }
}
