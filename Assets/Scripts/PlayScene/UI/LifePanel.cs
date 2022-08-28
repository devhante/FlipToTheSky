using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class LifePanel : MonoBehaviour
    {
        public GameObject lifePrefab;

        private void Start()
        {
            for (int i = 0; i < PlaySceneManager.Instance.MaxLife; i++)
            {
                float xAxis = (PlaySceneManager.Instance.MaxLife - 1 - i) * -106;
                GameObject obj = Instantiate(lifePrefab, transform);
                obj.GetComponent<Life>().Index = i;
                obj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(xAxis, 0, 0);
            }
        }
    }
}