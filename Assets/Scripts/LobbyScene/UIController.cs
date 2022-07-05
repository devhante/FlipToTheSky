using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        private void Update()
        {
            coinText.text = GameManager.Instance.UserInfo.havingCoin.ToString();
        }

        private void OnClickPlayButton()
        {
            GameManager.Instance.LoadScene("PlayScene", (callback) => { callback(); });
        }

        private void OnClickShopButton()
        {
            GameManager.Instance.LoadScene("ShopScene", (callback) =>
            {
                GameManager.Instance.UpdateUserInfo(() =>
                {
                    callback();
                });
            });
        }
    }
}
