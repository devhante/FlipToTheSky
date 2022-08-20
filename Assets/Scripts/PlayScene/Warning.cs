using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTS.PlayScene
{
    public class Warning : MonoBehaviour
    {
        private Image imageComponent;
        private float alpha = 1;
        private float destAlpha = 0;

        private void Awake()
        {
            imageComponent = GetComponent<Image>();
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

            imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, alpha);
        }
    }
}