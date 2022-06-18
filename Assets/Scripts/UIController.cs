using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TMP_Text lifeText;
    public TMP_Text coinText;
    public TMP_Text dashText;
    public Slider dashCooldown;
    public Button dashButton;
    public Button pauseButton;
    public GameObject pauseMask;
    public Button resumeButton;
    public Button restartButton;

    private void Awake()
    {
        dashButton.onClick.AddListener(OnClickDashButton);
        pauseButton.onClick.AddListener(OnClickPauseButton);
        resumeButton.onClick.AddListener(OnClickResumeButton);
        restartButton.onClick.AddListener(OnClickRestartButton);
    }

    private void Update()
    {
        lifeText.text = "Life: " + GameManager.Instance.PlayerLife.ToString();
        coinText.text = "Coin: " + GameManager.Instance.Coin.ToString();
        dashText.text = "Dash: " + GameManager.Instance.Player.dashCount.ToString();
        dashCooldown.value = (GameManager.Instance.Player.dashCooldown - GameManager.Instance.Player.dashRemainingCooldown) / GameManager.Instance.Player.dashCooldown;
    }

    private void OnClickDashButton()
    {
        GameManager.Instance.Player.Dash();
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
        SceneManager.LoadScene(gameObject.scene.name);

        //TEST
        GameManager.Instance.Speed = 7;
        GameManager.Instance.Coin = 0;
        GameManager.Instance.PlayerLife = 3;
    }
}
