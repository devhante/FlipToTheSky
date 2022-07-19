using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    Image sr;
    float alpha = 1;
    float destAlpha = 0;

    private void Awake()
    {
        sr = GetComponent<Image>();
    }

    private void Update()
    {
        if (alpha == 1) destAlpha = 0;
        else if (alpha == 0) destAlpha = 1;
        
        if (destAlpha == 0)
        {
            alpha = Mathf.Max(alpha - Time.deltaTime, 0);
        }
        else if (destAlpha == 1)
        {
            alpha = Mathf.Min(alpha + Time.deltaTime, 1);
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }
}
