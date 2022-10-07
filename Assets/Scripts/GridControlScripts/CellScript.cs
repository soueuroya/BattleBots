using UnityEngine;
using static StaticHelper;

public class CellScript : MonoBehaviour
{
    public Transform tr;
    public bool hovering = false;
    public bool isSelected;
    public bool isLocked;
    public PieceType type;
    public Vector3 originalScale;

    void OnValidate()
    {
        tr = transform;
        originalScale = tr.localScale;
    }

    private void OnMouseEnter()
    {
        if (!hovering && !isLocked)
        {
            hovering = true;
            tr.localScale = originalScale * 1.2f;
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            tr.localScale = originalScale;
            hovering = false;
        }
    }
}