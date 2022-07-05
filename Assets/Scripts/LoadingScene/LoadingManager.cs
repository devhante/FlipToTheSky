using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTS.LoadingScene
{
    public class LoadingManager : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.LoadingCallback(() =>
            {
                GameManager.Instance.IsLoading = false;
            });
        }

        private void Update()
        {
            if (!GameManager.Instance.IsLoading)
            {
                SceneManager.LoadScene(GameManager.Instance.LoadingSceneName);
            }
        }
    }
}
