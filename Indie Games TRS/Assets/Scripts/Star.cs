using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2D;

    private void Start()
    {
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2D.intensity = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Level level = FindObjectOfType<Level>();
            level.starsCount++;
            StartCoroutine(EnableLight());
        }
    }

    private IEnumerator EnableLight()
    {
        while(light2D.intensity < 12)
        {
            light2D.intensity += 12f * Time.deltaTime;
            yield return null;
        }
        while (light2D.intensity > 6)
        {
            light2D.intensity -= 6f * Time.deltaTime;
            yield return null;
        }
    }
}
