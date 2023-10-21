using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Level level = FindObjectOfType<Level>();
            level.score++;
            Debug.Log("Score: " + level.score);
            Destroy(gameObject);
        }
    }
}
