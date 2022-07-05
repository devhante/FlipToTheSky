using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FTS.PlayScene
{
    public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool isButtonDown = false;

        private void Update()
        {
            if (isButtonDown)
            {
                PlayManager.Instance.Player.OnHoldJumpButton();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isButtonDown = true;
            PlayManager.Instance.Player.OnPressJumpButton();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isButtonDown = false;
            PlayManager.Instance.Player.OnReleaseJumpButton();
        }
    }
}