using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private GameObject[] constellation;
    
    [SerializeField] private string[] sceneName;
    private SceneFade sceneFade;
    private CameraController cameraController;
    private SoundManager soundManager;

    private void Start()
    {
        sceneFade = FindObjectOfType<SceneFade>();
        cameraController = FindObjectOfType<CameraController>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        soundManager.PauseMusic();
        soundManager.PlayMusic();
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

                    for (int i = 0; i < constellation.Length; i++)
                    {
                        if (hit.collider == constellation[i].GetComponent<Collider2D>())
                        {
                            StartCoroutine(EnterLevel(i));
                        }
                    }
                }
            }
        }
    }

    private IEnumerator EnterLevel(int i)
    {
        yield return new WaitForSeconds(2.0f);
        sceneFade.EndLevel(sceneName[i]);
    }
}
