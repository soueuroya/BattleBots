using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelectScript : CellScript
{

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            SelectCell();
        }
        else
        {
            UnselectCell();
        }
    }

    public void SelectCell()
    {
        isSelected = true;
        tr.localScale *= 1.4f;
        if (GridScript.Instance.currentCell != null)
        {
            GridScript.Instance.currentCell.UnselectCell();
        }
        GridScript.Instance.currentCell = this;
    }

    public void UnselectCell()
    {
        isSelected = false;
        tr.localScale = originalScale;
        GridScript.Instance.currentCell = null;
    }


    /*
    private void OnMouseDown()
    {
        origin = transform.localPosition;

        Vector3 point = Camera.main.ScreenToWorldPoint(Vector3.right * Input.mousePosition.x + Vector3.up * Input.mousePosition.y + Vector3.forward * tr.localPosition.y);        
        offsetX = tr.localPosition.x - point.x;
        offsetY = tr.localPosition.z - point.y; // this is intentional, the object is flipped.
    }

    private void OnMouseDrag()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Vector3.right * Input.mousePosition.x + Vector3.up * Input.mousePosition.y + Vector3.forward * tr.localPosition.y);
        tr.localPosition = (Vector3.right * (point.x + offsetX)) + (Vector3.forward * (point.y + offsetY)) + (Vector3.up * tr.localPosition.y);
    }

    private void OnMouseUp()
    {
        tr.localPosition = origin;
    }
    */
}
