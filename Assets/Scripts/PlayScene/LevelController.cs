using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class LevelController : MonoBehaviour
    {
        public GameObject[] levels;

        private GameObject lastLevel;

        private void Start()
        {
            Instantiate(levels[Random.Range(0, levels.Length)], new Vector3(32, 0, 0), Quaternion.identity);
            Instantiate(levels[Random.Range(0, levels.Length)], new Vector3(64, 0, 0), Quaternion.identity);
            lastLevel = Instantiate(levels[Random.Range(0, levels.Length)], new Vector3(96, 0, 0), Quaternion.identity);
        }

        private void LateUpdate()
        {
            if (lastLevel.transform.position.x <= 64)
            {
                lastLevel = Instantiate(levels[Random.Range(0, levels.Length)], new Vector3(lastLevel.transform.position.x + 32, 0, 0), Quaternion.identity);
            }
        }
    }
}