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
                if (Player.Instance.Status != PlayerStatus.Gliding)
                {
                    Player.Instance.StartGliding();
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isButtonDown = true;
            Player.Instance.Jump();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isButtonDown = false;
            Player.Instance.FinishGliding();
        }
    }
}