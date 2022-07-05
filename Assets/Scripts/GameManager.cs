using FTS.TheBackend;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTS
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance = null;

        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        public struct UserInformation
        {
            public int havingCoin;
            public int earnedCoin;
        }

        public UserInformation UserInfo
        {
            get; private set;
        }

        private void Awake()
        {
            if (instance)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void UpdateUserInfo(callback callback)
        {
            BackendManager.Instance.GetUserInfo((value) =>
            {
                UserInfo = value;
                callback();
            });
        }

        public bool IsLoading
        {
            get; set;
        }

        public string LoadingSceneName
        {
            get; set;
        }

        public delegate void callback();
        public delegate void loadingCallback(callback finishLoadingCallback);

        public loadingCallback LoadingCallback
        {
            get; set;
        }

        public void LoadScene(string sceneName, loadingCallback callback)
        {
            IsLoading = true;
            LoadingSceneName = sceneName;
            LoadingCallback = callback;
            SceneManager.LoadScene("LoadingScene");
        }
    }

}