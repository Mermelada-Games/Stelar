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
                GetNextCellFaces(selectedCell);
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
                selectedCell.ResetCellPosition();
            }
            selectedCell = null;
        }
    }

    private void TrySwapCells(Cell cell1, Cell cell2)
    {
        Vector3 tempPosition = cell1.CellPosition;
        cell1.transform.position = cell2.CellPosition;
        cell2.transform.position = tempPosition;
        cell1.ResetCellPosition();
        cell2.ResetCellPosition();
    }

    public void ResetGrid()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell cell in cells)
        {
            cell.ResetInitialPosition();
        }
    }

    private void GetNextCellFaces(Cell currentCell)
    {
        Cell.CellFace exitface = currentCell.exitCellFace;

        Vector3 exitfacePosition = currentCell.transform.position;

        switch (exitface)
        {
            case Cell.CellFace.Up:
                exitfacePosition += Vector3.up * 2.0f;
                break;
            case Cell.CellFace.Down:
                exitfacePosition += Vector3.down * 2.0f;
                break;
            case Cell.CellFace.Left:
                exitfacePosition += Vector3.left * 2.0f;
                break;
            case Cell.CellFace.Right:
                exitfacePosition += Vector3.right * 2.0f;
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(exitfacePosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
            Cell nextCell = hit.collider.GetComponent<Cell>();
            Debug.Log("Enter face: " + nextCell.enterCellFace);
            Debug.Log("Exit face: " + nextCell.exitCellFace);
        }
    }

}
