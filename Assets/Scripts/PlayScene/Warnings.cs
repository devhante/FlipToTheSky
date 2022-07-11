using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class Warnings : MonoBehaviour
    {
        public GameObject[] warningsX;
        public GameObject[] warningsY;

        private List<IEnumerator> coroutines;

        private void Awake()
        {
            coroutines = new List<IEnumerator>();
        }

        public void ShowWarning(int x, int y, float time)
        {
            IEnumerator coroutine = ShowWarningCoroutine(x, y, time);
            coroutines.Add(coroutine);
            StartCoroutine(coroutine);
        }

        private IEnumerator ShowWarningCoroutine(int x, int y, float time)
        {
            GameObject target = null;

            if (x > 0 && x <= warningsX.Length && y == 0)
            {
                target = warningsX[x];
                
            }
            else if (y > 0 && y <= warningsY.Length && x == 0)
            {
                target = warningsY[y];
            }

            if (target != null)
            {
                target.SetActive(true);
                yield return new WaitForSeconds(time);
                target.SetActive(false);
            }
        }

        public void HideWarnings()
        {
            foreach (var i in coroutines)
            {
                StopCoroutine(i);
            }
            foreach (var i in warningsX)
            {
                i.SetActive(false);
            }
            foreach (var i in warningsY)
            {
                i.SetActive(false);
            }
        }
    }
}