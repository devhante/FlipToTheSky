using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public class BlockSpawner : MonoBehaviour
    {
        public GameObject blockH;
        public GameObject blockV;

        private List<Vector2> spawnPointH;
        private List<Vector2> spawnPointV;

        private void Awake()
        {
            spawnPointH = new List<Vector2>
            {
                new Vector2(20f, 7.5f),
                new Vector2(20f, 5f),
                new Vector2(20f, 2.5f),
                new Vector2(20f, 0f),
                new Vector2(20f, -2.5f),
                new Vector2(20f, -5f),
                new Vector2(20f, -7.5f)
            };
            spawnPointV = new List<Vector2>
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

        public void Spawn(int h, int v)
        {
            Vector2 cameraPos = PlayManager.Instance.MainCamera.transform.position;

            if (h > 0 && h <= spawnPointH.Count && v == 0)
            {
                Instantiate(blockH, cameraPos + spawnPointH[h - 1], Quaternion.identity);
            }
            else if (v > 0 && v <= spawnPointV.Count && h == 0)
            {
                Instantiate(blockV, cameraPos + spawnPointV[v - 1], Quaternion.identity);
            }
        }
    }
}