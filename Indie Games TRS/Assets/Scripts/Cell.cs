using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum CellFace
    {
        Up,
        Down,
        Right,
        Left
    }

    [SerializeField] public CellFace enterCellFace;
    [SerializeField] public CellFace exitCellFace;
    [SerializeField] public bool isStartCell;
    [SerializeField] public bool isEndCell;

    private Vector3 initialPosition;
    private Vector3 cellPosition;
    private bool isDragging = false;

    private void Start()
    {
        initialPosition = transform.position;
        cellPosition = transform.position;

        if (isStartCell || isEndCell)
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }

    public void ResetInitialPosition()
    {
        transform.position = initialPosition;
    }

    public Vector3 CellPosition
    {
        get { return cellPosition; }
    }

    public void ResetCellPosition()
    {
        cellPosition = transform.position;
    }
}
