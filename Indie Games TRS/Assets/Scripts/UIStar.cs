using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStar : MonoBehaviour
{
    private Image starImage;
    private Color initialColor;
    private bool collected = false;

    private void Start()
    {
        starImage = GetComponent<Image>();
        initialColor = starImage.color;

        Color startColor = starImage.color;
        startColor.a = 0.5f;
        starImage.color = startColor;
    }

    public void CollectStar()
    {
        if (!collected)
        {
            collected = true;

            starImage.color = initialColor;
        }
    }
}
