using UnityEngine;
using UnityEngine.UI;
using static StaticHelper;

public class SocketCellScript : CellScript
{
    public bool isSocketed = false;
    public PieceType socketedCell;
    public Vector2 XY;
    public MeshRenderer meshRenderer;
    public Material originalMaterial;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)){
            ClearCell();
        }
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            SelectCell();
        }
    }

    public void SelectCell()
    {
        if (GridScript.Instance.currentCell != null)
        {
            socketedCell = GridScript.Instance.currentCell.type;
            meshRenderer.material = GridScript.Instance.currentCell.GetComponent<MeshRenderer>().material;
        }
    }

    public void ClearCell()
    {
        meshRenderer.material = originalMaterial;
        socketedCell = PieceType.NONE;
    }
}
