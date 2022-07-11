using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class Warnings : MonoBehaviour
    {
        public GameObject[] warningsX;
        public GameObject[] warningsY;

        public void ShowWarning(int x, int y, float time)
        {
            StartCoroutine(ShowWarningCoroutine(x, y, time));
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
    }
}