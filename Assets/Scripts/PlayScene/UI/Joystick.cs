using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FTS.PlayScene
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public Vector3 Normal { get; private set; }

        private RectTransform rt;
        private RectTransform handleRT;
        private Canvas canvas;
        private float radius;
        private Vector2 mousePos;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
            handleRT = transform.GetChild(0).GetComponent<RectTransform>();
            canvas = transform.parent.GetComponent<Canvas>();
            radius = rt.sizeDelta.x * 0.5f;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            mousePos = (eventData.position - rt.anchoredPosition) / (radius * canvas.scaleFactor);
            if (mousePos.magnitude > 1) mousePos = mousePos.normalized;
            if (mousePos.magnitude <= 0) mousePos = Vector2.zero;
            handleRT.anchoredPosition = mousePos * radius;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            mousePos = Vector2.zero;
            handleRT.anchoredPosition = Vector2.zero;
        }
    }
}
