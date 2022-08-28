using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FTS.PlayScene
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Vector3 Normal { get; private set; }

        private Transform handle;
        private float radius;
        private int xAxis;
        private int yAxis;

        private void Awake()
        {
            handle = transform.GetChild(0);
            radius = GetComponent<RectTransform>().sizeDelta.x * 0.5f;
        }

        private void Start()
        {
            StartCoroutine(ChangeControllerPositionCoroutine());
        }

        private IEnumerator ChangeControllerPositionCoroutine()
        {
            yield return null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine("PointerDown");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopCoroutine("PointerDown");
            handle.transform.position = transform.position;
            Normal = Vector3.zero;
        }
    }
}
