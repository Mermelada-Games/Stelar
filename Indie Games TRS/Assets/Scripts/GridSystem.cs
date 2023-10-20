using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    private Cell selectedCell;
    private int sortingOrder = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Cell"))
            {
                selectedCell = hit.collider.GetComponent<Cell>();
                sortingOrder++;
                selectedCell.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedCell != null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(selectedCell.transform.position, 0.1f);

            if (colliders.Length > 1)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Cell") && collider != selectedCell.GetComponent<Collider2D>())
                    {
                        Cell otherCell = collider.GetComponent<Cell>();
                        TrySwapCells(selectedCell, otherCell);
                        break;
                    }
                }
            }
            else
            {
                selectedCell.ResetPosition();
            }
            selectedCell = null;
        }
    }

    private void TrySwapCells(Cell cell1, Cell cell2)
    {
        Collider2D collider1 = cell1.GetComponent<Collider2D>();
        Collider2D collider2 = cell2.GetComponent<Collider2D>();
            Vector3 tempPosition = cell1.InitialPosition;
            cell1.transform.position = cell2.InitialPosition;
            cell2.transform.position = tempPosition;
            cell1.ResetInitialPosition();
            cell2.ResetInitialPosition();

            Debug.Log("Celdas intercambiadas.");

    }

}
