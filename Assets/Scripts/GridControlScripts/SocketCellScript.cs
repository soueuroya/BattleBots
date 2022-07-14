using UnityEngine;
using UnityEngine.UI;
using static StaticHelper;

public class SocketCellScript : CellScript
{
    public bool isSocketed = false;
    public PieceType pieceType;
    public int x, y;
    public int nextCellsCount = 0;
    public MeshRenderer meshRenderer;
    public Material originalMaterial;

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
            pieceType = GridScript.Instance.currentCell.type;
            meshRenderer.material = GridScript.Instance.currentCell.GetComponent<MeshRenderer>().material;
            GridScript.Instance.UnlockNextCells(x, y);
        }
    }

    public void ClearCell()
    {
        if (!pieceType.Equals(PieceType.NONE) && nextCellsCount < 2)
        {
            meshRenderer.material = originalMaterial;
            pieceType = PieceType.NONE;
            GridScript.Instance.LockNextCells(x, y);
        }
    }
}
