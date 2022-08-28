using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTS.PlayScene
{
    public class DashButton : SimpleButton
    {
        public Sprite[] dashSprites;

        protected override void OnClick()
        {
            Player.Instance.OnClickDashButton();
        }

        private void Update()
        {
            if (dashSprites.Length > Player.Instance.DashCount)
            {
                buttonComponent.image.sprite = dashSprites[Player.Instance.DashCount];
            }
        }
    }
}