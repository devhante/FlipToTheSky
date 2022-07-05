using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FTS.TheBackend;

namespace FTS.PlayScene
{
    public class UIController : MonoBehaviour
    {
        public Image[] lifeImages;
        public TMP_Text coinText;
        public TMP_Text dashText;
        public Slider dashCooldown;
        public Button dashButton;
        public Button pauseButton;
        public GameObject pauseMask;
        public Button resumeButton;
        public Button restartButton;
        public Button exitButton;

        public Sprite filledLifeSprite;
        public Sprite emptyLifeSprite;

        private void Awake()
        {
            dashButton.onClick.AddListener(OnClickDashButton);
            pauseButton.onClick.AddListener(OnClickPauseButton);
            resumeButton.onClick.AddListener(OnClickResumeButton);
            restartButton.onClick.AddListener(OnClickRestartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        private void Update()
        {
            lifeImages[0].sprite = PlayManager.Instance.PlayerLife < 1 ? emptyLifeSprite : filledLifeSprite;
            lifeImages[1].sprite = PlayManager.Instance.PlayerLife < 2 ? emptyLifeSprite : filledLifeSprite;
            lifeImages[2].sprite = PlayManager.Instance.PlayerLife < 3 ? emptyLifeSprite : filledLifeSprite;

            coinText.text = "Coin: " + PlayManager.Instance.Coin.ToString();
            dashText.text = "Dash: " + PlayManager.Instance.Player.dashCount.ToString();
            dashCooldown.value = (PlayManager.Instance.Player.dashCooldown - PlayManager.Instance.Player.dashRemainingCooldown) / PlayManager.Instance.Player.dashCooldown;
        }

        private void OnClickDashButton()
        {
            PlayManager.Instance.Player.Dash();
        }

        private void OnClickPauseButton()
        {
            Time.timeScale = 0;
            pauseMask.SetActive(true);
        }

        private void OnClickResumeButton()
        {
            Time.timeScale = 1;
            pauseMask.SetActive(false);
        }

        private void OnClickRestartButton()
        {
            Time.timeScale = 1;
            GameManager.Instance.LoadScene("PlayScene", (callback) =>
            {
                callback();
            });
        }

        private void OnClickExitButton()
        {
            Time.timeScale = 1;
            GameManager.Instance.LoadScene("LobbyScene", (callback) =>
            {
                BackendManager.Instance.SaveCoin(PlayManager.Instance.Coin, () =>
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