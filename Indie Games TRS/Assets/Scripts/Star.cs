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
            level.CollectStar(level.starsCount - 1);
            EnableLight();
        }
    }

    private void EnableLight()
    {
        while(light2D.intensity < 1)
        {
            light2D.intensity += 0.05f * Time.deltaTime;
        }
    }
}
