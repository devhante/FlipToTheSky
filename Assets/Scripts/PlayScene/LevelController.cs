using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class LevelController : MonoBehaviour
    {
        public GameObject[] runLevels;
        public GameObject flipLevel;
        public GameObject[] flyLevels;
        public GameObject landLevel;

        private int levelCount;
        private PlayPhase levelPhase;
        private GameObject lastLevel;

        private void Start()
        {
            levelCount = 1;
            levelPhase = PlayPhase.Run;
            Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(30, 0, 0), Quaternion.identity);
            Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(60, 0, 0), Quaternion.identity);
            lastLevel = Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(90, 0, 0), Quaternion.identity);
            StartCoroutine(SpawnFlyBlockCoroutine());
        }

        private IEnumerator SpawnFlyBlockCoroutine()
        {
            while (true)
            {
                Debug.Log(PlayManager.Instance.Phase);
                if (PlayManager.Instance.Phase != PlayPhase.Fly)
                {
                    yield return null;
                }
                else
                {
                    int hMax = PlayManager.Instance.UIController.warnings.warningsH.Length;
                    int vMax = PlayManager.Instance.UIController.warnings.warningsV.Length;
                    bool isH = Random.Range(0, 2) == 0;
                    int coordH = isH ? Random.Range(1, hMax + 1) : 0;
                    int coordV = isH ? 0 : Random.Range(1, vMax + 1);
                    PlayManager.Instance.UIController.warnings.ShowWarning(coordH, coordV, 2);
                    yield return new WaitForSeconds(2);
                }
            }
        }

        private void LateUpdate()
        {
            if (lastLevel.transform.position.x <= 60)
            {
                if (levelCount <= 0)
                {
                    if (levelPhase == PlayPhase.Run)
                    {
                        levelPhase = PlayPhase.Flip;
                        levelCount = 3;
                    }
                    else if (levelPhase == PlayPhase.Flip)
                    {
                        levelPhase = PlayPhase.Fly;
                        levelCount = 5;
                    }
                    else if (levelPhase == PlayPhase.Fly)
                    {
                        levelPhase = PlayPhase.Land;
                        levelCount = 3;
                    }
                    else if (levelPhase == PlayPhase.Land)
                    {
                        levelPhase = PlayPhase.Run;
                        levelCount = 5;
                    }
                }

                switch (levelPhase)
                {
                    case PlayPhase.Run:
                        lastLevel = Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(lastLevel.transform.position.x + 30, 0, 0), Quaternion.identity);
                        break;
                    case PlayPhase.Flip:
                        lastLevel = Instantiate(flipLevel, new Vector3(lastLevel.transform.position.x + 30, 0, 0), Quaternion.identity);
                        break;
                    case PlayPhase.Fly:
                        lastLevel = Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(lastLevel.transform.position.x + 30, 0, 0), Quaternion.identity);
                        break;
                    case PlayPhase.Land:
                        lastLevel = Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(lastLevel.transform.position.x + 30, 0, 0), Quaternion.identity);
                        break;
                }

                levelCount--;
            }
        }
    }
}