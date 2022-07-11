using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class MainCamera : MonoBehaviour
    {
        private float shakeAmount;
        private float shakeTime;

        public Vector3 InitialPosition
        {
            get; set;
        }

        public void Vibrate(float amount, float time)
        {
            shakeAmount = amount;
            shakeTime = time;
        }

        private void Start()
        {
            InitialPosition = transform.position;
        }

        private void Update()
        {
            if (shakeTime > 0)
            {
                transform.position = Random.insideUnitSphere * shakeAmount + InitialPosition;
                shakeTime -= Time.deltaTime;
            }
            else
            {
                shakeTime = 0;
                transform.position = InitialPosition;
            }
        }
    }
}