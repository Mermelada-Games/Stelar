using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] public float zoomInSize = 2.0f;
    [SerializeField] public float zoomOutSize = 5.0f;
    [SerializeField] public float zoomSpeed = 2.0f;
    [SerializeField] private Transform galaxyTransform;

    public Transform targetTransform;
    private float currentSize;
    private float targetSize;

    public bool selectLevel = false;
    private bool levelSelected = false;

    private void Start()
    {
        currentSize = virtualCamera.m_Lens.OrthographicSize;
        targetSize = currentSize;
    }

    private void Update()
    {
        if (selectLevel && !levelSelected)
        {
            levelSelected = true;
            targetSize = zoomInSize;
        }

        if (currentSize != targetSize)
        {
            currentSize = Mathf.Lerp(currentSize, targetSize, Time.deltaTime * zoomSpeed);
            ChangeCameraZoomAndLookAt(targetTransform, currentSize);
        }
    }

    private void ChangeCameraZoomAndLookAt(Transform target, float newSize)
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.OrthographicSize = newSize;
            virtualCamera.m_Follow = target;
            virtualCamera.m_LookAt = target;
        }
    }

    public void ZoomOut()
    {
        selectLevel = false;
        levelSelected = false;
        targetTransform = galaxyTransform;
        targetSize = zoomOutSize;
    }
}
