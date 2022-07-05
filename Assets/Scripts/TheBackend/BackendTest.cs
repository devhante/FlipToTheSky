using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

namespace FTS.TheBackend
{
    public class BackendTest : MonoBehaviour
    {
        public Button button;
        public InputField inputField;

        private void Awake()
        {
            button.onClick.AddListener(GetGoogleHash);
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (!Backend.IsInitialized)
            {
                Backend.InitializeAsync(true, bro =>
                {
                    if (bro.IsSuccess())
                    {
                        Debug.Log("초기화 성공!");
                    }
                    else
                    {
                        Debug.LogError("초기화 실패!");
                    }
                });
            }
        }

        public void GetGoogleHash()
        {
            string googleHashKey = Backend.Utils.GetGoogleHash();
            if (!string.IsNullOrEmpty(googleHashKey))
            {
                Debug.Log(googleHashKey);
                inputField.text = googleHashKey;
            }
        }
    }
}
