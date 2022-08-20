using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class LevelSpawner : MonoBehaviour
    {
        public GameObject[] runLevels;
        public GameObject flipLevel;
        public GameObject[] flyLevels;
        public GameObject landLevel;

        private int levelCount;
        private GamePhase levelPhase;
        private GameObject lastLevel;

        private void Start()
        {
            levelCount = 1;
            levelPhase = GamePhase.Run;
            Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(30, 0, 0), Quaternion.identity);
            Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(60, 0, 0), Quaternion.identity);
            lastLevel = Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(90, 0, 0), Quaternion.identity);
            StartCoroutine(SpawnFlyBlockCoroutine());
        }

        private IEnumerator SpawnFlyBlockCoroutine()
        {
            while (true)
            {
                if (PlaySceneManager.Instance.Phase != GamePhase.Fly)
                {
                    yield return null;
                }
                else
                {
                    bool isHorizontal = Random.Range(0, 2) == 0;

                    int maxIndex = isHorizontal ? FlyingBlockSpawner.Instance.MaxHorizontalIndex
                        : FlyingBlockSpawner.Instance.MaxVerticalIndex;

                    int index = Random.Range(0, maxIndex);

                    if (isHorizontal)
                        FlyingBlockSpawner.Instance.SpawnHorizontalBlock(index, 2);
                    else
                        FlyingBlockSpawner.Instance.SpawnVerticalBlock(index, 2);

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
                    if (levelPhase == GamePhase.Run)
                    {
                        levelPhase = GamePhase.Flip;
                        levelCount = 3;
                    }
                    else if (levelPhase == GamePhase.Flip)
                    {
                        levelPhase = GamePhase.Fly;
                        levelCount = 5;
                    }
                    else if (levelPhase == GamePhase.Fly)
                    {
                        levelPhase = GamePhase.Land;
                        levelCount = 3;
                    }
                    else if (levelPhase == GamePhase.Land)
                    {
                        levelPhase = GamePhase.Run;
                        levelCount = 5;
                    }
                }

                switch (levelPhase)
                {
                    case GamePhase.Run:
                        lastLevel = Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(lastLevel.transform.position.x + 30, 0, 0), Quaternion.identity);
                        break;
                    case GamePhase.Flip:
                        lastLevel = Instantiate(flipLevel, new Vector3(lastLevel.transform.position.x + 30, 0, 0), Quaternion.identity);
                        break;
                    case GamePhase.Fly:
                        lastLevel = Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(lastLevel.transform.position.x + 30, 0, 0), Quaternion.identity);
                        break;
                    case GamePhase.Land:
                        lastLevel = Instantiate(runLevels[Random.Range(0, runLevels.Length)], new Vector3(lastLevel.transform.position.x + 30, 0, 0), Quaternion.identity);
                        break;
                }

                levelCount--;
            }
        }
    }
}