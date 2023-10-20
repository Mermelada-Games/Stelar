using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }

    public Vector3 InitialPosition
    {
        get { return initialPosition; }
    }

    public void ResetInitialPosition()
    {
        initialPosition = transform.position;
    }
}
