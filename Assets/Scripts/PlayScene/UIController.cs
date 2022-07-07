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
        public TMP_Text dreampieceText;
        public Button dashButton;
        public Image dashMask;
        public Button pauseButton;
        public GameObject pauseMask;
        public Button resumeButton;
        public Button restartButton;
        public Button exitButton;
        public GameObject hitEffect;

        public Sprite filledLifeSprite;
        public Sprite emptyLifeSprite;

        public Sprite[] dashSprites;

        private float dashCooldownValue;

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

            dreampieceText.text =  PlayManager.Instance.Dreampiece.ToString();

            if (dashSprites.Length >= 4)
            {
                dashButton.image.sprite = dashSprites[PlayManager.Instance.Player.dashCount];
            }

            if (PlayManager.Instance.Player.dashCount < 3)
            {
                dashCooldownValue = (PlayManager.Instance.Player.dashCooldown - PlayManager.Instance.Player.dashRemainingCooldown) / PlayManager.Instance.Player.dashCooldown;
                dashMask.rectTransform.offsetMax = new Vector2(dashMask.rectTransform.offsetMax.x, dashCooldownValue * -200);
            }
            else
            {
                dashMask.rectTransform.offsetMax = new Vector2(dashMask.rectTransform.offsetMax.x, -200);
            }
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
                BackendManager.Instance.SaveCoin(PlayManager.Instance.Dreampiece, () =>
                {
                    GameManager.Instance.UpdateUserInfo(() => 
                    {
                        callback();
                    });
                });
            });
        }

        public void PlayHitEffect()
        {
            StartCoroutine(HitEffectCoroutine());
        }

        private IEnumerator HitEffectCoroutine()
        {
            hitEffect.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            hitEffect.SetActive(false);
        }
    }
}