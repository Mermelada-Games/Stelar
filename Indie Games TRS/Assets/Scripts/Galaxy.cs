using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private GameObject constellation;
    private SceneFade sceneFade;
    private CameraController cameraController;

    private void Start()
    {
        sceneFade = FindObjectOfType<SceneFade>();
        cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Constellation"))
            {
                if ( !cameraController.levelSelected)
                {
                    cameraController.targetTransform = hit.collider.transform;
                    cameraController.selectLevel = true;
                }
                else
                {
                    sceneFade.EndLevel("Scene"); 
                }
            }
        }
    }
}
