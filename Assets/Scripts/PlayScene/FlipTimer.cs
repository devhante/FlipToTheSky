using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FTS.PlayScene
{
    public class FlipTimer : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        float time = 5;
        public void StartTimer()
        {
            StartCoroutine(StartTimerCoroutine());
        }
        
        private IEnumerator StartTimerCoroutine()
        {
            float value = time;
            
            while (value > 0)
            {
                value = Mathf.Max(value - Time.smoothDeltaTime, 0);
                slider.value = value / time;
                yield return null;
            }
        }
    }
}
