using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTS.PlayScene
{
    public class DashMask : MonoBehaviour
    {
        private Image imageComponent;

        private void Awake()
        {
            imageComponent = GetComponent<Image>();
        }

        private void Update()
        {
            float ratio;

            if (Player.Instance.DashCount < 3)
            {
                ratio = (Player.Instance.DashCooldown - Player.Instance.RemainingDashCooldown) / Player.Instance.DashCooldown;
                imageComponent.rectTransform.offsetMax = new Vector2(imageComponent.rectTransform.offsetMax.x, ratio * -200);
            }
            else
            {
                imageComponent.rectTransform.offsetMax = new Vector2(imageComponent.rectTransform.offsetMax.x, -200);
            }
        }
    }
}