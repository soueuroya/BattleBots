using UnityEngine;
using static StaticHelper;

public class CellScript : MonoBehaviour
{
    public Transform tr;
    public PieceType type;
    public bool hovering = false;
    public Vector3 originalScale;

    void OnValidate()
    {
        tr = transform;
        originalScale = tr.localScale;
    }

    private void OnMouseEnter()
    {
        if (!hovering)
        {
            hovering = true;
            tr.localScale *= 1.2f;
        }
    }

    private void OnMouseExit()
    {
        if (hovering)
        {
            hovering = false;
            tr.localScale = originalScale;
        }
    }
}