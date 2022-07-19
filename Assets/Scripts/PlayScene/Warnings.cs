using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class Warnings : MonoBehaviour
    {
        public GameObject[] warningsH;
        public GameObject[] warningsV;
        public BlockSpawner blockSpawner;

        private List<IEnumerator> coroutines;

        private void Awake()
        {
            coroutines = new List<IEnumerator>();
        }

        public void ShowWarning(int h, int v, float time)
        {
            IEnumerator coroutine = ShowWarningCoroutine(h, v, time);
            coroutines.Add(coroutine);
            StartCoroutine(coroutine);
        }

        private IEnumerator ShowWarningCoroutine(int h, int v, float time)
        {
            GameObject target = null;

            if (h > 0 && h <= warningsH.Length && v == 0)
            {
                target = warningsH[h - 1];
                
            }
            else if (v > 0 && v <= warningsV.Length && h == 0)
            {
                target = warningsV[v - 1];
            }

            if (target != null)
            {
                target.SetActive(true);
                yield return new WaitForSeconds(time);

                target.SetActive(false);
                blockSpawner.Spawn(h, v);
            }
        }

        public void HideWarnings()
        {
            foreach (var i in coroutines)
            {
                StopCoroutine(i);
            }
            foreach (var i in warningsH)
            {
                i.SetActive(false);
            }
            foreach (var i in warningsV)
            {
                i.SetActive(false);
            }
        }
    }
}