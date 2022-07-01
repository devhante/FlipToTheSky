using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;
using FTS.TheBackend;

namespace FTS.ShopScene
{
    public class UIController : MonoBehaviour
    {
        public TMP_Text coinText;
        public Button pickOneButton;
        public Button pickFiveButton;
        public Button backButton;

        private void Awake()
        {
            pickOneButton.onClick.AddListener(OnClickPickOneButton);
            pickFiveButton.onClick.AddListener(OnClickPickFiveButton);
            backButton.onClick.AddListener(OnClickBackButton);
        }

        private void Start()
        {
            ShopManager.Instance.Coin = BackendManager.Instance.GetCoin();
        }

        private void Update()
        {
            coinText.text = ShopManager.Instance.Coin.ToString();
        }

        private void OnClickPickOneButton()
        {
            if (ShopManager.Instance.Coin >= ShopManager.Instance.pickOnePrice)
            {
                BackendManager.Instance.UseCoin(ShopManager.Instance.pickOnePrice);
                ShopManager.Instance.Coin = BackendManager.Instance.GetCoin();
            }
        }

        private void OnClickPickFiveButton()
        {
            if (ShopManager.Instance.Coin >= ShopManager.Instance.pickFivePrice)
            {
                BackendManager.Instance.UseCoin(ShopManager.Instance.pickFivePrice);
                ShopManager.Instance.Coin = BackendManager.Instance.GetCoin();
            }
        }

        private void OnClickBackButton()
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
}