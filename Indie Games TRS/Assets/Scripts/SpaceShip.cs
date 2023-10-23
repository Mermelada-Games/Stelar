using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float moveDistance = 2.5f;
    [SerializeField] private float rotationSpeed = 90.0f;
    [SerializeField] private int maxMovements;
    
    private Vector3 initialPosition;
    private Vector3 firstPosition;

    private GridSystem gridSystem;
    private Level level;
    private Cell currentCell;
    private Cell nextCell;
    private Vector3 moveDirection = Vector3.zero;
    private bool isMoving = false;
    private bool canMove = false;

    private void Start()
    {
        firstPosition = transform.position;
        level = FindObjectOfType<Level>();
        gridSystem = FindObjectOfType<GridSystem>();
        currentCell = gridSystem.GetCellAtPosition(transform.position);
        Debug.Log(maxMovements);
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
            else RestartGame();
        }

        if (currentCell.isEndCell)
        {
            level.endCell = true;
        }
    }

    public void Move()
    {
        initialPosition = transform.position;
        moveDistance = 2.5f;
        currentCell = gridSystem.GetCellAtPosition(transform.position);
        nextCell = gridSystem.GetNextCell(currentCell);
        if (currentCell != null && nextCell != null && !currentCell.isEndCell && maxMovements > 0)
        {
            if (currentCell.isRotationCell)
            {
                gridSystem.RotateCell(currentCell);
            }

            if (AreDirectionsValid(currentCell.exitCellFace, nextCell.enterCellFace) ||
                AreDirectionsValid(currentCell.exitCellFace, nextCell.exitCellFace))
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


                if (AreDirectionsValid(currentCell.exitCellFace, nextCell.exitCellFace))
                {
                    nextCell.exitCellFace = nextCell.enterCellFace;
                }

                maxMovements--;
                // Debug.Log(maxMovements);
            }
            else
            {
                moveDistance = 1.0f;

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

                canMove = false;
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
        Vector3 targetEulerAngles = Vector3.zero;

        if (direction == Vector3.up)
        {
            targetEulerAngles = new Vector3(0, 0, 90);
        }
        else if (direction == Vector3.down)
        {
            targetEulerAngles = new Vector3(0, 0, -90);
        }
        else if (direction == Vector3.left)
        {
            targetEulerAngles = new Vector3(0, 0, 180);
        }
        else if (direction == Vector3.right)
        {
            targetEulerAngles = Vector3.zero;
        }

        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        foreach (Transform child in transform)
        {
            child.rotation = Quaternion.RotateTowards(child.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void startMovement()
    {
        canMove = true;
        gridSystem.StartGame();
    }

    public void RestartGame()
    {
        canMove = false;
        transform.position = firstPosition;
        gridSystem.RestartGame();
    }

}
