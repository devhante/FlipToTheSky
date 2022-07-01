using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FTS.TheBackend;
using BackEnd;

namespace FTS.LobbyScene
{
    public class UIController : MonoBehaviour
    {
        public TMP_Text coinText;
        public Button playButton;
        public Button shopButton;

        private void Awake()
        {
            playButton.onClick.AddListener(OnClickPlayButton);
            shopButton.onClick.AddListener(OnClickShopButton);
        }

        private void Start()
        {
            LobbyManager.Instance.Coin = BackendManager.Instance.GetCoin();
        }

        private void Update()
        {
            coinText.text = LobbyManager.Instance.Coin.ToString();
        }

        private void OnClickPlayButton()
        {
            SceneManager.LoadScene("PlayScene");
        }

        private void OnClickShopButton()
        {
            SceneManager.LoadScene("ShopScene");
        }
    }
}
