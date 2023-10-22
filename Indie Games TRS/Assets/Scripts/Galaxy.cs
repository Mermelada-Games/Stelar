using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Galaxy : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    private CameraController cameraController;

    private void Start()
    {
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
                cameraController.startLevel = true;
                StartCoroutine(ChangeLevel());
            }
        }
    }

    private IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Scene");
    }
}
