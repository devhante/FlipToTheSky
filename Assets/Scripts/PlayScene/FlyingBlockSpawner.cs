using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class FlyingBlockSpawner : MonoBehaviour
    {
        private static FlyingBlockSpawner instance = null;

        public static FlyingBlockSpawner Instance
        {
            get
            {
                return instance;
            }
        }

        public int MaxHorizontalIndex { get; private set; } = 7;
        public int MaxVerticalIndex { get; private set; } = 10;

        public GameObject flyingLeftBlock;
        public GameObject flyingUpBlock;

        private List<Vector2> flyingLeftSpawnPointList;
        private List<Vector2> flyingUpSpawnPointList;

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            flyingLeftSpawnPointList = new List<Vector2>
            {
                new Vector2(20f, 7.5f),
                new Vector2(20f, 5f),
                new Vector2(20f, 2.5f),
                new Vector2(20f, 0f),
                new Vector2(20f, -2.5f),
                new Vector2(20f, -5f),
                new Vector2(20f, -7.5f)
            };
            flyingUpSpawnPointList = new List<Vector2>
            {
                new Vector2(-14.5f, -13f),
                new Vector2(-11.25f, -13f),
                new Vector2(-8f, -13f),
                new Vector2(-4.75f, -13f),
                new Vector2(-1.5f, -13f),
                new Vector2(1.75f, -13f),
                new Vector2(5f, -13f),
                new Vector2(8.25f, -13f),
                new Vector2(11.5f, -13f),
                new Vector2(-14.75f, -13f),
            };
        }

        public FlyingBlock SpawnHorizontalBlock(int index, float delay)
        {
            FlyingBlock result = null;
            Vector2 cameraPos = MainCamera.Instance.transform.position;

            if (index < MaxHorizontalIndex)
            {
                result = Instantiate(flyingLeftBlock, cameraPos + flyingLeftSpawnPointList[index], Quaternion.identity, transform).GetComponent<FlyingBlock>();
                result.WarningIndex = index;
                result.WarningTime = delay;
            }

            return result;
        }

        public FlyingBlock SpawnVerticalBlock(int index, float delay)
        {
            FlyingBlock result = null;
            Vector2 cameraPos = MainCamera.Instance.transform.position;
            
            if (index < MaxVerticalIndex)
            {
                result = Instantiate(flyingUpBlock, cameraPos + flyingUpSpawnPointList[index], Quaternion.identity, transform).GetComponent<FlyingBlock>();
                result.WarningIndex = index;
                result.WarningTime = delay;
            }

            return result;
        }
    }
}