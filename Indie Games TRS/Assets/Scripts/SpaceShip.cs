using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Cell exitPortalCell;
    private Cell balckHoleCell;
    private Vector3 moveDirection = Vector3.zero;
    private bool isMoving = false;
    private bool canMove = false;
    private bool isWaiting = false;

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
            if (!isWaiting)
            {
                if (canMove) Move();
                else if (!currentCell.isEndCell && !currentCell.isStartCell) RestartGame();
            }
            
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
            Debug.Log("currentCell.enterCellFace: " + currentCell.enterCellFace + " currentCell.exitCellFace: " + currentCell.exitCellFace + "nextCell.enterCellFace: " + nextCell.enterCellFace + " nextCell.exitCellFace: " + nextCell.exitCellFace);
            
            if (currentCell.isRotationCell)
            {
                Debug.Log("RotateCell");
                gridSystem.RotateCell(currentCell);
                currentCell = gridSystem.GetCellAtPosition(transform.position);
                nextCell = gridSystem.GetNextCell(currentCell);
                StartCoroutine(Wait());
            }

            if (currentCell.isPortalEnterCell)
            {
                Debug.Log("PortalEnterCell");
                exitPortalCell = gridSystem.Portal();
                transform.position = exitPortalCell.transform.position;
                currentCell = gridSystem.GetCellAtPosition(transform.position);
                nextCell = gridSystem.GetNextCell(currentCell);
                StartCoroutine(Wait());
            }

            if (currentCell.isBlackHoleCell)
            {
                Debug.Log("BlackHoleCell");
                StartCoroutine(MoveToBlackHole());
            }

            if (!isWaiting)
            {
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
        balckHoleCell = gridSystem.IsInBlackHoleZone();
    }

    public void RestartGame()
    {
        canMove = false;
        transform.position = firstPosition;
        gridSystem.RestartGame();
        currentCell = gridSystem.GetCellAtPosition(transform.position);
    }

    private IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1.0f);
        isWaiting = false;
    }

    private IEnumerator MoveToBlackHole()
    {
        isWaiting = true;
        Vector3 targetPosition = balckHoleCell.transform.position;
        float journeyLength = Vector3.Distance(transform.position, targetPosition);
        float distanceCovered = 0;
        float moveSpeed = 0.1f;

        while (distanceCovered <= 0.3f)
        {
            float journeyFraction = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetPosition, journeyFraction);
            distanceCovered += moveSpeed * Time.deltaTime;

            yield return null;
        }

        RestartGame();
        isWaiting = false;
    }

}
