using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    private Cell selectedCell;
    private Level level;
    private int sortingOrder = 0;
    private bool isGameStarted = false;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    private void Update()
    {
        if (!isGameStarted)
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
                Collider2D[] colliders = Physics2D.OverlapCircleAll(selectedCell.transform.position, 0.05f);

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
                        else
                        {
                            selectedCell.transform.position = selectedCell.CellPosition;
                        }
                    }
                }
                else
                {
                    selectedCell.transform.position = selectedCell.CellPosition;
                }
                selectedCell = null;
            }
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

    public Cell GetCellAtPosition(Vector3 position)
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell cell in cells)
        {
            float cellSize = 2.0f;
            if (Vector3.Distance(cell.transform.position, position) < cellSize / 2)
            {
                return cell;
            }
        }
        return null;
    }

    public Cell GetNextCell(Cell currentCell)
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
            return nextCell;
        }
        return null;
    }

    public void StartGame()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell cell in cells)
        {
            Collider2D collider = cell.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
            cell.canDrag = false;
            cell.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        isGameStarted = true;
    }

    public void RestartGame()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        foreach (Cell cell in cells)
        {
            cell.DisableCollider();
            cell.enterCellFace = cell.initialEnterCellFace;
            cell.exitCellFace = cell.initialExitCellFace;
            cell.canDrag = true;
            cell.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        level.RestartLevel();
        isGameStarted = false;
    }

    public void RotateCell(Cell cell)
    {
        cell.transform.Rotate(0, 0, -90);

        switch (cell.exitCellFace)
        {
            case Cell.CellFace.Up:
                cell.exitCellFace = Cell.CellFace.Right;
                break;
            case Cell.CellFace.Right:
                cell.exitCellFace = Cell.CellFace.Down;
                break;
            case Cell.CellFace.Down:
                cell.exitCellFace = Cell.CellFace.Left;
                break;
            case Cell.CellFace.Left:
                cell.exitCellFace = Cell.CellFace.Up;
                break;
        }

        switch (cell.enterCellFace)
        {
            case Cell.CellFace.Up:
                cell.enterCellFace = Cell.CellFace.Right;
                break;
            case Cell.CellFace.Right:
                cell.enterCellFace = Cell.CellFace.Down;
                break;
            case Cell.CellFace.Down:
                cell.enterCellFace = Cell.CellFace.Left;
                break;
            case Cell.CellFace.Left:
                cell.enterCellFace = Cell.CellFace.Up;
                break;
        }
    }


}
