using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] public Transform targetTransform;
    [SerializeField] public float zoomInSize = 2.0f;
    [SerializeField] public float zoomOutSize = 5.0f;
    [SerializeField] public float zoomSpeed = 2.0f;

    private float currentSize;
    private float targetSize;

    public bool startLevel = false;
    private bool levelStarted = false;

    private void Start()
    {
        currentSize = virtualCamera.m_Lens.OrthographicSize;
        targetSize = currentSize;
    }

    void Update()
    {
        if (startLevel && !levelStarted)
        {
            levelStarted = true;
            targetSize = zoomInSize;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            targetSize = zoomOutSize;
        }

        if (currentSize != targetSize)
        {
            currentSize = Mathf.Lerp(currentSize, targetSize, Time.deltaTime * zoomSpeed);
            ChangeCameraZoomAndLookAt(targetTransform, currentSize);
        }
    }

    void ChangeCameraZoomAndLookAt(Transform target, float newSize)
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.OrthographicSize = newSize;
            virtualCamera.m_Follow = target;
            virtualCamera.m_LookAt = target;
        }
    }
}
