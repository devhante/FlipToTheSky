using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.MainScene
{
    public class MainManager : MonoBehaviour
    {
        private void Update()
        {
            // Mobile
            if (Input.touchCount > 0)
            {
                LoadScene();
            }

            // PC
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadScene();
            }
        }

        private void LoadScene()
        {
            GameManager.Instance.LoadScene("LobbyScene", (callback) =>
            {
                TheBackend.BackendManager.Instance.InitAndLogin(() =>
                {
                    GameManager.Instance.UpdateUserInfo(() =>
                    {
                        callback();
                    });
                });
            });
        }
    }
}