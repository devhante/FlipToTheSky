using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTS.PlayScene
{
    public class FlipTimer : MonoBehaviour
    {
        private Slider slider;

        public float TimeLimit { get; private set; } = 5;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public void StartTimer()
        {
            StartCoroutine(StartTimerCoroutine());
        }
        
        private IEnumerator StartTimerCoroutine()
        {
            float value = TimeLimit;
            
            while (value > 0)
            {
                value = Mathf.Max(value - Time.smoothDeltaTime, 0);
                slider.value = value / TimeLimit;
                yield return null;
            }
        }
    }
}
