using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

        private void Update()
        {
            coinText.text = GameManager.Instance.UserInfo.havingCoin.ToString();
        }

        private void OnClickPickOneButton()
        {
            if (GameManager.Instance.UserInfo.havingCoin >= ShopManager.Instance.pickOnePrice)
            {
                BackendManager.Instance.UseCoin(ShopManager.Instance.pickOnePrice, () =>
                {
                    GameManager.Instance.UpdateUserInfo(() => { });
                });
            }
        }

        private void OnClickPickFiveButton()
        {
            if (GameManager.Instance.UserInfo.havingCoin >= ShopManager.Instance.pickFivePrice)
            {
                BackendManager.Instance.UseCoin(ShopManager.Instance.pickFivePrice, () =>
                {
                    GameManager.Instance.UpdateUserInfo(() => { });
                });
            }
        }

        private void OnClickBackButton()
        {
            GameManager.Instance.LoadScene("LobbyScene", (callback) =>
            {
                GameManager.Instance.UpdateUserInfo(() =>
                {
                    callback();
                });
            });
        }
    }
}