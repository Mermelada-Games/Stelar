using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float moveDistance = 2.5f;
    private Vector3 initialPosition;

    private GridSystem gridSystem;
    private Cell currentCell;
    private Cell nextCell;
    private Vector3 moveDirection = Vector3.zero;
    private bool isMoving = false;
    private bool canMove = false;

    private void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>();
    }

    void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, initialPosition) < moveDistance)
            {
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                RotateDirection(moveDirection);
            }
            else
            {
                isMoving = false;
            }
        }
        else
        {
            if (canMove) Move();
        }
    }

    public void Move()
    {
        initialPosition = transform.position;
        moveDistance = 2.5f;
        currentCell = gridSystem.GetCellAtPosition(transform.position);
        nextCell = gridSystem.GetNextCell(currentCell);
        if (currentCell != null && nextCell != null && !currentCell.isEndCell && AreDirectionsValid(currentCell.exitCellFace, nextCell.enterCellFace))
        {
            isMoving = true;

            if (currentCell.exitCellFace == Cell.CellFace.Up)
            {
                moveDirection = Vector3.up;
            }
            else if (currentCell.exitCellFace == Cell.CellFace.Down)
            {
                moveDirection = Vector3.down;
            }
            else if (currentCell.exitCellFace == Cell.CellFace.Left)
            {
                moveDirection = Vector3.left;
            }
            else if (currentCell.exitCellFace == Cell.CellFace.Right)
            {
                moveDirection = Vector3.right;
            }
        }
        else 
        {
            canMove = false;
        }
    }

    private bool AreDirectionsValid(Cell.CellFace exitFace, Cell.CellFace enterFace)
    {
        Dictionary<Cell.CellFace, Cell.CellFace> validPairs = new Dictionary<Cell.CellFace, Cell.CellFace>
        {
            { Cell.CellFace.Right, Cell.CellFace.Left },
            { Cell.CellFace.Left, Cell.CellFace.Right },
            { Cell.CellFace.Up, Cell.CellFace.Down },
            { Cell.CellFace.Down, Cell.CellFace.Up }
        };

        return validPairs.ContainsKey(exitFace) && validPairs[exitFace] == enterFace;
    }

    private void RotateDirection(Vector3 direction)
    {
        float angle = 0f;
        if (direction == Vector3.up)
        {
            angle = 90f;
        }
        else if (direction == Vector3.down)
        {
            angle = -90f;
        }
        else if (direction == Vector3.left)
        {
            angle = 180f;
        }
        else if (direction == Vector3.right)
        {
            angle = 0f;
        }

        foreach (Transform child in transform)
        {
            child.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void startMovement()
    {
        canMove = true;
        gridSystem.StartGame();
    }
}
